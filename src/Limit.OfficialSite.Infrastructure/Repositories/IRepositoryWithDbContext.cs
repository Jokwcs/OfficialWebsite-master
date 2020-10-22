using Microsoft.EntityFrameworkCore;

namespace Limit.OfficialSite.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}