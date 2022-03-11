
using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Data;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services;
using TamagotchiWeb.Services.Interfaces;

namespace TamagotchiWeb.Controllers
{
    public class AnimalTypesController : Controller
    {
        private readonly IAnimalTypeService _animalTypeService;
        private readonly Context _db;
        public AnimalTypesController(Context db)
        {
            _db = db;
            _animalTypeService = new AnimalTypeService();
        }
        public IActionResult Index()
        {
            var result = new GetAnimalTypes
            {
                AnimalTypes = _db.AnimalTypes,
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Synch(string gg)
        {
            var answer = await _animalTypeService.GetAnimalTypes();
            //foreach (var item in answer.AnimalTypes)
            //{
            //    _db.AnimalTypes.Add(item);
            //}

            //_db.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
