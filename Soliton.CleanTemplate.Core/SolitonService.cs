using Soliton.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soliton.CleanTemplate.Core
{
    public class SolitonService(ISolitonRepository _solitonRepository) : ISolitonService
    {
        public async Task<MessageHolder<SolitonUser?>> CreateUserAsync(SolitonUser user)
        {
            if (user.EmployeeId == 0 || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.EmailId))
            {
                return "Invalid Data";
            }
            user.Id = Guid.NewGuid().ToString();
            var result = await _solitonRepository.CreateUserAsync(user);
            return result ? (MessageHolder<SolitonUser?>)user : new MessageHolder<SolitonUser?>(result.ErrorCode, result.Message, false);
        }

        public async Task<MessageHolder> DeleteUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "Id cannot be empty";
            }
            return await _solitonRepository.DeleteUserAsync(id);
        }

        public async Task<MessageHolder<SolitonUser?>> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "Id cannot be empty";
            }
            return await _solitonRepository.GetUserById(id);
        }

        public async Task<MessageHolder<List<SolitonUser>>> GetAllUsers() => await _solitonRepository.GetAllUsers();

        public async Task<MessageHolder<SolitonUser?>> UpdateUserAsync(SolitonUser user)
        {
            if (user.EmployeeId == 0 || string.IsNullOrEmpty(user.Name)
                || string.IsNullOrEmpty(user.EmailId)) return "Invalid details present";
            var result = await _solitonRepository.UpdateUserAsync(user);
            if (result)
            {
                return user;
            }
            else
            {
                return new MessageHolder<SolitonUser?>(result.ErrorCode, result.Message, false);
            }
        }
    }
}
