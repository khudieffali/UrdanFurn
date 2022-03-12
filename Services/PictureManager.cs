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
    public class PictureManager
    {
        private readonly ApplicationDbContext _context;

        public PictureManager(ApplicationDbContext context)
        {
            _context = context;
        }
     
       public void AddPicture(Picture picture)
        {
            _context.Add(picture);
            _context.SaveChanges();
        }
        public void RemovePicture(List<int> picIds)
        {
            var oldPicture=GetProductIds(picIds);
            _context.ProdductPictures.RemoveRange(oldPicture);
            _context.SaveChanges();
        }
        public List<ProductPicture> GetProductIds(List<int> picIds)
        {
            return _context.ProdductPictures.Include(x=>x.Picture).Where(x=>picIds.Contains(x.PictureId)).ToList();
        }
    }
}
