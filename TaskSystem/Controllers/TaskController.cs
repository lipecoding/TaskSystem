using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Model;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepo _taskRepo;
        public TaskController(ITaskRepo taskRepo) 
        {
            _taskRepo = taskRepo;
        }
        [HttpGet("FindAllTasks")]
        public async Task<ActionResult<List<TaskModel>>> FindAllTasks()
        {
            List<TaskModel> Tasks = await _taskRepo.FindAllTasks();
            return Ok(Tasks);
        }
        [HttpGet("FindAllTasksByUser/{userId}")]
        public async Task<ActionResult<List<TaskModel>>> FindAllTasksByUser(int userId)
        {
            List<TaskModel> Tasks = await _taskRepo.FindAllTasksByUser(userId);
            return Ok(Tasks);
        }
        [HttpGet("FindTaskById/{Id}")]
        public async Task<ActionResult<TaskModel>> FindTaskById(int Id)
        {
            TaskModel task = await _taskRepo.FindTaskById(Id);
            return Ok(task);
        }
        [HttpGet("FindTaskByName/{Name}")]
        public async Task<ActionResult<TaskModel>> FindTaskByName(string name)
        {
            TaskModel task = await _taskRepo.FindTaskByName(name);
            return Ok(task);
        }

        [HttpPost("AddTask")]

        public async Task<ActionResult<TaskModel>> AddTask([FromBody]TaskModel taskModel)
        {
            TaskModel task = await _taskRepo.AddTask(taskModel);
            return Ok(task);
        }

        [HttpPut("UpdateTask/{Id}")]

        public async Task<ActionResult<TaskModel>> UpdateTask([FromBody] TaskModel taskModel, int Id)
        {
            taskModel.Id = Id;
            TaskModel user = await _taskRepo.UpdateTask(taskModel, Id);
            return Ok(user);
        }

        [HttpDelete("DeleteTask/{Id}")]

        public async Task<ActionResult<TaskModel>> DeleteTask(int Id)
        {
            bool user = await _taskRepo.DeleteTask(Id);
            return Ok(user);
        }
    }
}
