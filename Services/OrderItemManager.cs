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
    public class OrderItemManager
    {
        private readonly ApplicationDbContext _context;

        public OrderItemManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<OrderItem> GetAll()
        {
            var selectedOrder= _context.OrderItems
                .Include(x => x.Product).ThenInclude(x => x.ProductPictures)
                .ThenInclude(x=>x.Picture)
                .Include(x => x.Order).OrderByDescending(x => x.Order.OrderDate).ToList();
            return selectedOrder;
        }
    }
}
