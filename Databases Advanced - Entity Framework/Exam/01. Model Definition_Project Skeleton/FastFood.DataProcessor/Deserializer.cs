using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var employeesJson = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            var str = new StringBuilder();
            var employees = new List<Employee>();

            foreach (var e in employeesJson)
            {
                if (e.Age == null)
                {
                    str.AppendLine(FailureMessage);
                    continue;
                }
                var parsedAge = int.Parse(e.Age);

                if (e.Name.Length < 3 || e.Name.Length > 30 || parsedAge > 80 || parsedAge < 15 ||
                    e.Position.Length > 30 || e.Position.Length < 3 ||
                    e.Position == null || e.Name == null)
                {
                    str.AppendLine(FailureMessage);
                    continue;
                }
                var position = employees.FirstOrDefault(x => x.Position.Name == e.Position)?.Position;

                if (position==null)
                {
                    position= new Position()
                    {
                        Name = e.Position
                    };
                }

                var employee = new Employee()
                {
                    Name = e.Name,
                    Age = parsedAge,
                    Position = position
                };

                employees.Add(employee);
                str.AppendLine(string.Format(SuccessMessage, e.Name));
            }
            context.AddRange(employees);
            context.SaveChanges();

            return str.ToString();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var itemsJson = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);
            var str = new StringBuilder();
            var items = new List<Item>();

            foreach (var i in itemsJson)
            {
                if (string.IsNullOrEmpty(i.Name) || string.IsNullOrEmpty(i.Category) || string.IsNullOrEmpty(i.Price))
                {
                    str.AppendLine(FailureMessage);
                    continue;
                }

                var parsedPrice = decimal.Parse(i.Price);
                if (parsedPrice < 0.01m || i.Name.Length < 3 || i.Name.Length > 30 ||
                    i.Category.Length > 30 || i.Category.Length < 3 ||
                    context.Items.Any(x => x.Name == i.Name) ||
                    items.Any(x => x.Name == i.Name))
                {
                    str.AppendLine(FailureMessage);
                    continue;
                }
                Category categorie = new Category();

                if (context.Categories.Any(x => x.Name == i.Category))
                {
                    categorie = context.Categories.Single(x => x.Name == i.Category);
                }
                else if (items.Any(x => x.Category.Name == i.Category))
                {
                    foreach (var a in items)
                    {
                        if (a.Category.Name == i.Category)
                        {
                            categorie.Name = i.Category;
                            categorie = a.Category;
                            break;
                        }
                    }

                }

                else if (!context.Categories.Any(x => x.Name == i.Category) &&
                    !items.Any(x => x.Name == i.Category))
                {
                    categorie = new Category()
                    {
                        Name = i.Category
                    };
                }


                var item = new Item()
                {
                    Name = i.Name,
                    Price = parsedPrice,
                    Category = categorie
                };

                items.Add(item);
                str.AppendLine(string.Format(SuccessMessage, i.Name));
            }

            context.AddRange(items);
            context.SaveChanges();

            return str.ToString();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var ordersXml = XDocument.Parse(xmlString);
            var str = new StringBuilder();
            var orders = new List<Order>();

            foreach (var o in ordersXml.Root.Elements())
            {
                var customer = o.Element("Customer")?.Value;
                var employee = o.Element("Employee")?.Value;
                var dateTime = o.Element("DateTime")?.Value;
                var type = o.Element("Type")?.Value;

                var emp = context.Employees.FirstOrDefault(x => x.Name == employee);

                if (string.IsNullOrEmpty(customer) ||
                    string.IsNullOrEmpty(employee) ||
                    string.IsNullOrEmpty(dateTime) ||
                    string.IsNullOrEmpty(type) ||
                    emp == null)
                {
                    str.AppendLine(FailureMessage);
                    continue;
                }

                var order = new Order()
                {
                    Employee = emp,
                    Customer = customer,
                    DateTime = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Type = Enum.Parse<OrderType>(type)
                };

                foreach (var i in o.Element("Items").Elements())
                {
                    var name = i.Element("Name")?.Value;
                    var quantity = i.Element("Quantity")?.Value;

                    var item = context.Items.FirstOrDefault(x => x.Name == name);
                    

                    if (string.IsNullOrEmpty(name) ||
                        string.IsNullOrEmpty(quantity) ||
                        item == null)
                    {
                        str.AppendLine(FailureMessage);
                        goto SkipEntity;
                    }

                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Item = item,
                        Quantity = int.Parse(quantity)
                        
                    };

                    order.OrderItems.Add(orderItem);
                }
                
                orders.Add(order);
                str.AppendLine($"Order for {customer} on {dateTime} added") ;

                SkipEntity:
                {
                    //////////
                }

            }

            context.Orders.AddRange(orders);
            context.SaveChanges();
            Console.WriteLine(str.ToString());
            return str.ToString();
        }
    }
}