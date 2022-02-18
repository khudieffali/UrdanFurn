using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BlogManager
    {
        private readonly ApplicationDbContext _context;

        public BlogManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Blog> GetBlogs()
        {
            return _context.Blogs.Include(x=>x.BlogCategory).Where(x=>!x.IsDeleted).ToList();
        }
        public Blog GetBlogById(int id)
        {
            var selectedBlog=_context.Blogs.Include(x=>x.BlogCategory).Where(x=>!x.IsDeleted).FirstOrDefault(x=>x.Id == id);
            return selectedBlog;
        }
    }
}
