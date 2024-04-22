using Soliton.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soliton.CleanTemplate.Core
{
    public interface ISolitonService
    {
        Task<MessageHolder<List<SolitonUser>>> GetAllUsers();

        Task<MessageHolder<SolitonUser?>> GetUserById(string id);

        Task<MessageHolder<SolitonUser?>> CreateUserAsync(SolitonUser user);

        Task<MessageHolder<SolitonUser?>> UpdateUserAsync(SolitonUser user);

        Task<MessageHolder> DeleteUserAsync(string id);
    }
}
