using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class MyRepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext dbcontext;

        public MyRepositoryAsync(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<T> Create(T entidad)
        {
            try
            {
                 await dbcontext.Set<T>().AddAsync(entidad);
                await dbcontext.SaveChangesAsync();
                return entidad;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(T entidad)
        {
            try
            {
                dbcontext.Set<T>().Remove(entidad);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>> filtro)
        {
            try
            {
                T entidad = await dbcontext.Set<T>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> filtro = null)
        {
            try
            {
                IQueryable<T> queryEntidad = filtro == null ? dbcontext.Set<T>() : dbcontext.Set<T>().Where(filtro);
                return queryEntidad;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(T entidad)
        {
            try
            {
                dbcontext.Set<T>().Update(entidad);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
