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
    public class CourseModel : PageModel
    {
        private readonly IDbStore store;

        public CourseModel(IDbStore store)
        {
            this.store = store;
        }


        public async Task OnGetAsync()
        {
            Classes =(await store.FindALLAsync<Class>()).ToList();
            Teachers = (await store.FindALLAsync<Teacher>()).ToList();

            var itms = (await store.FindALLAsync<Course>()).ToList();
            Courses = new List<CourseViewModel>();
            itms.ForEach(x => Courses.Add(new CourseViewModel
            {
                 Name = x.Name,
                 Class = Classes.FirstOrDefault(y=>y.Id == x.ClassId),
                  Teacher = Teachers.FirstOrDefault(y=>y.Id == x.TeacherId)
            }));
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await store.InsertAsync(Course);

            return RedirectToPage("/Course");
        }



        [BindProperty]
        public Course Course { get; set; }

        public List<CourseViewModel> Courses { get; set; }

        public List<Class> Classes { get; set; }
        public List<Teacher> Teachers { get; set; }


        public List<SelectListItem> ClassesList => Classes.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

        public List<SelectListItem> TeachersList => Teachers.Select(x => new SelectListItem(x.Fullname, x.Id.ToString())).ToList();


    }
}
