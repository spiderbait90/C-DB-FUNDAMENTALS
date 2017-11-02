using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dough
{
    private string flourType;
    private string bakingTechnique;
    private double weight;
    private double modifierType;
    private double modifierTechnique;

    public Dough(string flourType, string bakingTechnique, double weight)
    {
        FlourType = flourType;
        BakingTechnique = bakingTechnique;
        Weight = weight;
    }

    public string FlourType
    {
        get { return flourType; }
        set
        {
            if (value == "white" || value == "wholegrain")
            {
                var val = value.Replace('w', 'W');
                flourType = val;
            }
            else
            {
                throw new ArgumentException("Invalid type of dough.");
            }
        }
    }
    public string BakingTechnique
    {
        get { return bakingTechnique; }
        set
        {
            if (value != "crispy" && value != "chewy" && value != "homemade")
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            var val = char.ToUpper(value[0])+value.Substring(1);
            bakingTechnique = val;
        }
    }
    public double Weight
    {
        get { return weight; }
        set
        {
            if (value < 1 || value > 200)
            {
                throw new ArgumentException("Dough weight should be in the range [1..200].");
            }
            weight = value;
        }
    }

    public double Calories()
    {        
        switch (BakingTechnique)
        {
            case "Crispy":modifierTechnique = 0.9;break;
            case "Chewy": modifierTechnique = 1.1; break;
            case "Homemade": modifierTechnique = 1.0; break;
        }
        switch (FlourType)
        {
            case "White": modifierType = 1.5; break;
            case "Wholegrain": modifierType = 1.0; break;
        }

        return (Weight * 2) * modifierType * modifierTechnique;
    }
}

