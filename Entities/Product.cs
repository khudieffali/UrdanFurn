using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public class Product : BaseEntity
    {
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(400)")]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public ushort InStock { get; set; }
        public int? CoverPhotoId { get; set; }
        [MaxLength(150)]
        public string? SKU { get; set; }
        [MaxLength(200)]
        public string Barcode { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSlider { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category  { get; set; }
        public virtual List<ProductPicture>? ProductPictures { get; set; }

    }
}
