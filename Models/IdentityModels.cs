using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public void User()
        {
            this.CarAdvs = new HashSet<CarAdv>();
            this.JobAdvs = new HashSet<JobAdv>();
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Gid { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Mobile1 { get; set; }
        public string UPhoto { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarAdv> CarAdvs { get; set; }
        public virtual City City { get; set; }
        public virtual Governrate Governrate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobAdv> JobAdvs { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<AccessoriesAdv> AccessoriesAdv { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<CarAdv> CarAdv { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Governrate> Governrate { get; set; }
        public DbSet<JobAdv> JobAdv { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<TechJob> TechJob { get; set; }
        public DbSet<Accessory> Accessory { get; set; }

        public DbSet<CarModel> CarModel { get; set; }

        public DbSet<CarPhoto> CarPhoto { get; set; }

        public DbSet<Contactus> Contactus { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}