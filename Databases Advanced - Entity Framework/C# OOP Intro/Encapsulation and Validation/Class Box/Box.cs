using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Box
{
    private decimal length;
    private decimal width;
    private decimal height;

    public Box(decimal length, decimal width, decimal height)
    {
        Length = length;
        Width = width;
        Height = height;
    }
    public decimal Length
    {
        get { return length; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Length cannot be zero or negative.");
            }

            length = value;
        }
    }
    public decimal Width
    {
        get { return width; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative.");
            }

            width = value;
        }
    }
    public decimal Height
    {
        get { return height; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative.");
            }

            height = value;
        }
    }

    public void Volume()
    {
        var result = Length * Height * Width;
        Console.WriteLine($"Volume - {result:f2}");
    }
    public void LateralArea()
    {
        var result = (2 * Length * Height) + (2 * Width * Height);
        Console.WriteLine($"Lateral Surface Area - {result:f2}");
    }
    public void SurfaceArea()
    {
        var result = (2 * Length * Width) + (2 * Length * Height) + (2 * Width * Height);
        Console.WriteLine($"Surface Area - {result:f2}");
    }
}

