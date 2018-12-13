using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WellsTest.Core.Services
{
    //Where to persist data from
    public interface IDbStore : IDisposable
    {
        Task<IEnumerable<T>> FindAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> query, int start = 0, int max = 100);

        Task<IEnumerable<T>> FindALLAsync<T>();

        Task<bool> InsertAsync<T>(T item);

        Task<bool> UpdateAsync<T>(T item);

        Task<bool> DeleteAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> query);
    }
}
