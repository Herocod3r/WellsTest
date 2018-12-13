using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WellsTest.Core
{
    public class LiteDBStore : Services.IDbStore
    {
        LiteDB.LiteDatabase db;
        public LiteDBStore(Services.IApp app)
        {
            db = new LiteDB.LiteDatabase(app.GetPathToDb());
        }

        public async Task<bool> DeleteAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> query)
        {
            var col = db.GetCollection<T>();
            try
            {

                await Task.Run(() => col.Delete(query));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> query, int start = 0, int max = 100)
        {
            var col = db.GetCollection<T>();
           return await Task.Run(()=>  col.Find(query, start, max));
        }

        public async Task<bool> InsertAsync<T>(T item)
        {
            var col = db.GetCollection<T>();
            try
            {
                await Task.Run(()=> col.Insert(item));
                return true;
            }
            catch 
            {
                return false;
            }

        }

        public async Task<bool> UpdateAsync<T>(T item)
        {
            var col = db.GetCollection<T>();
            try
            {
                await Task.Run(()=> col.Update(item));
                return true;
            }
            catch 
            {
                return false;
            }

        }

        public async Task<IEnumerable<T>> FindALLAsync<T>()
        {
            try
            {
                var col = db.GetCollection<T>();
                return await Task.Run(() => col.FindAll());
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

      
        public void Dispose()
        {
           
            Dispose(true);

        }

      
        #endregion
    }
}
