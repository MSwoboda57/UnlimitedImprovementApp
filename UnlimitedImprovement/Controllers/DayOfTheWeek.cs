using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayOfTheWeekController : Controller
    {
        private readonly IDayOfTheWeek _dayOfTheWeekRepo;


        public DayOfTheWeekController(IDayOfTheWeek dayOfTheWeekRepository)
        {
            _dayOfTheWeekRepo = dayOfTheWeekRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllDayOfTheWeek()
        {

            var dayOfTheWeeks = _dayOfTheWeekRepo.GetAllDayOfTheWeek();

            return Ok(dayOfTheWeeks);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public DaysOfTheWeek GetDayOfTheWeekById(int id)
        {
            return _dayOfTheWeekRepo.GetDayOfTheWeekById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public DaysOfTheWeek CreateDayOfTheWeek(DaysOfTheWeek dayOfTheWeek)

        {
            var newDayOfTheWeek = _dayOfTheWeekRepo.CreateDayOfTheWeek(dayOfTheWeek);

            return newDayOfTheWeek;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateDayOfTheWeek(DaysOfTheWeek dayOfTheWeek)
        {
            _dayOfTheWeekRepo.UpdateDayOfTheWeek(dayOfTheWeek);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteDayOfTheWeek(int id)

        {

            _dayOfTheWeekRepo.DeleteDayOfTheWeek(id);
        }

    }
}