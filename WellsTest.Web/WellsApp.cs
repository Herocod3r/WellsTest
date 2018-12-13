using System;
using Microsoft.AspNetCore.Hosting;

namespace WellsTest.Web
{
    public class WellsApp : WellsTest.Core.Services.IApp
    {
        private readonly IHostingEnvironment env;

        public WellsApp(IHostingEnvironment env)
        {
            this.env = env;
        }

        public string GetPathToDb()
        {
           
            return $"Filename={ System.IO.Path.Combine(env.WebRootPath, "Data.db")};Mode=Exclusive";
        }
    }
}
