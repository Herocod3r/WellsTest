using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WellsTest.Core.Models;
using WellsTest.Core.Services;
using WellsTest.Core.ViewModels;

namespace WellsTest.Web.Pages
{
    public class StudentRecordModel : PageModel
    {
        private readonly IDbStore store;

        public StudentRecordModel(IDbStore store)
        {
            this.store = store;
        }

        public async Task OnGetAsync(Guid id)
        {

            Student = (await store.FindAsync<Student>(x => x.Id == id)).FirstOrDefault();
            if(Student is null)
            {
                RedirectToPage("ChooseStudent");
                return;
            }

            var courses = (await store.FindAsync<Course>(x => x.ClassId == Student.ClassId)).ToList();

            var records = (await store.FindAsync<Record>(x => x.StudentId == Student.Id)).ToList();

            Records = new List<RecordViewModel>();


            records.ForEach(x => Records.Add(new RecordViewModel
            {

                  CourseId = x.CourseId, Exam = x.Exam, FirstAssignment = x.FirstAssignment, FirstTest = x.FirstTest, Id = x.Id, SecondAssignment = x.SecondAssignment, SecondTest = x.SecondTest, Student = Student, StudentId = x.StudentId, Course = courses.FirstOrDefault(y=>y.Id == x.CourseId)
            }));

        }



        public List<RecordViewModel> Records { get; set; }

        public Student Student { get; set; }



    }
}
