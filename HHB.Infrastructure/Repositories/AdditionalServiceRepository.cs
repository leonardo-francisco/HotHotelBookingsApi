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

namespace HHB.Infrastructure.Repositories
{
    public class AdditionalServiceRepository : IAdditionalServiceRepository
    {
        private readonly IMongoCollection<AdditionalService> _collection;

        public AdditionalServiceRepository(MongoDbContext context)
        {
            _collection = context.AdditionalServices;
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("ID inválido.", nameof(id));

            await _collection.DeleteOneAsync(c => c.Id == objectId);
        }

        public async Task<IEnumerable<AdditionalService>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<AdditionalService>> GetAvailableServicesAsync(string hotelId)
        {
            var services = await _collection.Find(x => x.HotelId == hotelId && x.IsAvailable == true).ToListAsync();
            return services;
        }

        public async Task<IEnumerable<AdditionalService>> GetByHotelIdAsync(string hotelId)
        {
            if (!ObjectId.TryParse(hotelId, out ObjectId objectId))
                return null;

            var hotels = await _collection.Find(c => c.HotelId == objectId.ToString()).ToListAsync();
            return hotels;
        }

        public async Task<AdditionalService> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            var hotel = await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<AdditionalService> InsertAsync(AdditionalService entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<AdditionalService> UpdateAsync(string id, AdditionalService entity)
        {
            var filter = Builders<AdditionalService>.Filter.Eq(c => c.Id, entity.Id);
            var update = Builders<AdditionalService>.Update
                .Set(c => c.ServiceName, entity.ServiceName)
                .Set(c => c.Description, entity.Description)
                .Set(c => c.Price, entity.Price)
                .Set(c => c.IsAvailable, entity.IsAvailable);

            await _collection.UpdateOneAsync(filter, update);
            var updatedAdditional = await _collection.Find(filter).FirstOrDefaultAsync();
            return updatedAdditional!;
        }
    }
}
