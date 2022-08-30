using Magazine_Palpay.Web.Extensions;
using Magazine_Palpay.Web.IdentityModels;
using Magazine_Palpay.Web.Models;
using Magazine_Palpay.Web.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Magazine_Palpay.Web
{
    /*
      public sealed class IdentityDbContext : IdentityDbContext<FluentUser, FluentRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, FluentRoleClaim, IdentityUserToken<string>>,
        IIdentityDbContext,
        IModuleDbContext
     */
    public class ApplicationDbContext : IdentityDbContext<FluentUser, FluentRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, FluentRoleClaim, IdentityUserToken<string>>
    {
        private readonly PersistenceSettings _persistenceOptions;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<PersistenceSettings> persistenceOptions)
            : base(options)
        {
            _persistenceOptions = persistenceOptions.Value;
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
        public DbSet<FluentUser> FluentUser { get; set; }
        public DbSet<FluentRole> FluentRole { get; set; }
        public DbSet<FluentRoleClaim> FluentRoleClaim { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyMagazineConfiguration(_persistenceOptions);
        }
    }
}
