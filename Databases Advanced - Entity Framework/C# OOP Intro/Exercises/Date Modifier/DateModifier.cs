using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DateModifier
{
    public int DaysDiff(string date1,string date2)
    {
        var d1 = Convert.ToDateTime(date1);
        var d2 = Convert.ToDateTime(date2);
        int days = Math.Abs((d1 - d2).Days);
        return days;
    }
}

