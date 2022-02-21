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
        public List<BlogCategory> GetBlogCategories()
        {
            return _context.BlogCategories.Where(x=>!x.IsDeleted).ToList();
        }
    }
}
