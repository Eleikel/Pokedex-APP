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
    public class TipoService
    {
        private readonly TipoRepository _tipoRepository;
        public readonly PokemonRepository _pokemonRepository;

        public TipoService(ApplicationDbContext DbContext)
        {
            _tipoRepository = new(DbContext);
            _pokemonRepository = new(DbContext);
        }


        //Add
        public async Task Add(TipoViewModel vm)
        {
            Tipo tipo = new();
            tipo.TypeName = vm.TypeName;

            await _tipoRepository.AddAsync(tipo);
        }

        //Update
        public async Task Update(TipoViewModel vm)
        {
            Tipo tipo = new();
            tipo.Id = vm.Id;
            tipo.TypeName = vm.TypeName;

            await _tipoRepository.UpdateAsync(tipo);
        }

        //Delete
        public async Task Delete(int id)
        {
            var tipo = await _tipoRepository.GetByIdAsync(id);

            //Asignar 'No Type' cuando se elimina el tipo secundario
            List<Pokemon> getAllPokemons = await _pokemonRepository.GetAllAsync();
            foreach (var pokemons in getAllPokemons)
            {
                if (pokemons.SecondaryTypeId == id)
                {
                     pokemons.SecondaryTypeId = 18;
                     //await _pokemonRepository.DeleteAsync(pokemons);
                }
            }

            await _tipoRepository.DeleteAsync(tipo);
        }


        public async Task<List<TipoViewModel>> GetAllViewModel()
        {
            var tipoList = await _tipoRepository.GetAllAsync();
            return tipoList.Select(tipo => new TipoViewModel 
            {
                Id = tipo.Id,
                TypeName = tipo.TypeName
            }).ToList();
        }

        public async Task<TipoViewModel> GetByIdViewModel(int id)
        {
            var tipo = await _tipoRepository.GetByIdAsync(id);

            TipoViewModel tipoView = new();
            tipoView.Id = tipo.Id;
            tipoView.TypeName = tipo.TypeName;

            return tipoView;
        }

    }
}
