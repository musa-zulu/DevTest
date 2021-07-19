using System.Threading.Tasks;

namespace Regenesys.Persistence
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync();
    }
}
