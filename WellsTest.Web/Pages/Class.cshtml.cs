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
    public class ClassModel : PageModel
    {
        private readonly IDbStore store;

        public ClassModel(IDbStore store)
        {
            this.store = store;

        }




        public async Task OnGetAsync()
        {
            var items = await store.FindALLAsync<Class>();
            Classes = items.ToList();
        }


        public IList<Class> Classes { get; set; }


        [BindProperty]
        public Class Class { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await store.InsertAsync(Class);

            return RedirectToPage("/Class");
        }
    }
}
