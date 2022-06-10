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
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RegionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<List<Region>> GetAllAsync()
        {
           return await _dbContext.Region.ToListAsync();

        }
        public override async Task<Region> GetByIdAsync(int id)
        {
            return await _dbContext.Region.FindAsync(id);
        }
        public override async Task AddAsync(Region region)
        {
             await _dbContext.Region.AddAsync(region);
             await _dbContext.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Region region)
        {
            _dbContext.Entry(region).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }
        public override async Task DeleteAsync(Region region)
        {
            _dbContext.Set<Region>().Remove(region);
            await _dbContext.SaveChangesAsync();
        }



    }
}
