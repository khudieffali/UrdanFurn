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
    public class BlogsController : Controller
    {
        private readonly BlogManager _blogManager;
        private readonly IWebHostEnvironment _webHost;
        private readonly BlogCategoryManager _blogCategoryManager;
        public BlogsController(BlogManager blogManager, IWebHostEnvironment webHost, BlogCategoryManager blogCategoryManager)
        {
            _blogManager = blogManager;
            _webHost = webHost;
            _blogCategoryManager = blogCategoryManager;
        }



        // GET: AlzzoniAdmin/Blogs
        public IActionResult Index()
        {
            var selectedBlog=_blogManager.GetBlogs();
            if(selectedBlog==null) return NotFound();
            return View(selectedBlog);
        }

        // GET: AlzzoniAdmin/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var selectedBlog = _blogManager.GetBlogById(id.Value);
            if (selectedBlog == null)
                return NotFound();

            return View(selectedBlog);
        }

        // GET: AlzzoniAdmin/Blogs/Create
        public IActionResult Create()
        {
            ViewBag.BlogCatList = _blogCategoryManager.GetBlogCategories();
            return View();
        }

        // POST: AlzzoniAdmin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BlogTitle,BlogPhoto,Description,BlogDate,BlogCategoryID")] Blog blog,IFormFile BlogPhoto)
        {
            ViewBag.BlogCatList = _blogCategoryManager.GetBlogCategories();
            if (!ModelState.IsValid)
            {
                if(BlogPhoto != null)
                {
                    string fileName = Guid.NewGuid() + BlogPhoto.FileName;
                    string rootFile = Path.Combine(_webHost.WebRootPath, "downloads");
                    string mainFile = Path.Combine(rootFile, fileName);
                    using FileStream str = new(mainFile, FileMode.Create);
                    BlogPhoto.CopyTo(str);
                    blog.BlogPhoto = "/downloads/" + fileName;
                }  
               _blogManager.Add(blog);
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: AlzzoniAdmin/Blogs/Edit/5
        public IActionResult Edit(int? id)
        {
            ViewBag.BlogCatList = _blogCategoryManager.GetBlogCategories();
            if (id == null)
                return NotFound();
            var selectedBlog=_blogManager.GetBlogById(id.Value);
            if (selectedBlog == null)
                return NotFound();
            return View(selectedBlog);
        }

        // POST: AlzzoniAdmin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BlogTitle,BlogPhoto,Description,BlogDate,IsDeleted,BlogCategoryID,Id")] Blog blog,IFormFile newBlogPhoto)
        {
            ViewBag.BlogCatList = _blogCategoryManager.GetBlogCategories();
            if (id != blog.Id)
                return NotFound();
            if (!ModelState.IsValid)
            {
                try
                {
                    if (newBlogPhoto != null)
                    {
                        string fileName = Guid.NewGuid() + newBlogPhoto.FileName;
                        string rootFile = Path.Combine(_webHost.WebRootPath, "downloads");
                        string mainFile = Path.Combine(rootFile, fileName);
                        using FileStream str = new(mainFile, FileMode.Create);
                        newBlogPhoto.CopyTo(str);
                        blog.BlogPhoto = "/downloads/" + fileName;
                    }
                    _blogManager.Update(blog);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_blogManager.BlogExists(blog.Id))
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
            return View(blog);
        }

        // GET: AlzzoniAdmin/Blogs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var blog = _blogManager.GetBlogById(id.Value);
            if (blog == null)
                return NotFound();

            return View(blog);
        }

        // POST: AlzzoniAdmin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var blog = _blogManager.GetBlogById(id);
            _blogManager.Delete(blog);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
