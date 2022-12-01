using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;
using UnlimitedImprovement.Repositories;
using Microsoft.AspNetCore.Mvc;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnlimitedImprovement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _userRepo;


        public UserController(IUser userRepository)
        {
            _userRepo = userRepository;
        }



        // GET: api/<MealProductController>
        [HttpGet]
        public ActionResult GetAllUsers()
        {

            var users = _userRepo.GetAllUsers();
            return Ok(users);

        }

        // GET api/<MealProductController>/5
        [HttpGet("{id}")]
        public User GetUserById(int id)
        {
            return _userRepo.GetUserById(id);

        }

        // POST api/<MealProductController>
        [HttpPost]
        public User CreateUser(User user)
        {
            var newUser = _userRepo.CreateUser(user);

            return newUser;
        }

        // PUT api/<MealProductController>/5
        [HttpPut("{id}")]
        public void UpdateUser(User user)
        {
            _userRepo.UpdateUser(user);
        }

        // DELETE api/<MealProductController>/5
        [HttpDelete("{id}")]
        public void DeleteUser(int id)

        {

            _userRepo.DeleteUser(id);
        }

    }
}