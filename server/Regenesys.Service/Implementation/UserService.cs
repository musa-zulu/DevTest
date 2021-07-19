using Microsoft.EntityFrameworkCore;
using Regenesys.Domain.Entities;
using Regenesys.Persistence;
using Regenesys.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Regenesys.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _dataContext;
        public UserService(IApplicationDbContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            _dataContext.Users.Add(user);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user == null)
                return false;

            _dataContext.Users.Remove(user);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _dataContext.Users
               .AsNoTracking()
               .SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dataContext.Users
                  .OrderByDescending(x => x.DateStamp)
                  .ThenByDescending(x => x.FirstName)
                  .AsNoTracking()
                  .ToListAsync() ?? new List<User>();
        }

        public async Task<bool> UpdateUserAsync(User userToUpdate)
        {
            _dataContext.Users.Update(userToUpdate);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
