namespace Entities
{
    public class BlogCategory : BaseEntity
    {
        public string BlogCategoryName { get; set; }
        public string? BlogCategoryIcon { get; set; }
        public bool IsDeleted { get; set; }
    }
}
