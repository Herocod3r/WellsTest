using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WellsTest.Core.Models;
using WellsTest.Core.Services;
using WellsTest.Core.ViewModels;

namespace WellsTest.Web.Pages
{
    public class RecordModel : PageModel
    {
        private readonly IDbStore store;

        public RecordModel(IDbStore store)
        {
            this.store = store;
        }

        public async Task OnGetAsync(Guid courseId)
        {
            Record = new Record { CourseId = courseId };

            var course = (await store.FindAsync<Course>(x => x.Id == courseId)).FirstOrDefault();
            if (course is null)
            {
                RedirectToPage("/ChooseCourse");
                return;
            }

            Students = (await store.FindAsync<Student>(x => x.ClassId == course.ClassId)).ToList();

            var itms = (await store.FindAsync<Record>(x => x.CourseId == courseId)).ToList();
            Records = new List<RecordViewModel>();

            itms.ForEach(x => Records.Add(new RecordViewModel
            {
                 Exam = x.Exam, CourseId = x.CourseId, Course = course, FirstAssignment = x.FirstAssignment, FirstTest = x.FirstTest, Id = x.Id, SecondAssignment = x.SecondAssignment, SecondTest = x.SecondTest, StudentId = x.StudentId, Student = Students.FirstOrDefault(y=>y.Id == x.StudentId)
            }));

        }


        [BindProperty]
        public Record Record { get; set; }


        public List<RecordViewModel> Records { get; set; }


        public List<Student> Students { get; set; }

        public List<SelectListItem> StudentsList => Students.Select(x => new SelectListItem(x.Fullname, x.Id.ToString())).ToList();


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if(Record.Total > 100)
            {
                return Page();
            }

            await store.InsertAsync(Record);

            return Redirect("/Record?courseId="+Record.CourseId.ToString());
        }



    }
}
