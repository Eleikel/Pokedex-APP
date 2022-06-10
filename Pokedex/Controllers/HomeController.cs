using Application.Services;
using Application.ViewModels.Pokemons;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        public readonly PokemonService _pokemonService;
        public RegionService _regionService;
        public ApplicationDbContext _dbcontext;


        public HomeController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
            _regionService = new(_dbcontext);
            _pokemonService = new(_dbcontext);
        }



        public async Task<IActionResult> Index(FilterPokemonViewModel vm, string sortOrder, string searchString)
        {
            ViewBag.Regions = await _regionService.GetAllViewModel();
            return View(await _pokemonService.GetAllViewModelWithFilter(vm));
        }

    }

}