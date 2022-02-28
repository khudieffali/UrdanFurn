#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using Services;

namespace Web.Areas.AlzzoniAdmin.Controllers
{
    [Area("AlzzoniAdmin")]
    public class BlogCategoriesController : Controller
    {
        private readonly BlogCategoryManager _blogCategoryManager;
        private readonly IWebHostEnvironment _webHost;

        public BlogCategoriesController(BlogCategoryManager blogCategoryManager, IWebHostEnvironment webHost)
        {
            _blogCategoryManager = blogCategoryManager;
            _webHost = webHost;
        }

        // GET: AlzzoniAdmin/BlogCategories
        public async Task<IActionResult> Index()
        {
            return View(await _blogCategoryManager.GetBlogCategories());
        }

        // GET: AlzzoniAdmin/BlogCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var blogCategory = await _blogCategoryManager.GetBlogCategoryId(id.Value);
            if (blogCategory == null)
                return NotFound();
            return View(blogCategory);
        }

        // GET: AlzzoniAdmin/BlogCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AlzzoniAdmin/BlogCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BlogCategoryName,BlogCategoryIcon,IsDeleted,Id")] BlogCategory blogCategory,IFormFile BlogCategoryIcon)
        {
            if (ModelState.IsValid)
            {
                if(BlogCategoryIcon != null)
                {
                    string fileName=Guid.NewGuid()+ BlogCategoryIcon.FileName;
                    string rootFile = Path.Combine(_webHost.WebRootPath, "downloads");
                    string mainFile=Path.Combine(rootFile,fileName);
                    using FileStream str=new FileStream(mainFile, FileMode.Create);
                    BlogCategoryIcon.CopyTo(str);
                    blogCategory.BlogCategoryIcon = "/downloads/" + fileName;
                }
                _blogCategoryManager.Add(blogCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategory);
        }

        // GET: AlzzoniAdmin/BlogCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var blogCategory = await _blogCategoryManager.GetBlogCategoryId(id.Value);
            if (blogCategory == null)
                return NotFound();
            return View(blogCategory);
        }

        // POST: AlzzoniAdmin/BlogCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BlogCategoryName,BlogCategoryIcon,IsDeleted,Id")] BlogCategory blogCategory,IFormFile newBlogCatPhoto)
        {
            if (id != blogCategory.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (newBlogCatPhoto != null)
                    {
                        string fileName = Guid.NewGuid() + newBlogCatPhoto.FileName;
                        string rootFile = Path.Combine(_webHost.WebRootPath, "downloads");
                        string mainFile = Path.Combine(rootFile, fileName);
                        using FileStream str = new FileStream(mainFile, FileMode.Create);
                        newBlogCatPhoto.CopyTo(str);
                        blogCategory.BlogCategoryIcon = "/downloads/" + fileName;
                    }
                    _blogCategoryManager.Update(blogCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_blogCategoryManager.BlogCategoryExists(blogCategory.Id))
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
            return View(blogCategory);
        }

        // GET: AlzzoniAdmin/BlogCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var blogCategory = await _blogCategoryManager.GetBlogCategoryId(id.Value);
            if (blogCategory == null)
                return NotFound();
            return View(blogCategory);
        }

        // POST: AlzzoniAdmin/BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCategory = await _blogCategoryManager.GetBlogCategoryId(id);
            _blogCategoryManager.Delete(blogCategory);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
