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
    public class ChooseCourseModel : PageModel
    {
        private readonly IDbStore store;

        public ChooseCourseModel(IDbStore store)
        {
            this.store = store;
        }


        public async Task OnGetAsync()
        {
            Courses = (await store.FindALLAsync<Course>()).ToList();
        }

       
        public List<Course> Courses { get; set; }
    }
}
