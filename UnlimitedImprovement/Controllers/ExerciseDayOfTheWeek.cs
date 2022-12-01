using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseDayOfTheWeekController : Controller
    {
        private readonly IExerciseDayOfTheWeek _exerciseDayOfTheWeekRepo;


        public ExerciseDayOfTheWeekController(IExerciseDayOfTheWeek exerciseDayOfTheWeekRepository)
        {
            _exerciseDayOfTheWeekRepo = exerciseDayOfTheWeekRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllExerciseDayOfTheWeek()
        {

            var exerciseDayOfTheWeeks = _exerciseDayOfTheWeekRepo.GetAllExerciseDayOfTheWeek();
            return Ok(exerciseDayOfTheWeeks);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public ExerciseDayOfTheWeek GetExerciseDayOfTheWeekById(int id)
        {
            return _exerciseDayOfTheWeekRepo.GetExerciseDayOfTheWeekById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public ExerciseDayOfTheWeek CreateExerciseDayOfTheWeek(ExerciseDayOfTheWeek exerciseDayOfTheWeek)

        {
            var newExerciseDayOfTheWeek = _exerciseDayOfTheWeekRepo.CreateExerciseDayOfTheWeek(exerciseDayOfTheWeek);

            return newExerciseDayOfTheWeek;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateExerciseDayOfTheWeek(ExerciseDayOfTheWeek exerciseDayOfTheWeek)
        {
            _exerciseDayOfTheWeekRepo.UpdateExerciseDayOfTheWeek(exerciseDayOfTheWeek);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteExerciseDayOfTheWeek(int id)

        {

            _exerciseDayOfTheWeekRepo.DeleteExerciseDayOfTheWeek(id);
        }

    }
}