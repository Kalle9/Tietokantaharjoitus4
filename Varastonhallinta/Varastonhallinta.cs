using Microsoft.EntityFrameworkCore;

namespace Varastonhallinta
{
    public class Varastonhallinta : DbContext
    {
        // Tietokannan taulu
        public DbSet<Tuote>? Tuotteet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            string connection = "Data Source=.;" +
                                "Initial Catalog=Varastonhallinta;" +
                                "User Id=sa;" + 
                                "Password=testi12345;" +
                                "MultipleActiveResultSets=true;" + 
                                "TrustServerCertificate=true"; 
            optionsBuilder.UseSqlServer(connection); 
        }
    }
}

