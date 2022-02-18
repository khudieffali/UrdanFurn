using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Category:BaseEntity
    {
        [Column(TypeName = "nvarchar(100)")]
        public string CategoryName { get; set; }
        [MaxLength(800)]
        public string? IconUrl { get; set; }
        public bool IsDeleted { get; set; }
    }

}
