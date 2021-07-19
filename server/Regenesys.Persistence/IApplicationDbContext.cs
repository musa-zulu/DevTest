using Microsoft.EntityFrameworkCore;
using Regenesys.Domain.Entities;
using System.Threading.Tasks;

namespace Regenesys.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
