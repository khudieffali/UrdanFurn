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
    public class ProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.Include(x=>x.ProductPictures).ThenInclude(x=>x.Picture).Include(x=>x.Category).Where(x=>!x.IsDeleted).ToList();
        }
        public Product GetById(int id)
        {
            var selectedProduct=_context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Where(x=>!x.IsDeleted).Include(x=>x.Category).FirstOrDefault(x=>x.Id==id);
            return selectedProduct;
        }
        public List<Product>? GetByIds(IEnumerable<int> ids)
        {
            var selectedProduct = _context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Include(x => x.Category).Where(x => ids.Contains(x.Id) && !x.IsDeleted).ToList();
            return selectedProduct;
        }
        public List<Product> GetSliders()
        {

            return _context.Products.Include(x => x.ProductPictures).ThenInclude(x => x.Picture).Include(x => x.Category).Where(x => x.IsSlider && !x.IsDeleted).ToList(); 


        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            product.PublishDate = DateTime.Now;
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            product.ModifiedOn = DateTime.Now;  
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            product.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
