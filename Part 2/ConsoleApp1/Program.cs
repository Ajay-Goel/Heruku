using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Wait for the async stuff to run... 
            Run().Wait();

            // Then Write Done... 
            Console.WriteLine("");
            Console.WriteLine("Done! Press any key to Exit...");
            Console.ReadLine();
            return;

         
        }

        static async Task Run()
        {
            Console.WriteLine("Student client operations..");
            try
            {
                // Create a company client instance: 
                var baseUri = new Uri("http://localhost:50255");
                var studentClient = new StudentClient("http://localhost:50255");

                // Read initial student list: 
                Console.WriteLine("Reading all students...");
                var students = await studentClient.GetStudentsAsync();
                WriteStudentsList(students);



                Console.WriteLine("Adding a new student...");
                int nextId = (from c in students select c.Id).Max() + 1;
                var xingyao = new Student(nextId, 4, "Wu", "Xingyao", "wu.x@husky.neu.edu", "800 555 1212", "mypassword", new System.DateTime(1980, 1, 1), false, "bó xué zhī，shěn wèn zhī，shèn sī zhī，míng biàn zhī，du xíng zhī", "Mr", 
                    new List<string> { "gaming", "coding" }, Gender.Male);
                var result = studentClient.AddStudent(xingyao);
                //WriteStatusCodeResult(result);

                Console.WriteLine("Updated student List after Add:");
                students = await studentClient.GetStudentsAsync();
                WriteStudentsList(students);

                Console.WriteLine("Updating a student...");
                var updateMe = await studentClient.GetStudentsAsync(1);
                //updateMe.First().Email = "test@123";
                //Console.WriteLine(updateMe);
                //Console.WriteLine(updateMe.First().Id);
                updateMe.First().Email = "test@123";
                result = await studentClient.UpdateStudentAsync(updateMe.First());
                WriteStatusCodeResult(result);

                Console.WriteLine("Updated student List after Update:");
                students = await studentClient.GetStudentsAsync();
                WriteStudentsList(students);

                Console.WriteLine("Deleting a student...");
                result = studentClient.DeleteStudent(1);
                //WriteStatusCodeResult(result);
                Console.WriteLine("Updated student List after Delete:");
                students = await studentClient.GetStudentsAsync();
                WriteStudentsList(students);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void WriteStudentsList(IEnumerable<Student> students)
        {
            foreach (Student student in students)
            {
                var output = JsonConvert.SerializeObject(student);
                Console.WriteLine(output);
            }
            Console.WriteLine("");
        }

        static void WriteStatusCodeResult(System.Net.HttpStatusCode statusCode)
        {
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Operation Succeeded - status code {0}", statusCode);
            }
            else
            {
                Console.WriteLine("Operation Failed - status code {0}", statusCode);
            }
            Console.WriteLine("");
        }
    }


}
