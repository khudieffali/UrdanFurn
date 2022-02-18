using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     public class CategoryManager
    {
        private readonly ApplicationDbContext _context;

        public CategoryManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.Where(x=>!x.IsDeleted).ToList();
        }
        public Category GetById(int id)
        {
            var selectedCategory=_context.Categories.Where(x=>!x.IsDeleted).FirstOrDefault(c => c.Id == id);
            return selectedCategory;
        }
        public void Add(Category cat) 
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
        }
        public void Update(Category cat)
        {
            _context.Categories.Update(cat);
            _context.SaveChanges();
        }
        public void Delete(Category cat)
        {
            cat.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(x=>x.Id == id);
        }
       
    }
}
