using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Model;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo) 
        {
            _userRepo = userRepo;
        }
        [HttpGet("FindAllUsers")]
        public async Task<ActionResult<List<UserModel>>> FindAllUsers()
        {
            List<UserModel> Users = await _userRepo.FindAllUsers();
            return Ok(Users);
        }
        [HttpGet("FindById/{Id}")]
        public async Task<ActionResult<UserModel>> FindById(int Id)
        {
            UserModel User = await _userRepo.FindById(Id);
            return Ok(User);
        }
        [HttpGet("FindByName/{Name}")]
        public async Task<ActionResult<UserModel>> FindByName(string name)
        {
            UserModel User = await _userRepo.FindByName(name);
            return Ok(User);
        }
        [HttpGet("FindByEmail/{Email}")]
        public async Task<ActionResult<UserModel>> FindByEmail(string email)
        {
            UserModel User = await _userRepo.FindByEmail(email);
            return Ok(User);
        }

        [HttpPost("AddUser")]

        public async Task<ActionResult<UserModel>> AddUser([FromBody]UserModel userModel)
        {
            UserModel user = await _userRepo.AddUser(userModel);
            return Ok(user);
        }

        [HttpPut("UpdateUser/{Id}")]

        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel userModel, int Id)
        {
            userModel.Id = Id;
            UserModel user = await _userRepo.UpdateUser(userModel, Id);
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{Id}")]

        public async Task<ActionResult<UserModel>> DeleteUser(int Id)
        {
            bool user = await _userRepo.DeleteUser(Id);
            return Ok(user);
        }

        [HttpGet("Login/{Email}-{Password}")]
        public async Task<ActionResult<UserModel>> Login(string Email, string Password)
        {

            bool login = await _userRepo.Login(Email, Password);
            return Ok(login);
        }
    }
}
