using ITSAIntermediate_VelaphiMhlanga.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ITSAIntermediate_VelaphiMhlanga.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RecUser> RecUsers { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<CompanyDetail> CompanyDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
