using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            GetStudent();


            Console.ReadKey();
        }
        static void GetStudent()
        {
            {
                using (var db = new MyDbContext())
                {

                    db.Student.Add(new Student
                    {
                        StudentName = "liusile",
                        Course = new List<Course>
                        {
                         new  Course {  CourseName = "English" },
                         new  Course {  CourseName = "Chinese" }
                        }
                    });
                    db.SaveChanges();
                    var model = db.Student.FirstOrDefault(o => o.Id == 1);

                    Console.Write(model.StudentName);
                    Console.Write(model.Course.FirstOrDefault().CourseName);
                }
            }
        }
    }
}
