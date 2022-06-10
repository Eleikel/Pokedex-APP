using Application.Repository.Repositories;
using Application.ViewModels;
using Database;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RegionService
    {
        private readonly RegionRepository _regionRepository;

        public RegionService(ApplicationDbContext dbContext)
        {
            _regionRepository = new(dbContext);
        }


        //GET ALL
        public async Task<List<RegionViewModel>> GetAllViewModel()
        {
            var regionList = await _regionRepository.GetAllAsync();
            return regionList.Select(product => new RegionViewModel
            {
                Id = product.Id,
                Name = product.Name
            }).ToList();
        }

        //GET ID
        public async Task<RegionViewModel> GetByIdSaveViewModel(int id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            RegionViewModel regionViewModel = new();
            regionViewModel.Id = region.Id;
            regionViewModel.Name = region.Name;

            return regionViewModel;
           
        }

        //Add
        public async Task Add(RegionViewModel vm)
        {
            Region region = new();
            region.Name = vm.Name;

            await _regionRepository.AddAsync(region);
        }


        //Update
        public async Task Update(RegionViewModel vm)
        {
            Region region = new();
            region.Id = vm.Id;
            region.Name = vm.Name;

            await _regionRepository.UpdateAsync(region);
        }


        //Delete
        public async Task Delete(int id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            await _regionRepository.DeleteAsync(region);
        }

    }
}
