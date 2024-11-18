using EF_Core_Project.Contexts;
using EF_Core_Project.Exceptions;
using EF_Core_Project.Extra_Methods;
using EF_Core_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext context = new AppDbContext())
            {
                bool f = false;

                do
                {
                    Console.WriteLine();
                    Console.WriteLine("1.Register  2.Login  0.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Surname: ");
                            string surname = Console.ReadLine();

                            Console.Write("Enter Username: ");
                            string username = Console.ReadLine();

                            Console.Write("Enter Password: ");
                            string password = Console.ReadLine();

                           
                                context.Users.Add(new User
                                {
                                    Name = name,
                                    Surname = surname,
                                    Username = username,
                                    Password = password

                                });
                                context.SaveChanges();
                                Console.WriteLine("Successfully created.");
                            
                            break;

                        case 2:
                            Console.Write("UserName: ");
                            string username1 = Console.ReadLine();
                            Console.Write("Password: ");
                            string password1 = Console.ReadLine();

                            try
                            {
                                
                                    var userExist = context.Users.Any(x => x.Username == username1 && x.Password == password1);
                                    if (!userExist)
                                    {
                                        throw new UserNotFoundException("UserName or Password is incorrect.");
                                    }
                                    else
                                    {
                                        bool f1 = false;
                                        do
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("1. Look Products  2.Look Basket  0.Go back");

                                            choice = int.Parse(Console.ReadLine());
                                            switch (choice)
                                            {
                                                case 1:

                                                    bool f2 = false;
                                                    AddProduct Addproduct = new AddProduct();
                                                    do
                                                    {
                                                        Addproduct.ShowAllProducts(context);

                                                        Console.WriteLine();
                                                        Console.WriteLine("1.Add Product to Basket,  2.Go Back ");

                                                        choice = int.Parse(Console.ReadLine());
                                                        Console.WriteLine();



                                                        switch (choice)
                                                        {
                                                            case 1:

                                                                Console.Write("Choose one Product by Id: ");
                                                                int input = int.Parse(Console.ReadLine());

                                                                try
                                                                {

                                                                    Product product = context.Products.FirstOrDefault(x => x.Id == input);
                                                                    if (product != null)
                                                                    {
                                                                        User user = context.Users.FirstOrDefault(x => x.Username == username1);

                                                                        Basket basket = new Basket
                                                                        {
                                                                            UserId = user.Id,
                                                                            ProductId = product.Id
                                                                        };

                                                                        context.Baskets.Add(basket);
                                                                        context.SaveChanges();
                                                                        Console.WriteLine($"Product: {product.Name} added to basket.");
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new ProductNotFoundException("Product isn't exist.");
                                                                    }


                                                                }
                                                                catch (ProductNotFoundException ex)
                                                                {
                                                                    Console.WriteLine(ex.Message);
                                                                }
                                                                break;

                                                            case 2:
                                                                f2 = true;
                                                                break;
                                                        }



                                                    } while (!f2);



                                                    break;

                                                case 2:
                                                    User newuser= context.Users.FirstOrDefault(x=>x.Username == username1);
                                                    var newbasket = context.Baskets
                                                    .Where(b => b.UserId == newuser.Id)
                                                    .Include(b => b.Product)  
                                                    .ToList();

                                                    if (newbasket.Any())
                                                    {
                                                        foreach(var b in newbasket)
                                                        {
                                                            Console.WriteLine($"Id:{b.Product.Id}, Name:{b.Product.Name}, Price:{b.Product.Price}");
                                                        }



                                                        Console.WriteLine();
                                                        Console.Write("Enter Product Id that you want to remove from your Basket: ");
                                                        int Id = int.Parse(Console.ReadLine());

                                                        var removeproduct = context.Baskets
                                                            .FirstOrDefault(b => b.UserId == newuser.Id && b.ProductId == Id);


                                                        if (removeproduct != null)
                                                        {
                                                            context.Baskets.Remove(removeproduct);
                                                            context.SaveChanges();
                                                            Console.WriteLine($"The Product: {Id} is removed from Basket");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Product not found in your Basket.");
                                                        }


                                                    }


                                                    else
                                                    {
                                                        Console.WriteLine("Your Basket is empty.");
                                                    }

                                                    
                                                    break;

                                                case 0:
                                                    f1 = true;
                                                    break;
                                            }

                                        } while (!f1);

                                    }
                                

                            }
                            catch (UserNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);

                            }




                            break;

                        case 0:
                            f = true;
                            break;
                    }
                } while (!f);

            }
           
        }
    }
}
