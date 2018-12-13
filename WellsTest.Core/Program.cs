using System;
using System.Threading.Tasks;



namespace WellsTest.Core
{
    public class Program
    {
        private readonly Services.IDbStore store;

        public Program(Services.IDbStore store)
        {
            this.store = store;
        }


        public async Task<Models.Student> EnrollStudentAsync(ViewModels.StudentViewModel student)
        {
            var value = new Models.Student() { Fullname = student.Fullname, ClassId = student.Class.Id };

            await store.InsertAsync(value);
            return value;
        }

        public async Task<Models.Teacher> EnrollTeacherAsync(Models.Teacher teacher)
        {
            await store.InsertAsync(teacher);
            return teacher;
        }


        public async Task<Models.Class> InsertClassRecord(Models.Class cls)
        {
            await store.InsertAsync(cls);
            return cls;
        }



    }
}
