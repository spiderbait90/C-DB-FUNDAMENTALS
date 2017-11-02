using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartUp
{
    public static void Main(string[] args)
    {
        try
        {
            var studentSplit = Console.ReadLine().Split();
            var workerSplit = Console.ReadLine().Split();

            var student = new Student(studentSplit[0], studentSplit[1], studentSplit[2]);
            
            var worker =
                new Worker(workerSplit[0], workerSplit[1],
                decimal.Parse(workerSplit[2]), decimal.Parse(workerSplit[3]));

            Console.WriteLine(student);
            Console.WriteLine();
            Console.WriteLine(worker);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }
}

