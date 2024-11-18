using EF_Core_Project.Contexts;
using EF_Core_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project.Extra_Methods
{
    public class AddProduct
    {
        public void ShowAllProducts(AppDbContext db)
        {
            
                if (!db.Products.Any())
                {
                    Product product1 = new Product { Name = "Product1", Price = 15 };
                    Product product2 = new Product { Name = "Product2", Price = 20 };
                    Product product3 = new Product { Name = "Product3", Price = 18 };
                    Product product4 = new Product { Name = "Product4", Price = 22 };
                    Product product5 = new Product { Name = "Product5", Price = 36 };

                    db.Products.Add(product1);
                    db.Products.Add(product2);
                    db.Products.Add(product3);
                    db.Products.Add(product4);
                    db.Products.Add(product5);

                    db.SaveChanges();
                }

                
                List<Product> products = [];

                
                 products=db.Products.ToList();
                
                

                foreach (Product product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
                }
            
        }
    }
}
