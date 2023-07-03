using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<int> GetUserIdAsync(User user);
        Task AddAsync(User user);
        Task DeleteAsync(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly TodoDbContext _todoDbContext;

        public UserRepository(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<User> GetById(int id)
        {
            var result = await _todoDbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == id);

            if (result == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            return result;
        }

        public async Task<User> GetUserByUsernameAsync(string? username)
        {
            var result = await _todoDbContext.Set<User>().FirstOrDefaultAsync(u => u.Username == username);

            if (result == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            return result;
        }

        public async Task<int> GetUserIdAsync(User inputUser)
        {
            var user = await _todoDbContext.Set<User>().FirstOrDefaultAsync(u => u.Username == inputUser.Username && u.Password == inputUser.Password);

            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            return user.Id;
        }

        public async Task AddAsync(User user)
        {
            try
            {
                await _todoDbContext.AddAsync(user);
                await _todoDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DuplicateRecordException("Kullanıcı adı zaten kullanımda.");
            }
        }

        public async Task DeleteAsync(User user)
        {
            _todoDbContext.Set<User>().Remove(user);
            await _todoDbContext.SaveChangesAsync();
        }
    }
}
