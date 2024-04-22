using Soliton.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soliton.CleanTemplate.Core
{
    public interface ISolitonRepository
    {
        Task<MessageHolder<List<SolitonUser>>> GetAllUsers();

        Task<MessageHolder<SolitonUser?>> GetUserById(string id);

        Task<MessageHolder> CreateUserAsync(SolitonUser user);

        Task<MessageHolder> UpdateUserAsync(SolitonUser user);

        Task<MessageHolder> DeleteUserAsync(string id);
    }
}
