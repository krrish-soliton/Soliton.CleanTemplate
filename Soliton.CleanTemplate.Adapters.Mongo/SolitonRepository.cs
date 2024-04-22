using MongoDB.Driver;
using Soliton.CleanTemplate.Core;
using Soliton.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soliton.CleanTemplate.Adapters.Mongo
{
    public class SolitonRepository(IMongoDatabase _mongoDatabase) : ISolitonRepository
    {
        private readonly IMongoCollection<SolitonUser> _users = _mongoDatabase.GetCollection<SolitonUser>("SolitonRepo");

        public async Task<MessageHolder> CreateUserAsync(SolitonUser user)
        {
            try
            {
                await _users.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }
            return true;
        }

        public async Task<MessageHolder> DeleteUserAsync(string id)
        {
            try
            {
                await _users.DeleteOneAsync(Builders<SolitonUser>.Filter.Eq(i => i.Id, id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }
            return true;
        }

        public async Task<MessageHolder<List<SolitonUser>>> GetAllUsers()
        {
            try
            {
                return (await _users.FindAsync(Builders<SolitonUser>.Filter.Empty)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }
        }

        public async Task<MessageHolder<SolitonUser?>> GetUserById(string id)
        {
            try
            {
                return (await _users.FindAsync(Builders<SolitonUser>.Filter.Eq(i => i.Id, id))).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }
        }

        public async Task<MessageHolder> UpdateUserAsync(SolitonUser user)
        {
            try
            {
                await _users.ReplaceOneAsync(Builders<SolitonUser>.Filter.Eq(i => i.Id, user.Id), user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message;
            }
            return true;
        }
    }
}
