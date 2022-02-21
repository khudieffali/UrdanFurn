using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogManager _blogManager;

        public BlogController(BlogManager blogManager)
        {
            _blogManager = blogManager;
        }
        public IActionResult Blog()
        {
            BlogVm vm = new()
            {
                BlogList = _blogManager.GetBlogs()
            };
            if(vm==null) return NotFound();
            return View(vm);
        }

        public IActionResult BlogDetails(int? id)
        {
            if(id==null) return NotFound();
            var selectedBlog=_blogManager.GetBlogById(id.Value);
            if(selectedBlog==null) return NotFound();
            BlogVm vm = new()
            {
                Blog = selectedBlog,
            };
            return View(vm);
        }
    }
}
