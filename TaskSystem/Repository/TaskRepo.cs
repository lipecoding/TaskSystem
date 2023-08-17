using Microsoft.EntityFrameworkCore;
using TaskSystem.Data;
using TaskSystem.Model;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Repository
{
    public class TaskRepo : ITaskRepo
    {
        private readonly TaskSysDBContext _dbContext;
        public TaskRepo(TaskSysDBContext taskSysDBContext) 
        {
            _dbContext = taskSysDBContext;
        }
        public async Task<List<TaskModel>> FindAllTasks()
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<List<TaskModel>> FindAllTasksByUser(int userId)
        {
            return await _dbContext.Tasks.Where(x => x.UserId == userId)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TaskModel> FindTaskById(int id)
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TaskModel> FindTaskByName(string name)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<TaskModel> AddTask(TaskModel task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> UpdateTask(TaskModel task, int id)
        {
            TaskModel SearchTaskbyId = await FindTaskById(id);

            if (SearchTaskbyId == null)
            {
                throw new Exception($"Task ID: {id} Unknown!");
            }

            SearchTaskbyId.Name = task.Name;
            SearchTaskbyId.Description = task.Description;
            SearchTaskbyId.Status = task.Status;
            SearchTaskbyId.UserId = task.UserId; 


            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();

            return SearchTaskbyId;
        }

        public async Task<bool> DeleteTask(int id)
        {
            TaskModel SearchTaskbyId = await FindTaskById(id);

            if (SearchTaskbyId == null)
            {
                throw new Exception($"Task ID: {id} Unknown!");
            }

            _dbContext.Remove(SearchTaskbyId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
