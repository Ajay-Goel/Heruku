using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public enum Gender { Male = 1, Female = 2 }
    public class Student
    {
        public Student(int id, int gpa, string firstname, string lastname, string email, string mobileNo, 
            string password, DateTime birthDate, bool isPartTime, string notes, string title, 
            IEnumerable<string> interests, Gender gender)
        {
            Id = id;
            GPA = gpa;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            MobileNo = mobileNo;
            Password = password;
            BirthDate = birthDate;
            IsPartTime = isPartTime;
            Notes = notes;
            Title = title;
            Interests = interests;
            Gender = gender;
        }

        // Add Key Attribute:
        [Key]
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int GPA { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string MobileNo { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Part Time?")]
        public bool IsPartTime { get; set; }

        public string Notes { get; set; }
        public string Title { get; set; }

        public IEnumerable<string> Interests { get; set; }
        public Gender Gender { get; set; }

        public List<SelectListItem> GetTitles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Value= "Mr", Text = "Mr."},
                new SelectListItem{ Value= "Mrs", Text = "Mrs."},
                new SelectListItem{ Value= "Dr", Text = "Dr."},
            };
        }

        public List<SelectListItem> GetInterests()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Value= "C", Text = "Coding"},
                new SelectListItem{ Value= "G", Text = "Gaming"},
                new SelectListItem{ Value= "M", Text = "Movies"},
                new SelectListItem{ Value= "S", Text = "Badminton"},
                new SelectListItem{ Value= "S", Text = "PingPong"},
                new SelectListItem{ Value= "S", Text = "Cricket"},
                new SelectListItem{ Value= "S", Text = "Squash"}
            };
        }
    }
}
