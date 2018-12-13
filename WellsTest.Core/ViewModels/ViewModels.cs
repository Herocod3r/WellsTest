using System;
namespace WellsTest.Core.ViewModels
{
   
    public class StudentViewModel : Models.Student
    {
       
        public Models.Class Class { get; set; }
    }

    public class CourseViewModel : Models.Course
    {
        public Models.Class Class { get; set; }
        public Models.Teacher Teacher { get; set; }
    }


    public class RecordViewModel : Models.Record
    {
        public Models.Course Course { get; set; }
        public Models.Student Student { get; set; }
    }


}
