using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViceController : Controller
    {
        private readonly IVice _viceRepo;


        public ViceController(IVice viceRepository)
        {
            _viceRepo = viceRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult Get()
        {

            var vices = _viceRepo.GetAllVices();
            return Ok(vices);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public Vice GetViceById(int id)
        {
            return _viceRepo.GetViceById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public Vice CreateVice(Vice vice)
        {
            var newVice = _viceRepo.CreateVice(vice);

            return newVice;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateVice(Vice vice)
        {
            _viceRepo.UpdateVice(vice);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteVice(int id)

        {

            _viceRepo.DeleteVice(id);
        }

    }
}