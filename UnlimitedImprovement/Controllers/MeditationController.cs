using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeditationController : Controller
    {
        private readonly IMeditation _meditationRepo;


        public MeditationController(IMeditation meditationRepository)
        {
            _meditationRepo = meditationRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllMeditations()
        {

            var meditations = _meditationRepo.GetAllMeditations();
            return Ok(meditations);

        }


        // POST api/<MealProductController>
        [HttpPost]
        public Meditation CreateMeditation(Meditation meditation)
        {
            var newMeditation = _meditationRepo.CreateMeditation(meditation);

            return newMeditation;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateMeditation(Meditation meditation)
        {
            _meditationRepo.UpdateMeditation(meditation);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteMeditation(int id)

        {

            _meditationRepo.DeleteMeditation(id);
        }

    }
}