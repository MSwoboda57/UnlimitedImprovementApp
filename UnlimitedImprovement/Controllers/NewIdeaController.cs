using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewIdeaController : Controller
    {
        private readonly INewIdea _newIdeaRepo;


        public NewIdeaController(INewIdea newIdeaRepository)
        {
            _newIdeaRepo = newIdeaRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllNewIdeas()
        {

            var newIdea = _newIdeaRepo.GetAllNewIdeas();
            return Ok(newIdea);

        }


        // POST api/<MealProductController>
        [HttpPost]
        public NewIdeas CreateNewIdea(NewIdeas newIdea)
        {
            var newNewIdea = _newIdeaRepo.CreateNewIdea(newIdea);

            return newNewIdea;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateNewIdea(NewIdeas newIdea)
        {
            _newIdeaRepo.UpdateNewIdea(newIdea);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteNewIdea(int id)

        {

            _newIdeaRepo.DeleteNewIdea(id);
        }

    }
}