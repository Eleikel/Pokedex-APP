using Application.Repository.IRepositories;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected ApplicationDbContext _context;
        //protected DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
           _context = context;
            //this.dbSet = context.Set<T>();
        }


        public virtual Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
