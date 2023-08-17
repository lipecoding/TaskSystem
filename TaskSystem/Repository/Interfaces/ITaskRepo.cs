using TaskSystem.Model;

namespace TaskSystem.Repository.Interfaces
{
    public interface ITaskRepo
    {
        Task<List<TaskModel>> FindAllTasks();
        Task<List<TaskModel>> FindAllTasksByUser(int id);
        Task<TaskModel> FindTaskById(int id);
        Task<TaskModel> FindTaskByName(string name);
        Task<TaskModel> AddTask(TaskModel task);
        Task<TaskModel> UpdateTask(TaskModel task, int id);
        Task<bool> DeleteTask(int userId);
    }
}
