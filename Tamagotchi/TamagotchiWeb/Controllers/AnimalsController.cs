using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Data;
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
        public IActionResult Index()
        {
            var result = new GetAnimals
            {
                Animals = _db.Animals
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Synch(string gg)
        {
            //var firstRequest = await _animalService.GetAnimals(1);

            //for (int i = 1; i <= firstRequest.Pagination.total_pages; i++)
            //{
            //var answer = await _animalService.GetAnimals(1);

            //foreach (var item in answer.Animals)
            //{
            //    _db.Add(item);
            //    //_db.SavedChanges();
            //}
            //}

            var result = new GetAnimals
            {
                Animals = _db.Animals
            };

            await Task.Delay(1000);

            return View(result);
        }
    }
}
