using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using Services;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.AlzzoniAdmin.Controllers
{
    [Area("AlzzoniAdmin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly IWebHostEnvironment _webHost;
        private readonly PictureManager _pictureManager;
        public ProductsController(ProductManager productManager, CategoryManager categoryManager, IWebHostEnvironment webHost, PictureManager pictureManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _webHost = webHost;
            _pictureManager = pictureManager;
        }

        // GET: AlzzoniAdmin/Products
        public IActionResult Index()
        {
            var selectedProduct = _productManager.GetProducts();
            return View(selectedProduct);
        }

        // GET: AlzzoniAdmin/Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var selectedProduct = _productManager.GetById(id.Value);
            if (selectedProduct == null)
                return NotFound();
            return View(selectedProduct);
        }

        // GET: AlzzoniAdmin/Products/Create
        public IActionResult Create()
        {
             ViewBag.catList=_categoryManager.GetCategories();
            return View();
        }

        // POST: AlzzoniAdmin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Price,Discount,InStock,PhotoUrl,SKU,Barcode,PublishDate,ModifiedOn,IsDeleted,IsSlider,IsFeatured,CategoryId,Id")] Product product,IFormFile[] PictureUrlss,int? thumbnailPictureId)
        {
            ViewBag.catList = _categoryManager.GetCategories();
            if (ModelState.IsValid)
            {
                product.ProductPictures = new List<ProductPicture>();
                foreach (var PhotoUrl in PictureUrlss)
                {
                    if (PhotoUrl != null)
                    {
                        string photoname = Guid.NewGuid() + PhotoUrl.FileName;
                        string rootFile = Path.Combine(_webHost.WebRootPath, "downloads");
                        string mainFile = Path.Combine(rootFile, photoname);
                        using FileStream stream = new(mainFile, FileMode.Create);
                        PhotoUrl.CopyTo(stream);
                        Picture pic = new Picture() { Url = "/downloads/" + photoname };
                        _pictureManager.AddPicture(pic);
                        product.ProductPictures.Add(new ProductPicture() { ProductId=product.Id,PictureId=pic.Id});

                    }
                }
                int picFirstId = product.ProductPictures.First().PictureId;
                product.CoverPhotoId = product.ProductPictures != null ?
                    product.ProductPictures[
                        thumbnailPictureId.HasValue ?
                        thumbnailPictureId.Value :
                        0].PictureId
                        : null;
                _productManager.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        // GET: AlzzoniAdmin/Products/Edit/5
        public IActionResult Edit(int? id)
        {
            ViewBag.catList = _categoryManager.GetCategories();
            if (id == null)
                return NotFound();
            var selectedProduct = _productManager.GetById(id.Value);
            if (selectedProduct == null)
                return NotFound();
            return View(selectedProduct);
        }

        // POST: AlzzoniAdmin/Products/Edit/5
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product, IFormFile[] PictureUrlss,string oldPicture, string? removePicturesIds,int? thumbnailPictureId)
        {
            if (id != product.Id)
                return NotFound();
            ViewBag.catList = _categoryManager.GetCategories();
            if (ModelState.IsValid)
            {
                try
                {
                    List<int> rmvPicIds=new List<int>();
                    if(removePicturesIds != null)
                    {
                          rmvPicIds = removePicturesIds.Split("-")
                                            .Select(x => int.Parse(x)).ToList();
                        _pictureManager.RemovePicture(rmvPicIds);
                        product.ProductPictures = new List<ProductPicture>();
                        product.ProductPictures = product.ProductPictures.Where(x => !rmvPicIds.Contains(x.PictureId)).ToList();
                    }
                   
                    List<int> oldPictureIds = oldPicture.Split("-")
                      .Select(x => int.Parse(x))
                       .Where(x => !rmvPicIds.Contains(x)).ToList();
                   
                    var oldPicturewithoutRemove = _pictureManager.GetProductIds(oldPictureIds);
                    product.ProductPictures = oldPicturewithoutRemove.Count > 0 ? oldPicturewithoutRemove : new List<ProductPicture>();
                    foreach (var PhotoUrl in PictureUrlss)
                    {
                        if (PhotoUrl != null)
                        {
                            string photoname = Guid.NewGuid() + PhotoUrl.FileName;
                            string rootFile = Path.Combine(_webHost.WebRootPath, "downloads");
                            string mainFile = Path.Combine(rootFile, photoname);
                            using FileStream stream = new(mainFile, FileMode.Create);
                            PhotoUrl.CopyTo(stream);
                            Picture pic = new Picture() { Url = "/downloads/" + photoname };
                            _pictureManager.AddPicture(pic);
                            product.ProductPictures.Add(new ProductPicture() { ProductId = product.Id, PictureId = pic.Id });

                        }
                    }
                    int picFirstId = product.ProductPictures.First().PictureId;
                    product.CoverPhotoId = product.ProductPictures != null ?
                        product.ProductPictures[
                            thumbnailPictureId.HasValue ?
                            thumbnailPictureId.Value :
                            0].PictureId
                            : null;

                    _productManager.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productManager.ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: AlzzoniAdmin/Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var selectedProduct=_productManager.GetById(id.Value);
            if (selectedProduct == null)
                return NotFound();
            return View(selectedProduct);
        }

        // POST: AlzzoniAdmin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var selectedProduct = _productManager.GetById(id);
           _productManager.Delete(selectedProduct);
            return RedirectToAction(nameof(Index));
        }

     
    }
}
