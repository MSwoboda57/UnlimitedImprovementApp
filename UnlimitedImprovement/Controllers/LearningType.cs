using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningTypeController : Controller
    {
        private readonly ILearningType _learningTypeRepo;


        public LearningTypeController(ILearningType learningTypeRepository)
        {
            _learningTypeRepo = learningTypeRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllLearningType()
        {

            var learningTypes = _learningTypeRepo.GetAllLearningTypes();
            return Ok(learningTypes);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public LearningType GetLearningTypeById(int id)
        {
            return _learningTypeRepo.GetLearningTypeById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public LearningType CreateLearningType(LearningType learningType)
        {
            var newLearningType = _learningTypeRepo.CreateLearningType(learningType);

            return newLearningType;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateLearningType(LearningType learningType)
        {
            _learningTypeRepo.UpdateLearningType(learningType);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteLearningType(int id)

        {

            _learningTypeRepo.DeleteLearningType(id);
        }

    }
}