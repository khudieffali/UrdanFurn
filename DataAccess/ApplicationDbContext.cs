using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProductPicture> ProdductPictures { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }





        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<User>().HasData(
                new User
                {
                    Id ="1023",
                    Email = "ali.xudiyev98@gmail.com",
                    FirstName ="Əli",
                    LastName ="Xudiyev",
                    Address ="Azərbaycan, Cəlilabad, Alar",
                    PhoneNumber ="+994-55-305-32-32",
                    UserName ="ali.xudiyev98@gmail.com",
                    ConcurrencyStamp ="sdchsuicmkms",
                    EmailConfirmed =true,
                    NormalizedEmail="ALI.XUDIYEV98@GMAIL.COM",
                    PhoneNumberConfirmed =true, 
                    LockoutEnabled=true,
                    NormalizedUserName= "ALI.XUDIYEV98@GMAIL.COM",
                    TwoFactorEnabled=true,
                    AccessFailedCount=1,
                    LockoutEnd=DateTime.Now,
                    PasswordHash=null,
                    SecurityStamp=null,
                });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id ="abs21",
                    Name ="Admin",
                    NormalizedName="ADMIN",
                    ConcurrencyStamp="sdxjwdxjmskm"
                }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId= "abs21",
                    UserId="1023"
                });
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Yataq Dəsti",
                    IconUrl = "sdws8485xd48sdx8s4x5xd",
                    IsDeleted = false,
                }
                );
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name="Sultan",
                    Description = "Keyfiyyətli və dözümlü",
                    Price = 1700,
                    InStock = 5,
                    Discount= 0,
                    //PhotoUrl = "dwswdkmsidjsimc65265dcs",
                    PublishDate = DateTime.Now,
                    IsDeleted=false,
                    IsSlider=false,
                    SKU="dscscsdecfedcsf4845cd",
                    Barcode="4726628996236",
                    CategoryId=1,
                });
        }
    }
}