using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionDayOfTheWeekController : Controller
    {
        private readonly INutritionDayOfTheWeek _nutritionDayOfTheWeekRepo;


        public NutritionDayOfTheWeekController(INutritionDayOfTheWeek nutritionDayOfTheWeekRepository)
        {
            _nutritionDayOfTheWeekRepo = nutritionDayOfTheWeekRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllExerciseDayOfTheWeek()
        {

            var nutritionDayOfTheWeeks = _nutritionDayOfTheWeekRepo.GetAllNutritionDayOfTheWeek();
            return Ok(nutritionDayOfTheWeeks);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public NutritionDayOfTheWeek GetNutritionDayOfTheWeekById(int id)
        {
            return _nutritionDayOfTheWeekRepo.GetNutritionDayOfTheWeekById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public NutritionDayOfTheWeek CreateNutritionDayOfTheWeek(NutritionDayOfTheWeek nutritionDayOfTheWeek)

        {
            var newNutritionDayOfTheWeek = _nutritionDayOfTheWeekRepo.CreateNutritionDayOfTheWeek(nutritionDayOfTheWeek);

            return newNutritionDayOfTheWeek;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateNutritionDayOfTheWeek(NutritionDayOfTheWeek nutritionDayOfTheWeek)
        {
            _nutritionDayOfTheWeekRepo.UpdateNutritionDayOfTheWeek(nutritionDayOfTheWeek);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteNutritionDayOfTheWeek(int id)

        {

            _nutritionDayOfTheWeekRepo.DeleteNutritionDayOfTheWeek(id);
        }

    }
}