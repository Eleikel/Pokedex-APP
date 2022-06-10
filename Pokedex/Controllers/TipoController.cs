using Application.Services;
using Application.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    public class TipoController : Controller
    {
        public readonly TipoService _tipoService;

        public TipoController(ApplicationDbContext dbContext)
        {
            _tipoService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _tipoService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            return View("CreateAndUpdate", new TipoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TipoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAndUpdate", vm);
            }
            TempData["mensaje"] = "El Típo se ha creado correctamente";

            await _tipoService.Add(vm);
            return RedirectToRoute(new { controller = "Tipo", action = "Index" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            return View("CreateAndUpdate", await _tipoService.GetByIdViewModel(id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(TipoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAndUpdate", vm);
            }

            TempData["mensaje"] = "La region se ha actualizado correctamente";

            await _tipoService.Update(vm);
            return RedirectToRoute(new { controller = "Tipo", action = "Index" });

        }


        public async Task<IActionResult> Delete(int id)
        {
            return View(await _tipoService.GetByIdViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            TempData["mensaje"] = "La region se ha eliminado correctamente";

            await _tipoService.Delete(id);
            return RedirectToRoute(new { controller = "Tipo", action = "Index" });
        }
    }
}
