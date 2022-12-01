using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : Controller
    {
        private readonly IExercise _exerciseRepo;


        public ExerciseController(IExercise exerciseRepository)
        {
            _exerciseRepo = exerciseRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult Get()
        {

            var exercises = _exerciseRepo.GetAllExercises();
            return Ok(exercises);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public Exercise GetExerciseById(int id)
        {
            return _exerciseRepo.GetExerciseById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public Exercise CreateLearning(Exercise exercise)
        {
            var newExercise = _exerciseRepo.CreateExercise(exercise);

            return newExercise;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateExercise(Exercise exercise)
        {
            _exerciseRepo.UpdateExercise(exercise);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteExercise(int id)

        {

            _exerciseRepo.DeleteExercise(id);
        }

    }
}