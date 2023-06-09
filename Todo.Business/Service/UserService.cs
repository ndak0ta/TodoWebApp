﻿using Todo.Infrastructure.Repositories;
using Todo.Infrastructure.Exceptions;
using Todo.Data.Models;

namespace Todo.Business.Service
{
    public interface IUserService
    {
        Task<int> GetUserIdAsync(User user);
        Task AddAsync(User user);
        Task DeleteAsync(int userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITodoService _todoService;

        public UserService(IUserRepository userRepository, ITodoService todoService)
        {
            _userRepository = userRepository;
            _todoService = todoService;
        }

        public async Task<int> GetUserIdAsync(User user)
        {
            return await _userRepository.GetUserIdAsync(user);
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _userRepository.GetById(userId);

            await _userRepository.DeleteAsync(user);
            await _todoService.DeleteAllByUserIdAsync(user);
        }
    }
}
