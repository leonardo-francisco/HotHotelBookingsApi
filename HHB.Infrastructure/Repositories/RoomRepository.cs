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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HHB.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IMongoCollection<Room> _collection;

        public RoomRepository(MongoDbContext context)
        {
            _collection = context.Rooms;
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("ID inválido.", nameof(id));

            await _collection.DeleteOneAsync(c => c.Id == objectId);
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(string hotelId)
        {
            var rooms = await _collection.Find(c => c.HotelId == hotelId && c.IsAvailable == true).ToListAsync();
            return rooms;
        }

        public async Task<IEnumerable<Room>> GetByHotelIdAsync(string hotelId)
        {
            if (!ObjectId.TryParse(hotelId, out ObjectId objectId))
                return null;

            var rooms = await _collection.Find(c => c.HotelId == objectId.ToString()).ToListAsync();
            return rooms;
        }

        public async Task<Room> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            var customer = await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Room> InsertAsync(Room entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<Room> UpdateAsync(string id, Room entity)
        {
            var filter = Builders<Room>.Filter.Eq(c => c.Id, entity.Id);
            var update = Builders<Room>.Update
                .Set(c => c.Name, entity.Name)
                .Set(c => c.Number, entity.Number)
                .Set(c => c.RoomType, entity.RoomType)
                .Set(c => c.Description, entity.Description)
                .Set(c => c.Capacity, entity.Capacity)
                .Set(c => c.PricePerNight, entity.PricePerNight)
                .Set(c => c.IsAvailable, entity.IsAvailable);

            await _collection.UpdateOneAsync(filter, update);
            var updatedRoom = await _collection.Find(filter).FirstOrDefaultAsync();
            return updatedRoom!;
        }
    }
}
