using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> filtro = null);
        Task<T> Get(Expression<Func<T, bool>> filtro);
        Task<T> Create(T entidad);
        Task<bool> Update(T entidad);
        Task<bool> Delete(T entidad);
    }
}
