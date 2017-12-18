using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Start
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //ResetDatabase();
            //SeedDatabaseJson();
            //ExecuteQueriesJsonAndWriteFiles();

            //SeedDatabaseXml();
            //ExecuteQueriesXmlAndWriteFiles();
        }

        private static void ExecuteQueriesXmlAndWriteFiles()
        {
            //ProductsInRangeXml();
            //SoldProductsXml();
            //CategoriesByProductCountXml();
            //UsersAndProductsXml();

        }

        private static void UsersAndProductsXml()
        {
            using (var db = new ProductsShopDb())
            {
                var users = db.Users
                    .Include(x => x.ProductsSold)
                    .Where(x => x.ProductsSold.Count > 0)
                    .OrderByDescending(x => x.ProductsSold.Count)
                    .ThenBy(x => x.LastName)
                    .Select(x => new
                    {
                        firstName = x.FirstName,
                        lastName = x.LastName,
                        age = x.Age,
                        proucts = x.ProductsSold.Select(a => new
                        {
                            a.Name,
                            a.Price
                        })

                    }).ToArray();

                var xml = new XDocument(new XElement("users"));

                foreach (var u in users)
                {
                    var user = new XElement("user",
                        new XElement("sold-products", new XAttribute("coute", u.proucts.Count())));

                    if (u.firstName != null)
                    {
                        user.Add(new XAttribute("first-name",u.firstName));
                    }

                    if (u.lastName != null)
                    {
                        user.Add(new XAttribute("last-name", u.lastName));
                    }

                    if (u.age != null)
                    {
                        user.Add(new XAttribute("age", u.age));
                    }

                    foreach (var p in u.proucts)
                    {
                        var product = new XElement("product",
                            new XAttribute("name", p.Name),
                            new XAttribute("price", p.Price));

                        user.Element("sold-products").Add(product);
                    }

                    xml.Root.Add(user);
                }

                xml.Save("UserProducts.xml");
            }
        }

        private static void CategoriesByProductCountXml()
        {
            using (var db = new ProductsShopDb())
            {
                var categories = db.Categories
                    .Include(x => x.CategoryProducts)
                    .ThenInclude(x => x.Product)
                    .OrderBy(x => x.CategoryProducts.Count)
                    .Select(a => new
                    {
                        name = a.Name,
                        productsCount = a.CategoryProducts.Count,
                        avarage = a.CategoryProducts.Average(x => x.Product.Price),
                        sum = a.CategoryProducts.Sum(x => x.Product.Price)
                    }).ToArray();

                var xml = new XDocument(new XElement("catecories"));

                foreach (var c in categories)
                {
                    xml.Root.Add(new XElement("category", new XAttribute("name", c.name),
                        new XElement("products-count", c.productsCount),
                        new XElement("average-price", c.avarage),
                        new XElement("total-revenue", c.sum)));
                }
                xml.Save("CategoriesByProduct.xml");
            }
        }

        private static void SoldProductsXml()
        {
            using (var db = new ProductsShopDb())
            {
                var users = db.Users
                    .Include(x => x.ProductsSold)
                    .Where(x => x.ProductsSold.Count > 0)
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(x => new
                    {
                        firstname = x.FirstName,
                        lastname = x.LastName,
                        soldProducts = x.ProductsSold.Select(e => new
                        {
                            name = e.Name,
                            price = e.Price
                        })
                    }).ToArray();

                var xml = new XDocument(new XElement("users"));

                foreach (var u in users)
                {
                    var usersToAdd =
                        new XElement("user",
                        new XAttribute("first-name", u.firstname ?? "No Name"),
                        new XAttribute("last-name", u.lastname),
                        new XElement("sold-products"));

                    foreach (var p in u.soldProducts)
                    {
                        var child = new XElement("product",
                            new XElement("name", p.name),
                            new XElement("price", p.price));

                        usersToAdd.Element("sold-products").Add(child);
                    }

                    xml.Root.Add(usersToAdd);
                }

                xml.Save("SoldProducts.xml");
            }
        }

        private static void ProductsInRangeXml()
        {
            using (var db = new ProductsShopDb())
            {

                var products = db.Products
                    .Include(x => x.Buyer)
                    .Where(p => p.Price >= 1000 && p.Price <= 2000)
                    .Where(b => b.BuyerId != null)
                    .OrderBy(p => p.Price)
                    .Select(x => new
                    {
                        name = x.Name,
                        price = x.Price,
                        buyer = $"{x.Buyer.FirstName} {x.Buyer.LastName}"
                    }).ToArray();

                XDocument xml = new XDocument(new XElement("products"));

                foreach (var p in products)
                {
                    xml.Root.Add(new XElement("product", new XAttribute("name", p.name), new XAttribute("price", p.price), new XAttribute("buyer", p.buyer)));
                }

                File.WriteAllText("ProductsInRange.xml", xml.ToString());
            }
        }

        private static void SeedDatabaseXml()
        {
            SeedUsersXml();
            SeedCategoriesXml();
            SeedProductsXml();
            SeedCategoryProductsXml();
        }

        private static void SeedCategoryProductsXml()
        {
            using (var db = new ProductsShopDb())
            {
                var products = db.Products.Select(x => x.Id).ToArray();
                var categories = db.Categories.Select(x => x.Id).ToArray();

                var result = new List<CategoryProduct>();

                var rnd = new Random();

                foreach (var c in categories)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var cat = new CategoryProduct()
                        {
                            CategoryId = c,
                            ProductId = rnd.Next(1, products.Length)
                        };

                        result.Add(cat);
                    }
                }

                db.CategoryProducts.AddRange(result);
                db.SaveChanges();
            }
        }

        private static void SeedProductsXml()
        {
            var read = File.ReadAllText("JsonAndXmlData/products.xml");
            var products = XDocument.Parse(read);

            var result = new List<Product>();

            var rnd = new Random();

            using (var db = new ProductsShopDb())
            {
                var usersId = db.Users.Select(x => x.Id).ToArray();

                var count = 1;
                foreach (var x in products.Root.Elements())
                {
                    var p = new Product();
                    p.Name = x.Element("name").Value;
                    p.Price = decimal.Parse(x.Element("price").Value);
                    p.SellerId = rnd.Next(1, usersId.Length);

                    if (count % 2 == 0)
                        p.BuyerId = rnd.Next(1, usersId.Length);

                    result.Add(p);

                    count++;
                }

                db.Products.AddRange(result);
                db.SaveChanges();
            }
        }

        private static void SeedCategoriesXml()
        {
            var read = File.ReadAllText("JsonAndXmlData/categories.xml");

            var xml = XDocument.Parse(read);

            var categories = new List<Category>();

            foreach (var x in xml.Root.Elements())
            {
                var name = x.Element("name").Value;

                categories.Add(new Category()
                {
                    Name = name
                });
            }

            var db = new ProductsShopDb();

            db.Categories.AddRange(categories);
            db.SaveChanges();
        }

        private static void SeedUsersXml()
        {
            var read = File.ReadAllText("JsonAndXmlData/users.xml");

            var xml = XDocument.Parse(read);

            var users = new List<User>();

            foreach (var x in xml.Root.Elements())
            {
                var firstname = x.Attribute("firstName")?.Value;
                var lastname = x.Attribute("lastName").Value;

                int? age = null;

                if (x.Attribute("age") != null)
                {
                    age = int.Parse(x.Attribute("age").Value);
                }

                var user = new User()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Age = age
                };

                users.Add(user);
            }

            var db = new ProductsShopDb();

            db.Users.AddRange(users);
            db.SaveChanges();
        }

        private static void ExecuteQueriesJsonAndWriteFiles()
        {
            ProductsInRange();
            SoldProducts();
            CategoriesByProductCount();
            UsersAndProducts();
        }

        private static void SeedDatabaseJson()
        {
            SeedUsers();
            SeedCategories();
            SeedProducts();
            SeedCategoryProducts();
        }

        private static void UsersAndProducts()
        {
            using (var db = new ProductsShopDb())
            {
                var users = db.Users
                    .Include(x => x.ProductsSold)
                    .Where(x => x.ProductsSold.Count > 0)
                    .OrderByDescending(x => x.ProductsSold.Count)
                    .ThenBy(x => x.LastName)
                    .Select(x => new
                    {
                        firstName = x.FirstName,
                        lastName = x.LastName,
                        age = x.Age,
                        soldProducts = new
                        {
                            count = x.ProductsSold.Count,
                            products = x.ProductsSold.Select(e => new
                            {
                                e.Name,
                                e.Price
                            })
                        }
                    });

                var result = new
                {
                    usersCount = users.Count(),
                    users
                };


                var parsedJson = JsonConvert.SerializeObject(result, Formatting.Indented);

                File.WriteAllText("UsersAndProducts.json", parsedJson);
            }
        }

        private static void CategoriesByProductCount()
        {
            using (var db = new ProductsShopDb())
            {
                var categories = db.Categories
                    .Include(x => x.CategoryProducts)
                    .ThenInclude(x => x.Product)
                    .OrderBy(x => x.Name)
                    .Select(x => new
                    {
                        x.Name,
                        productsCount = x.CategoryProducts.Count,
                        averagePrice = x.CategoryProducts.Select(e => e.Product.Price).Average(),
                        totalRevenue = x.CategoryProducts.Sum(p => p.Product.Price)
                    }).ToArray();

                var parsedJason = JsonConvert.SerializeObject(categories, Formatting.Indented);

                File.WriteAllText("CategoriesByProductCount.json", parsedJason);
            }
        }

        private static void SoldProducts()
        {
            using (var db = new ProductsShopDb())
            {
                var users = db.Users.Include(x => x.ProductsSold)
                    .ThenInclude(x => x.Buyer)
                    .Where(x => x.ProductsSold.Any(e => e.BuyerId != null))
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(x => new
                    {
                        x.FirstName,
                        x.LastName,
                        SoldProducts = x.ProductsSold.Select(a => new
                        {
                            a.Name,
                            a.Price,
                            BuyerFirstName = a.Buyer.FirstName,
                            buyerLastName = a.Buyer.LastName
                        })
                    }).ToArray();

                var parsedJson = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("SoldProducts.json", parsedJson);
            }
        }

        private static void ProductsInRange()
        {
            using (var db = new ProductsShopDb())
            {
                var products = db.Products.Where(x => x.Price >= 500 && x.Price <= 1000)
                    .OrderBy(x => x.Price)
                    .Select(x => new
                    {
                        ProductName = x.Name,
                        Price = x.Price,
                        Seller = $"{x.Seller.FirstName} {x.Seller.LastName}"
                    }).ToArray();

                var parsedJson = JsonConvert.SerializeObject(products, Formatting.Indented);

                File.WriteAllText("ProductsInRange.json", parsedJson);
            }
        }

        private static void SeedCategoryProducts()
        {
            using (var db = new ProductsShopDb())
            {
                var products = db.Products.ToArray();
                var categories = db.Categories.ToArray();

                var catecoryProducts = new List<CategoryProduct>();

                var rnd = new Random();

                foreach (var c in categories)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var cat = new CategoryProduct();
                        cat.Product = products[rnd.Next(0, products.Length - 1)];
                        cat.Category = c;
                        catecoryProducts.Add(cat);
                    }
                }

                db.CategoryProducts.AddRange(catecoryProducts);
                db.SaveChanges();
            }
        }

        private static void SeedCategories()
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(File.ReadAllText(@"JsonAndXmlData\categories.json"));

            using (var db = new ProductsShopDb())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
        }

        private static void SeedUsers()
        {
            var users = JsonConvert.DeserializeObject<User[]>(File.ReadAllText(@"JsonAndXmlData\users.json"));

            using (var db = new ProductsShopDb())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }

        private static void SeedProducts()
        {
            var products = JsonConvert.DeserializeObject<Product[]>(File.ReadAllText(@"JsonAndXmlData\products.json"));

            var rnd = new Random();

            using (var db = new ProductsShopDb())
            {
                var usersId = db.Users.Select(x => x.Id).ToArray();

                for (int i = 0; i < products.Length; i++)
                {
                    products[i].SellerId = rnd.Next(1, usersId.Length - 1);

                    if (i % 2 == 0)
                        products[i].BuyerId = rnd.Next(1, usersId.Length - 1);

                }

                db.Products.AddRange(products);
                db.SaveChanges();
            }
        }

        private static void ResetDatabase()
        {
            using (var db = new ProductsShopDb())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
