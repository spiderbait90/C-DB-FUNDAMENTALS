using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using FastFood.Data;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var parsedEnum = Enum.Parse<OrderType>(orderType);

            var orders = context.Employees
                .Include(x => x.Orders)
                .Where(x => x.Orders.Any(a => a.Type == parsedEnum && a.Employee.Name == employeeName))
                .Select(x => new
                {
                    Name = x.Name,
                    Orders = x.Orders.Select(a => new
                        {
                            Customer = a.Customer,
                            Items = a.OrderItems.Select(b => new
                            {
                                Name = b.Item.Name,
                                Price = b.Item.Price,
                                Quantity = b.Quantity
                            }).ToArray(),
                            TotalPrice = a.TotalPrice
                        }).OrderByDescending(v => v.TotalPrice)
                        .ThenByDescending(w => w.Items.Count()),
                    TotalMade = x.Orders.Sum(z => z.TotalPrice)
                }).First();

            var json = JsonConvert.SerializeObject(orders, Formatting.Indented);

            return json;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var categoriesArr = categoriesString.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var cat = context.Categories
                .Where(x => categoriesArr.Contains(x.Name))
                .Select(x => new
                {
                    Name = x.Name,
                    MostPopularItem = x.Items.Select(a => new
                    {
                        Name = a.Name,
                        TotalMade = a.OrderItems.Sum(b => b.Quantity * b.Item.Price),
                        TimesSold = a.OrderItems.Sum(q => q.Quantity)
                    }).OrderByDescending(q => q.TotalMade).First()
                })
                .OrderByDescending(x => x.MostPopularItem.TotalMade)
                .ThenByDescending(x => x.MostPopularItem.TimesSold)
                .ToArray();

            var xml = new XDocument(new XElement("Categories"));
            foreach (var c in cat)
            {
                var category = new XElement("Category");
                category.Add(new XElement("Name", c.Name));

                var MostPopularItem = new XElement("MostPopularItem");

                MostPopularItem.Add(new XElement("Name", c.MostPopularItem.Name));

                MostPopularItem.Add(new XElement("TotalMade", c.MostPopularItem.TotalMade));

                MostPopularItem.Add(new XElement("TimesSold", c.MostPopularItem.TimesSold));
                category.Add(MostPopularItem);

                xml.Root.Add(category);
            }

            var s = xml.ToString();
            return xml.ToString();

        }
    }
}