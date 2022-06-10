using Application.Services;
using Application.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    public class RegionController : Controller
    {
        private readonly RegionService _regionService;


        public RegionController(ApplicationDbContext dbContext)
        {
            _regionService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _regionService.GetAllViewModel());
        }

        public  IActionResult Create()
        {
            return View("SaveRegion", new RegionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveRegion", vm);
            }
            TempData["mensaje"] = "La region se ha creado correctamente";

            await _regionService.Add(vm);
            return RedirectToRoute(new {controller = "Region", action = "Index"});
        }


        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveRegion", await _regionService.GetByIdSaveViewModel(id));
        }

        [HttpPost] 
        public async Task<IActionResult> Edit(RegionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveRegion", vm);
            }

            TempData["mensaje"] = "La region se ha actualizado correctamente";

            await _regionService.Update(vm);
            return RedirectToRoute(new { controller = "Region", action = "Index" });

        }


        public async Task<IActionResult> Delete(int id)
        {
            return View(await _regionService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            TempData["mensaje"] = "La region se ha eliminado correctamente";

            await _regionService.Delete(id);
            return RedirectToRoute(new { controller = "Region", action = "Index" });
        }
    }
}
