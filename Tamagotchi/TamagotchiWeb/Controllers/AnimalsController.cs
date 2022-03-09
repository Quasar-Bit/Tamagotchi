using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Data;
using TamagotchiWeb.Entities;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services;

namespace TamagotchiWeb.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly Context _db;
        public AnimalsController(Context db)
        {
            _db = db;
            _animalService = new AnimalService();
        }
        public async Task<IActionResult> IndexAsync()
        {
            //IEnumerable<Animal> animals = _db.Animals;
            var result = await _animalService.GetAnimals();

            return View(result);
        }
    }
}
