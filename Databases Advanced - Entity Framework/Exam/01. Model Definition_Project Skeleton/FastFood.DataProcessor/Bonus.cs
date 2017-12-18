using System;
using System.Linq;
using System.Text;
using FastFood.Data;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
	    public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
	        var item = context.Items.FirstOrDefault(x => x.Name == itemName);

	        var str = new StringBuilder();

	        if (item != null)
	        {
	            str.AppendLine($"{item.Name} Price updated from ${item.Price:f2} to ${newPrice:f2}");
	            item.Price = newPrice;
	            context.SaveChanges();
	        }

	        else
	        {
	            str.AppendLine($"Item {itemName} not found!");
	        }

	        return str.ToString();
	    }
    }
}
