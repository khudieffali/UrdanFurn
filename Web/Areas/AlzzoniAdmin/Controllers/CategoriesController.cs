using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.AlzzoniAdmin.Controllers
{
    [Area("AlzzoniAdmin")]
    [Authorize(Roles ="Admin")]
    public class CategoriesController : Controller
    {
        private readonly CategoryManager _categoryManager;

        public CategoriesController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var selectedCategory = _categoryManager.GetCategories();
            return View(selectedCategory);
        }
        public IActionResult Details(int? id)
        {
            if(id == null) return NotFound();
            var selectedCategory=_categoryManager.GetById(id.Value);
            if(selectedCategory == null) return NotFound();
            return View(selectedCategory);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                _categoryManager.Add(cat);
                return RedirectToAction("Index");
            }

            return View(cat);
        }
        public IActionResult Edit(int? id)
        {
            if(id==null) return NotFound();
            var selectedCategory = _categoryManager.GetById(id.Value);
            if(selectedCategory==null) return NotFound();   
            return View(selectedCategory);
        }
        [HttpPost]
        public IActionResult Edit(int? id,Category category)
        {
            if (id != category.Id) return NotFound();
            if (ModelState.IsValid) 
            {
                try
                {
                    _categoryManager.Update(category);
                }
                catch
                {
                    if (!_categoryManager.CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var selectedCategory = _categoryManager.GetById(id.Value);
            if(selectedCategory == null) return NotFound();
            return View(selectedCategory);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var selectedCategory=_categoryManager.GetById(id);
            _categoryManager.Delete(selectedCategory);
            return RedirectToAction("Index");
        }

    }
}
