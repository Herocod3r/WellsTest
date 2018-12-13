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
    public class StudentModel : PageModel
    {
        private readonly IDbStore store;

        public StudentModel(IDbStore store)
        {
            this.store = store;
        }

        public async Task OnGetAsync()
        {
            var itm = (await store.FindALLAsync<Student>()).ToList();
            var itmCls = (await store.FindALLAsync<Class>()).ToList();
            Students = new List<StudentViewModel>();
            Classes = itmCls;

            Students = itm.Join(itmCls,(arg) => arg.ClassId,(arg) => arg.Id,(arg1, arg2) => new StudentViewModel { Class = arg2, Fullname = arg1.Fullname, ClassId = arg1.Id }).ToList();
            
            
        }







        public List<StudentViewModel> Students { get; set; }

        public List<Class> Classes { get; set; }

        public List<SelectListItem> ClassesList => Classes?.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await store.InsertAsync(Student);

            return RedirectToPage("/Student");
        }
    }
}
