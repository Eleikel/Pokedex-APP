using Application.Repository.Repositories;
using Application.ViewModels;
using Application.ViewModels.Pokemons;
using Database;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PokemonService
    {
        public readonly PokemonRepository _pokemonRepository;

        public PokemonService(ApplicationDbContext dbContext)
        {
            _pokemonRepository = new(dbContext);
        }

        //GET ALL
        public async Task<List<PokemonViewModel>> GetAllViewModel()
        {
            var pokemonList = await _pokemonRepository.GetAllAsync();
            return pokemonList.Select(pokemon => new PokemonViewModel
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Foto = pokemon.Foto,
                RegionId = pokemon.RegionId,
                PrimaryTypeId = pokemon.TipoPrimary.Id,
                SecondaryTypeId = pokemon.TipoSecondary.Id,
                TipoPrimary = pokemon.TipoPrimary,
                TipoSecondary = pokemon.TipoSecondary,
                Region = pokemon.Region

            }).ToList();
        }


        //GET ALL With Filter
        public async Task<List<PokemonViewModel>> GetAllViewModelWithFilter(FilterPokemonViewModel filters)
        {
            var pokemonList = await _pokemonRepository.GetAllAsync();

            var listViewModels = pokemonList.Select(pokemon => new PokemonViewModel
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Foto = pokemon.Foto,
                RegionId = pokemon.Region.Id,
                PrimaryTypeId = pokemon.TipoPrimary.Id,
                SecondaryTypeId = pokemon.TipoSecondary.Id,
                TipoPrimary = pokemon.TipoPrimary,
                TipoSecondary = pokemon.TipoSecondary,
                Region = pokemon.Region


            }).ToList();

            if (filters.RegionId != null)
            {
                listViewModels = listViewModels.Where(pokemon => pokemon.RegionId == filters.RegionId.Value).ToList();
            }

            else if (filters.Name != null)
            {
                listViewModels = listViewModels.Where(pokemon => pokemon.Name == filters.Name).ToList();
            }

            return listViewModels;

        }



        //GET ID
        public async Task<PokemonViewModel> GetByIdViewModel(int id)
        {
            var pokemon = await _pokemonRepository.GetByIdAsync(id);

            PokemonViewModel pokemonViewModel = new();
            pokemonViewModel.Id = pokemon.Id;
            pokemonViewModel.Name = pokemon.Name;
            pokemonViewModel.Foto = pokemon.Foto;
            pokemonViewModel.RegionId = pokemon.RegionId;
            pokemonViewModel.PrimaryTypeId = pokemon.PrimaryTypeId;
            pokemonViewModel.SecondaryTypeId = pokemon.SecondaryTypeId;

            return pokemonViewModel;

        }

        //Add
        public async Task Add(PokemonViewModel vm)
        {
            Pokemon pokemon = new();
            pokemon.Id = vm.Id;
            pokemon.Name = vm.Name;
            pokemon.Foto = vm.Foto;
            pokemon.RegionId = vm.RegionId;
            pokemon.PrimaryTypeId = vm.PrimaryTypeId;
            pokemon.SecondaryTypeId = vm.SecondaryTypeId;

            await _pokemonRepository.AddAsync(pokemon);
        }


        //Update
        public async Task Update(PokemonViewModel vm)
        {
            Pokemon pokemon = new();
            pokemon.Id = vm.Id;
            pokemon.Name = vm.Name;
            pokemon.Foto = vm.Foto;
            pokemon.RegionId = vm.RegionId;
            pokemon.PrimaryTypeId = vm.PrimaryTypeId;
            pokemon.SecondaryTypeId = vm.SecondaryTypeId;

            await _pokemonRepository.UpdateAsync(pokemon);
        }


        //Delete
        public async Task Delete(int id)
        {
            var pokemon = await _pokemonRepository.GetByIdAsync(id);
            await _pokemonRepository.DeleteAsync(pokemon);
        }
    }
}
