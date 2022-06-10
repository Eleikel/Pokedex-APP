using Application.Services;
using Application.ViewModels;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    public class PokemonController : Controller
    {
        public readonly PokemonService _pokemonService;
        ApplicationDbContext _dbContext;

        public PokemonController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _pokemonService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pokemonService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            ViewBags();

            return View("CreateAndUpdate", new PokemonViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PokemonViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBags();

                return View("CreateAndUpdate", vm);
            }

            await _pokemonService.Add(vm);

            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            ViewBags();            

            return View("CreateAndUpdate", await _pokemonService.GetByIdViewModel(id));

        }


        [HttpPost]
        public async Task<IActionResult> Edit(PokemonViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBags();

                return View("CreateAndUpdate", vm);
            }
            
            await _pokemonService.Update(vm);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });

        }


        public async Task<IActionResult> Delete(int id)
        {
            return View(await _pokemonService.GetByIdViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {

            await _pokemonService.Delete(id);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }



        public void ViewBags()
        {
            //Combobox Regiones
            ViewBag.Regiones = new SelectList(_dbContext.Region, "Id", "Name");

            //Combobox TipoPrimario
            ViewBag.PrimaryType = new SelectList(_dbContext.Types, "Id", "TypeName");


            //Combobox TipoSecundario
            ViewBag.SecondaryType = new SelectList(_dbContext.Types, "Id", "TypeName");
        }
    }
}
