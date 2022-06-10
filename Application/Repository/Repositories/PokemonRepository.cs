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
    public class PokemonRepository : GenericRepository<Pokemon>, IPokemonRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PokemonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<List<Pokemon>> GetAllAsync()
        {

            return await _context.Set<Pokemon>()
               .Include(re => re.Region)
               .Include(tp =>tp.TipoPrimary)
               .Include(ts => ts.TipoSecondary)
                 .ToListAsync();
        }

        public override async Task<Pokemon> GetByIdAsync(int id)
        {
            return await _dbContext.Pokemons.FindAsync(id);
        }
        public override async Task AddAsync(Pokemon pokemon)
        {
            await _dbContext.Pokemons.AddAsync(pokemon);
            await _dbContext.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Pokemon pokemon)
        {
            _dbContext.Entry(pokemon).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }
        public override async Task DeleteAsync(Pokemon pokemon)
        {
            _dbContext.Set<Pokemon>().Remove(pokemon);
            await _dbContext.SaveChangesAsync();
        }

    }
}
