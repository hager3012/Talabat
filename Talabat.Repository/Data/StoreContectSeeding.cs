using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContectSeeding
    {
        public static async Task SeedAsnc(StoreDbContext dbContext)
        {
            if (dbContext.ProductBrands.Count()==0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);

                if (brands?.Count > 0)
                {
                    brands = brands.Select(B => new Brand()
                    {
                        Name = B.Name
                    }).ToList();
                    foreach (var item in brands)
                    {
                        dbContext.Set<Brand>().Add(item);
                    }
                }
                await dbContext.SaveChangesAsync(); 
            }

            if (dbContext.ProductCategories.Count() == 0)
            {
                var CategoriesProduct = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
                var Categories=JsonSerializer.Deserialize<List<Category>>(CategoriesProduct);
                if (Categories?.Count()>0)
                {
                    Categories = Categories.Select(C => new Category() { Name = C.Name }).ToList();
                    foreach (var item in Categories)
                    {
                        dbContext.Set<Category>().Add(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if(dbContext.Products.Count()==0)
            {
                var Products = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                var Product = JsonSerializer.Deserialize<List<product>>(Products);
                if (Products?.Count()>0)
                {
                    foreach (var item in Product)
                    {
                        dbContext.Set<product>().Add(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (dbContext.DeliveryMethods.Count() == 0)
            {
                var DeliveryMethods = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
                var DeliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethods);
                if (DeliveryMethods?.Count() > 0)
                {
                    foreach (var item in DeliveryMethod)
                    {
                        dbContext.Set<DeliveryMethod>().Add(item);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
