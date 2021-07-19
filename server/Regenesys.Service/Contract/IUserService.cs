using Regenesys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Regenesys.Service.Contract
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<bool> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<bool> UpdateUserAsync(User userToUpdate);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
