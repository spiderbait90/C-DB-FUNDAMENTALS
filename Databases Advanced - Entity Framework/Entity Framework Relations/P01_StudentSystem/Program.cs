using System;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;
using System.Linq;

namespace P01_StudentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var contex = new StudentSystemContext())
            {
                contex.Database.EnsureDeleted();
                contex.Database.EnsureCreated();

                var students = new[]
                {
                    new Student{Name="Ivan",RegisteredOn=new DateTime(2017,12,2)},
                    new Student{Name="Vasil",RegisteredOn=new DateTime(2017,12,2)}
                };

                contex.Students.AddRange(students);

                var courses = new[]
                {
                new Course{Name="Engeneering",StartDate= new DateTime(2016,2,2),EndDate=new DateTime(),Price=2.50m},
                new Course{Name="Something",StartDate= new DateTime(2016,2,2),EndDate=new DateTime(),Price=2.50m},
                };
                contex.Courses.AddRange(courses);

                contex.SaveChanges();

                var coursesId = contex.Courses.Select(x => x.CourseId).ToArray();
                var studentsId = contex.Students.Select(x => x.StudentId).ToArray();

                var resources = new[]
                {
                new Resource{Name="Edisi Kyde",Url="www.abv.bg",ResourceType=ResourceType.Document,CourseId=coursesId[0]},
                new Resource{Name="Edisi one",Url="www.abv.bg",ResourceType=ResourceType.Document,CourseId=coursesId[1]},
                };
                contex.Resources.AddRange(resources);

                var homeworks = new[]
                {
                new Homework{Content="alabala",ContentType=ContentType.Application,SubmissionTime=new DateTime(2015,2,2),StudentId=studentsId[0],CourseId=coursesId[0]},
                new Homework{Content="ehaa",ContentType=ContentType.Application,SubmissionTime=new DateTime(2015,2,2),StudentId=studentsId[1],CourseId=coursesId[1]},
                };
                contex.HomeworkSubmissions.AddRange(homeworks);

                var mappings = new[]
                {
                    new StudentCourse{StudentId=studentsId[0],CourseId=coursesId[1]},
                    new StudentCourse { StudentId=studentsId[1],CourseId=coursesId[0] }
                };
                contex.StudentCourses.AddRange(mappings);

                contex.SaveChanges();
            }
        }
    }
}
