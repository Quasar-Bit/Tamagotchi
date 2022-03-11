using Microsoft.AspNetCore.Mvc;
using TamagotchiWeb.Data;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services;
using TamagotchiWeb.Services.DTOs.OutPut.Common;
using TamagotchiWeb.Services.Interfaces;

namespace TamagotchiWeb.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationService _animalService;
        private readonly Context _db;
        public OrganizationsController(Context db)
        {
            _db = db;
            _animalService = new OrganizationService();
        }
        public IActionResult Index()
        {
            var result = new GetOrganizations
            {
                Organizations = _db.Organizations,
                Pagination = new Pagination
                {
                    total_count = _db.Organizations.Count()
                }
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Synch(string gg)
        {
            var firstRequest = await _animalService.GetOrganizations(1);

            for (int i = 801; i <= 900; i++)
            {
                var answer = await _animalService.GetOrganizations(i);

                foreach (var item in answer.Organizations)
                {
                    _db.Organizations.Add(item);
                }

                _db.SaveChanges();
            }


            return RedirectToAction("index");
        }
    }
}
