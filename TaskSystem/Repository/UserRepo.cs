using Microsoft.EntityFrameworkCore;
using TaskSystem.Data;
using TaskSystem.Model;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly TaskSysDBContext _dbContext;
        public UserRepo(TaskSysDBContext taskSysDBContext) 
        {
            _dbContext = taskSysDBContext;
        }
        public async Task<List<UserModel>> FindAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> FindById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> FindByName(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == username);
        }

        public async Task<UserModel> FindByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserModel> AddUser(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> UpdateUser(UserModel user, int id)
        {
            UserModel SearchbyId = await FindById(id);

            if (SearchbyId == null)
            {
                throw new Exception($"User ID: {id} Unknown!");
            }

            SearchbyId.Name = user.Name;
            SearchbyId.Email = user.Email;
            SearchbyId.Password = user.Password;
            SearchbyId.age = user.age;

            _dbContext.Users.Update(SearchbyId);
            await _dbContext.SaveChangesAsync();

            return SearchbyId;
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel SearchbyId = await FindById(id);

            if (SearchbyId == null)
            {
                throw new Exception($"User ID: {id} Unknown!");
            }

            _dbContext.Remove(SearchbyId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Login(string email, string password)
        {
            UserModel user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                return false;
            }

            return true;
        }

    }
}
