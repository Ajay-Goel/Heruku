using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace server.Models
{
    public class StudentManager
    {
        List<Student> _students;
        public StudentManager()
        {
            try
            {
                if (File.Exists("students.txt"))
                {
                    _students = ReadStudentList().ToList();
                }
                else
                {
                     _students = new List<Student>() {
                        new Student(1, 3, "Ajay", "Goel", "goel.aj@husky.neu.edu", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "I don't like to write a lot of code!", "Dr",
                        new List<string> {"gaming", "coding" }, Gender.Male),
                        new Student(2, 4, "Bill", "Gates", "bill@microsoft.com", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "A PC on every desk", "Mr",
                        new List<string> {"coding" }, Gender.Male),
                        new Student(3, 4, "Elon", "Musk", "elon@tesla.com", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "Human colonies on Mars, now!", "Mr",
                        new List<string> {"Movies" }, Gender.Male),
                        new Student(4, 2, "Donald", "Trump", "donald@whitehouse.gov", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "Mexicans will pay for the Wall", "Mr",
                        new List<string> {"Movies" }, Gender.Male),
                        new Student(5, 4, "Vladimir", "Putin", "vladimir@kgb.ru", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "I had nothing to do with the last US election", "Mr",
                        new List<string> {"Coding" }, Gender.Male),
                        new Student(6, 4, "Emmanuel", "Macron", "emmanuel@orsay.fr", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "Vive la France", "Mr",
                        new List<string> {"coding" }, Gender.Male),
                        new Student(7, 4, "Barack", "Obama", "barack@retired.gov", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "I don't know, ask Michelle", "Mr",
                        new List<string> {"Movies" }, Gender.Male),
                        new Student(8, 3, "Alexis", "Tsipras", "alexis@maximou.gr", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "I love Che Guevarra", "Mr",
                        new List<string> {"Movies" }, Gender.Male),
                        new Student(9, 4, "jinping", "Xi", "jinping@zhengfu.cn", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "Yīxiē dùzi bǎomǎn de wàiguó rén, méiyǒu shé me bǐ zhè gèng hǎo dele", "Mr",
                        new List<string> {"Badminton", "PingPong" }, Gender.Male),
                        new Student(10, 4, "Narendra", "Modi", "narendra@sarakaar.in", "800 555 1212",
                        "mypassword", new System.DateTime(1980,1,1), false, "main ek gareeb parivaar se aaya hoon, mainne gareebee dekhee hai gareebon ko sammaan kee aavashyakata hai, aur yah svachchhata ke saath shuruaat hai", "Mr",
                        new List<string> {"Cricket", "Squash" }, Gender.Male)
                    };
                    WriteStudentsList(_students);
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe);
            }
        }

        void WriteStudentsList(IEnumerable<Student> students)
        {
            var output = JsonConvert.SerializeObject(students);
            File.WriteAllText("students.txt", output); 
        }

        IEnumerable<Student> ReadStudentList()
        {
            return JsonConvert.DeserializeObject<List<Student>>(File.ReadAllText("students.txt"));
        }

        public IEnumerable<Student> GetAll { get { return _students; } }

        public IEnumerable<Student> GetStudentsByID(int id)
        {
            //return _students.Where(o => o.GPA.Equals(gpa)).ToList();
            return _students.Where(_ => _.Id == id).ToList();
        }

        public void AddStudent(Student s)
        {
            _students.Add(s);
            var output = JsonConvert.SerializeObject(_students);
            File.WriteAllText("students.txt", output);
        }

        public bool DeleteStudent(int id)
        {
            if (!_students.Any(_ => _.Id == id)) return false;
            _students.RemoveAll(_ => _.Id == id);
            WriteStudentsList(_students);
            return true;
        }

        public bool EditStudent(Student s)
        {
            var _s = _students.FirstOrDefault(_ => _.Id == s.Id);
            if (_s == null) return false;
            _s.GPA = s.GPA;
            _s.Firstname = s.Firstname;
            _s.Lastname = s.Lastname;
            _s.Email = s.Email;
            _s.MobileNo = s.MobileNo;
            _s.Password = s.Password;
            _s.BirthDate = s.BirthDate;
            _s.IsPartTime = s.IsPartTime;
            _s.Notes = s.Notes;
            _s.Title = s.Title;
            _s.Interests = s.Interests;
            _s.Gender = s.Gender;
            WriteStudentsList(_students);
            return true;
        }
    }
}
