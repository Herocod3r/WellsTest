using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WellsTest.Core.Models;
using WellsTest.Core.Services;

namespace WellsTest.Web.Pages
{
    public class ChooseStudentModel : PageModel
    {
        private readonly IDbStore store;

        public ChooseStudentModel(IDbStore store)
        {
            this.store = store;
        }

        public void OnGet()
        {
        }



        [BindProperty]
        public string Name { get; set; }

        [TempData]
        public string Message { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var item = (await store.FindAsync<Student>(x => x.Fullname.ToLower() == Name.ToLower())).FirstOrDefault();

            if(item is null)
            {
                return Page();
            }



            // await store.InsertAsync(Class);

            return RedirectToPage("/StudentRecord",new { item.Id });
        }

    }
}
