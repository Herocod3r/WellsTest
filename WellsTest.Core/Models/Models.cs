using System;
using System.ComponentModel.DataAnnotations;
namespace WellsTest.Core.Models
{

    public class User
    {
        public Guid Id { get; set; }

        [Required,MinLength(3)]
        public string Fullname { get; set; }
    }


    public class Student : User
    {
        public Guid ClassId { get; set; }
    }

    public class Teacher : User
    {
        
    }


    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

   
    public class Course 
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid ClassId { get; set; }
        public Guid TeacherId { get; set; }

    }

    public class Record
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public float FirstAssignment { get; set; }
        public float SecondAssignment { get; set; }
        public float FirstTest { get; set; }
        public float SecondTest { get; set; }
        public float Exam { get; set; }

        public float Total => FirstAssignment + SecondAssignment + FirstTest + SecondTest + Exam;
        public string Grade
        {
            get
            {
                if (Total > 70) return "A";
                if (Total >= 60) return "B";
                if (Total >= 50) return "C";
                if (Total >= 40) return "D";
                if (Total >= 38) return "E";

                return "F";
            }
        }

    }

}
