using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tire
{
    public double Tire1Pressure { get; set; }
    public int Tire1Age { get; set; }
    public double Tire2Pressure { get; set; }
    public int Tire2Age { get; set; }
    public double Tire3Pressure { get; set; }
    public int Tire3Age { get; set; }
    public double Tire4Pressure { get; set; }
    public int Tire4Age { get; set; }

    public Tire(double t1p,int t1a, double t2p, int t2a,
        double t3p, int t3a, double t4p, int t4a)
    {
        Tire1Pressure = t1p; Tire1Age = t1a;
        Tire2Pressure = t2p; Tire2Age = t2a;
        Tire3Pressure = t3p; Tire3Age = t3a;
        Tire4Pressure = t4p; Tire4Age = t4a;
    }
}

