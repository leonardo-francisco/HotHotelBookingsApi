using HHB.Domain.Contracts;
using HHB.Domain.Entities;
using HHB.Infrastructure.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HHB.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IMongoCollection<Hotel> _collection;

        public HotelRepository(MongoDbContext context)
        {
            _collection = context.Hotels;
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("ID inválido.", nameof(id));

            await _collection.DeleteOneAsync(c => c.Id == objectId);
        }

        public async Task<IEnumerable<Hotel>> GetActiveHotelsAsync()
        {
            var hotel = await _collection.Find(c => c.ClosedYear == null).ToListAsync();
            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Hotel> GetByEmailAsync(string email)
        {
            var hotel = await _collection.Find(c => c.Email == email).FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<Hotel> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            var hotel = await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<Hotel> GetByNameAsync(string name)
        {
            var hotel = await _collection.Find(c => c.Name == name).FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<Hotel> InsertAsync(Hotel entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<Hotel> UpdateAsync(string id, Hotel entity)
        {
            var filter = Builders<Hotel>.Filter.Eq(c => c.Id, entity.Id);
            var update = Builders<Hotel>.Update
                .Set(c => c.Name, entity.Name)
                .Set(c => c.Email, entity.Email)
                .Set(c => c.Address, entity.Address)
                .Set(c => c.FoundedYear, entity.FoundedYear)
                .Set(c => c.ClosedYear, entity.ClosedYear)
                .Set(c => c.Description, entity.Description)
                .Set(c => c.Rooms, entity.Rooms);

            await _collection.UpdateOneAsync(filter, update);
            var updatedHotel = await _collection.Find(filter).FirstOrDefaultAsync();
            return updatedHotel!;
        }
    }
}
