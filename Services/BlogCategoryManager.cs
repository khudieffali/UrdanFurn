using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BlogCategoryManager
    {
        private readonly ApplicationDbContext _context;

        public BlogCategoryManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<BlogCategory>> GetBlogCategories()
        {
            return _context.BlogCategories.Where(x=>!x.IsDeleted).ToList();
        }
        public async Task<BlogCategory> GetBlogCategoryId(int id)
        {
            return _context.BlogCategories.Where(x=>x.Id == id).FirstOrDefault();
        }
        public void Add(BlogCategory blogCategory)
        {
            _context.BlogCategories.Add(blogCategory);
            _context.SaveChanges();
        }
        public void Update(BlogCategory blogCategory)
        {
            _context.BlogCategories.Update(blogCategory);
            _context.SaveChanges();
        }
        public  void Delete(BlogCategory blogCategory)
        {
            blogCategory.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool BlogCategoryExists(int id)
        {
            return _context.BlogCategories.Any(e => e.Id == id);
        }
    }
}
