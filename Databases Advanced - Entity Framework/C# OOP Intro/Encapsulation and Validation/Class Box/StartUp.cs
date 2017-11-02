using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class StartUp
{    public static void Main(string[] args)
    {
        Type boxType = typeof(Box);
        FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        Console.WriteLine(fields.Count());

        var length = decimal.Parse(Console.ReadLine());
        var width = decimal.Parse(Console.ReadLine());
        var height = decimal.Parse(Console.ReadLine());

        try
        {
            var box = new Box(length, width, height);
            box.SurfaceArea();
            box.LateralArea();
            box.Volume();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);            
        }
        
        
    }
}

