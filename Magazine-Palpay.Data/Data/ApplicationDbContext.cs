using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Magazine_Palpay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //private readonly PersistenceSettings _persistenceOptions;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ads> Ads { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
        public DbSet<MagazineSetting> MagazineSetting { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostPhoto> PostPhoto { get; set; }
        public DbSet<PostType> PostType { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<LastNews> LastNews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyMagazineConfiguration(_persistenceOptions);
        }
    }
}
