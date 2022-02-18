using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Blog:BaseEntity
    {
        public string  BlogTitle { get; set; }
        public string BlogPhoto { get; set; }
        public string Description { get; set; }
        public DateTime? BlogDate { get; set; }
        public bool IsDeleted { get; set; }
        public int BlogCategoryID { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }
      
    }
}
