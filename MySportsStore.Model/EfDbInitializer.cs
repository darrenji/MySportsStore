
using System.Collections.Generic;
using System.Data.Entity;

namespace MySportsStore.Model
{
    public class EfDbInitializer : CreateDatabaseIfNotExists<EfDbContext>
    {
        protected override void Seed(EfDbContext context)
        {
            IList<Product> defaultProducts = new List<Product>();
            defaultProducts.Add(new Product(){Name = "Kayak", Description = "A boat for one person", Category = "Watersports", Price = 275.00M});
            defaultProducts.Add(new Product() { Name = "Lifejacket", Description = "Protective and fashionable", Category = "Watersports", Price = 48.95M });
            defaultProducts.Add(new Product() { Name = "Soccer ball", Description = "FIFA-approved size and weight", Category = "Soccer", Price = 19.50M });
            defaultProducts.Add(new Product() { Name = "Corneer flags", Description = "Giving your playing field that professional touch", Category = "Soccer", Price = 34.95M });
            defaultProducts.Add(new Product() { Name = "Stadium", Description = "Flat-packed 35,000-seat stadium", Category = "Soccer", Price = 79500.00M });
            defaultProducts.Add(new Product() { Name = "Thinking cap", Description = "Improve your brain efficiency by 75%", Category = "Chess", Price = 16.00M });
            defaultProducts.Add(new Product() { Name = "Unsteady Chair", Description = "Secretly give your opponent a disadvantage", Category = "Chess", Price = 29.95M });
            defaultProducts.Add(new Product() { Name = "Human Chess", Description = "A fun game for the whole family", Category = "Chess", Price = 75.00M });
            defaultProducts.Add(new Product() { Name = "Bling-bling King", Description = "Gold-plated, diamond-studded King", Category = "Chess", Price = 1200.00M });

            foreach (Product p in defaultProducts)
            {
                context.Products.Add(p);
            }
            base.Seed(context);
        }
    }
}