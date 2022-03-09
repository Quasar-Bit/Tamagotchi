using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Data;
using TamagotchiWeb.Entities;

namespace TamagotchiWeb.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly Context _db;
        public AnimalsController(Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Animal> animals = _db.Animals;
            return View(animals);
        }
    }
}
