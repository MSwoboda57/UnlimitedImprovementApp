using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionController : Controller
    {
        private readonly INutrition _nutritionRepo;


        public NutritionController(INutrition nutritionRepository)
        {
            _nutritionRepo = nutritionRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllNutrition()
        {

            var nutritions = _nutritionRepo.GetAllNutrition();
            return Ok(nutritions);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public Nutrition GetNutritionById(int id)
        {
            return _nutritionRepo.GetNutritionById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public Nutrition CreateNutrition(Nutrition nutrution)
        {
            var newNutrition = _nutritionRepo.CreateNutrition(nutrution);

            return newNutrition;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateNutrition(Nutrition nutrition)
        {
            _nutritionRepo.UpdateNutrition(nutrition);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteNutrition(int id)

        {

            _nutritionRepo.DeleteNutrition(id);
        }

    }
}