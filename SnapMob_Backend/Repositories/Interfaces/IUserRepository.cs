using System.Threading.Tasks;

namespace SnapMob_Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task BlockUnblockUserAsync(int id);
        Task SoftDeleteUserAsync(int id);
    }
}
