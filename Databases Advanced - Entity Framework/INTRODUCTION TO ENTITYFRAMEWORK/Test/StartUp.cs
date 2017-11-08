using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System.Globalization;

namespace P02_DatabaseFirst
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // 3.Employees Full Information

            //using (var db = new SoftUniContext())
            //{
            //    var employees = db.Employees
            //        .Select(x => new
            //        {
            //            x.FirstName,
            //            x.LastName,
            //            x.MiddleName,
            //            x.JobTitle,
            //            x.Salary
            //        })
            //        .ToList();

            //    foreach (var e in employees)
            //    {
            //        Console.WriteLine($"{e.FirstName} " +
            //            $"{e.LastName} " +
            //            $"{e.MiddleName} " +
            //            $"{e.JobTitle} " +
            //            $"{e.Salary:f2}");
            //    }



            // 4.Employees with Salary Over 50 000

            //using (var db = new SoftUniContext())
            //{
            //    var employees = db.Employees
            //        .Select(x => new
            //        {
            //            x.FirstName,
            //            x.Salary
            //        })
            //        .Where(x => x.Salary > 50000)
            //        .OrderBy(x=>x.FirstName)
            //        .ToList();

            //    foreach (var e in employees)
            //    {
            //        Console.WriteLine($"{e.FirstName}");
            //    }                
            //}
            // 


            // 5.Employees from Research and Development

            //using (var db = new SoftUniContext())
            //{
            //    var employees = db.Employees
            //        .Select(x => new
            //        {
            //            x.FirstName,
            //            x.LastName,
            //            x.Department,
            //            x.Salary
            //        })
            //        .Where(x => x.Department.Name == "Research and Development")
            //        .OrderBy(x => x.Salary)
            //        .ThenByDescending(x=>x.FirstName)
            //        .ToList();

            //    foreach (var e in employees)
            //    {
            //        Console.WriteLine($"{e.FirstName} " +
            //            $"{e.LastName} from " +
            //            $"{e.Department.Name} - " +
            //            $"${e.Salary:f2}");
            //    }
            //}


            // 6.Adding a New Address and Updating Employee

            //using (var db = new SoftUniContext())
            //{
            //    var adress = new Address()
            //    {
            //        AddressText = "Vitoshka 15",
            //        TownId = 4
            //    };

            //    var nakov = db.Employees
            //        .Include(x=>x.Address)
            //        .SingleOrDefault(x => x.LastName == "Nakov");

            //    nakov.Address = adress;

            //    foreach (var item in db.Employees
            //        .Include(x=>x.Address)
            //        .OrderByDescending(x=>x.AddressId)
            //        .Take(10)
            //        .ToList())
            //    {
            //        Console.WriteLine(item.Address.AddressText);
            //    }
            //}


            // 7.Employees and Projects

            //using (var db = new SoftUniContext())
            //{
            //    var employees = db.Employees
            //        .Include(x => x.EmployeesProjects)
            //        .ThenInclude(x => x.Project)
            //        .Where(x => x.EmployeesProjects
            //        .Any(s => s.Project.StartDate.Year >= 2001 &&
            //        s.Project.StartDate.Year <= 2003))
            //        .Take(30)
            //        .ToList();
            //    var format = "M/d/yyyy h:mm:ss tt";

            //    foreach (var e in employees)
            //    {
            //        var managerId = e.ManagerId;
            //        var manager = db.Employees.Find(managerId);

            //        Console.WriteLine($"{e.FirstName} {e.LastName} - " +
            //            $"Manager: {manager.FirstName} {manager.LastName}");

            //        foreach (var p in e.EmployeesProjects)
            //        {
            //            Console.Write($"--{p.Project.Name} - " +
            //                $"{p.Project.StartDate.ToString(format, CultureInfo.InvariantCulture)} - ");


            //            if (p.Project.EndDate == null)
            //                Console.WriteLine("not finished");
            //            else
            //            {
            //                Console.WriteLine(p.Project.EndDate.Value.ToString(format, CultureInfo.InvariantCulture));
            //            }

            //        }
            //    }
            //}


            // 8.Addresses by Town

            //using (var db = new SoftUniContext())
            //{
            //    var adresses = db.Addresses 
            //        .Include(x=>x.Employees)
            //        .Include(x=>x.Town)                    
            //        .OrderByDescending(x=>x.Employees.Count)
            //        .ThenBy(x=>x.Town.Name)
            //        .ThenBy(x=>x.AddressText)
            //        .Take(10)
            //        .ToList();

            //    foreach (var a in adresses)
            //    {
            //        Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees");
            //    }
            //}


            // 9.Employee 147

            //using (var db = new SoftUniContext())
            //{
            //    var e = db.Employees
            //        .Include(x => x.EmployeesProjects)
            //        .ThenInclude(x => x.Project)
            //        .First(x => x.EmployeeId == 147);

            //    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");

            //    foreach (var p in e.EmployeesProjects
            //        .OrderBy(x=>x.Project.Name))
            //    {
            //        Console.WriteLine(p.Project.Name);
            //    }
            //}


            // 10.Departments with More Than 5 Employees

            //using (var db = new SoftUniContext())
            //{
            //    var departments = db.Departments
            //        .Include(x => x.Employees)
            //        .Where(x => x.Employees.Count() > 5)
            //        .ToList();

            //    foreach (var d in departments
            //        .OrderBy(x => x.Employees.Count())
            //        .ThenBy(x=>x.Name))
            //    {
            //        var manager = db.Employees.Find(d.ManagerId);

            //        Console.WriteLine($"{d.Name} - {manager.FirstName} {manager.LastName}");

            //        foreach (var e in d.Employees
            //            .OrderBy(x => x.FirstName)
            //            .ThenBy(x => x.LastName))
            //        {
            //            Console.WriteLine($"{e.FirstName} {e.LastName} - " +
            //                $"{e.JobTitle}");
            //        }

            //        Console.WriteLine("----------");
            //    }
            //}


            // 11.Find Latest 10 Projects

            //using (var db = new SoftUniContext())
            //{
            //    var projects = db.Projects
            //        .OrderByDescending(x => x.StartDate)
            //        .Take(10)
            //        .OrderBy(x=>x.Name)
            //        .ToList();

            //    var format = "M/d/yyyy h:mm:ss tt";

            //    foreach (var p in projects)
            //    {
            //        Console.WriteLine(p.Name);
            //        Console.WriteLine(p.Description);
            //        Console.WriteLine(p.StartDate.ToString(format,CultureInfo.InvariantCulture));
            //    }
            //}

            // 12.Increase Salaries

            //using (var db = new SoftUniContext())
            //{
            //    var employees = db.Employees
            //        .Include(x => x.Department)
            //        .Where(x => x.Department.Name == "Engineering" ||
            //        x.Department.Name == "Tool Design" ||
            //        x.Department.Name == "Marketing" ||
            //        x.Department.Name == "Information Services")  
            //        .OrderBy(x=>x.FirstName)
            //        .ThenBy(x=>x.LastName)
            //        .ToList();

            //    foreach (var e in employees)
            //    {
            //        e.Salary += (e.Salary * 0.12m);
            //        Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
            //    }

            //    db.SaveChanges();
            //}

            // 13.Find Employees by First Name Starting With "Sa"

            //using (var db = new SoftUniContext())
            //{

            //    var employees = db.Employees
            //        .Where(x => x.FirstName.StartsWith("Sa"))
            //        .OrderBy(x => x.FirstName)
            //        .ThenBy(x => x.LastName)
            //        .ToList();

            //    foreach (var e in employees)
            //    {
            //        Console.WriteLine($"{e.FirstName} {e.LastName} - " +
            //            $"{e.JobTitle} - (${e.Salary:f2})");
            //    }
            //}

            // 14.Delete Project by Id

            //using (var db = new SoftUniContext())
            //{
            //    var project = db.Projects.Find(2);

            //    var empProj = db.EmployeesProjects
            //        .Where(x => x.ProjectId == 2)
            //        .ToList();

            //    db.EmployeesProjects.RemoveRange(empProj);
            //    db.Projects.Remove(project);
            //    db.SaveChanges();
            //    var projects = db.Projects.Take(10).ToList();

            //    foreach (var p in projects)
            //    {
            //        Console.WriteLine(p.Name);
            //    }
            //}


            // 15.Remove Towns


            //using(var db = new SoftUniContext())
            //{
            //    var input = Console.ReadLine();
            //    var town = db.Towns
            //        .Where(x => x.Name == input)
            //        .FirstOrDefault();

            //    if(town==null)
            //    {
            //        Console.WriteLine("No Such town");
            //        return;
            //    }

            //    var adresses = db.Addresses
            //        .Where(x => x.Town.Name == input)
            //        .ToList();                

            //    foreach (var e in db.Employees)
            //    {
            //        foreach (var adress in adresses)
            //        {
            //            if (adress.AddressId == e.AddressId)
            //                e.AddressId = null;
            //        }
            //    }

            //    db.Addresses.RemoveRange(adresses);
            //    db.Towns.Remove(town);                

            //    Console.WriteLine($"{adresses.Count()} addresses in {town.Name} were deleted");

            //    db.SaveChanges();
            //}
        }
    }
}

