using SchoolSystem.DTOs;
using System.Linq.Expressions;

namespace SchoolSystem.School.DAL.GenaricRepo
{
    public interface IGenaricRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(object id);
        Task<ResponseModel<object>> InsertAsync(T entity);
        Task<ResponseModel<object>> UpdateAsync(T entity);
        Task<ResponseModel<object>> DeleteAsync(object id);
        Task SaveChangesAsync();
    }
}
