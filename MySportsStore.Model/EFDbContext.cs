using System.Data.Entity;

namespace MySportsStore.Model
{
    public class EfDbContext : DbContext 
    {
        public EfDbContext()
            : base("conn") 
        {
            Database.SetInitializer(new EfDbInitializer());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<EfDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<EfDbContext>());           
        }
         public DbSet<Product> Products { get; set; }

    }
}