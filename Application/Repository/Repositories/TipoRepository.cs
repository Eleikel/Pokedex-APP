using Application.Repository.IRepositories;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Repositories
{
    public class TipoRepository : GenericRepository<Tipo>, ITipoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TipoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public override async Task<List<Tipo>> GetAllAsync()
        {
            return await _dbContext.Types.ToListAsync();

        }
        public override async Task<Tipo> GetByIdAsync(int id)
        {
            return await _dbContext.Types.FindAsync(id);
        }
        public override async Task AddAsync(Tipo tipo)
        {
            await _dbContext.Types.AddAsync(tipo);
            await _dbContext.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Tipo tipo)
        {
            _dbContext.Entry(tipo).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }
        public override async Task DeleteAsync(Tipo tipo)
        {
            _dbContext.Set<Tipo>().Remove(tipo);
            await _dbContext.SaveChangesAsync();
        }



    }
}
