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
        public void Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            blog.BlogDate = DateTime.Now;
            _context.SaveChanges();
        }
        public void Update(Blog blog)
        {
            _context.Blogs.Update(blog);
            _context.SaveChanges();
        }
        public void Delete(Blog blog)
        {
            blog.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
