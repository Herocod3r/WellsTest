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
    public class TeacherModel : PageModel
    {
        private readonly IDbStore store;

        public TeacherModel(IDbStore store)
        {
            this.store = store;

        }

        [BindProperty]
        public Teacher Teacher { get; set; }

        public async Task OnGetAsync()
        {
            var items = await store.FindALLAsync<Teacher>();
            Teachers = items.ToList();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await store.InsertAsync(Teacher);

            return RedirectToPage("/Teacher");
        }



        public List<Teacher> Teachers { get; set; }

    }
}
