using DataAccess;
using Entities;
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
    }
}
