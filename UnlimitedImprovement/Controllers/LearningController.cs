using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningController : Controller
    {
        private readonly ILearning _learningRepo;


        public LearningController(ILearning learningRepository)
        {
            _learningRepo = learningRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult Get()
        {

            var learnings = _learningRepo.GetAllLearning();
            return Ok(learnings);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public Learning GetLearningById(int id)
        {
            return _learningRepo.GetLearningById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public Learning CreateLearning(Learning learning)
        {
            var newLearning = _learningRepo.CreateLearning(learning);

            return newLearning;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateLearning(Learning learning)
        {
            _learningRepo.UpdateLearning(learning);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteLearning(int id)

        {

            _learningRepo.DeleteLearning(id);
        }

    }
}