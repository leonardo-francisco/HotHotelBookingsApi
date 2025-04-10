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
    public class BookingRepository : IBookingRepository
    {
        private readonly IMongoCollection<Booking> _collection;

        public BookingRepository(MongoDbContext context)
        {
            _collection = context.Bookings;
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("ID inválido.", nameof(id));

            await _collection.DeleteOneAsync(c => c.Id == objectId);
        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByClientIdAsync(string clientId)
        {
            var bookings = await _collection.Find(c => c.ClientId == clientId).ToListAsync();
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetByHotelAndRoomAsync(string hotelId, string roomId)
        {
            var filter = Builders<Booking>.Filter.Eq(b => b.HotelId, hotelId) &
                 Builders<Booking>.Filter.Eq(b => b.RoomId, roomId);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByHotelIdAsync(string hotelId)
        {
            var bookings = await _collection.Find(c => c.HotelId == hotelId).ToListAsync();
            return bookings;
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            var booking = await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetByRoomIdAsync(string roomId)
        {
            var bookings = await _collection.Find(c => c.RoomId == roomId).ToListAsync();
            return bookings;
        }

        public async Task<Booking> InsertAsync(Booking entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<Booking> UpdateAsync(string id, Booking entity)
        {
            var filter = Builders<Booking>.Filter.Eq(c => c.Id, entity.Id);
            var update = Builders<Booking>.Update
                .Set(c => c.CheckIn, entity.CheckIn)
                .Set(c => c.CheckOut, entity.CheckOut)
                .Set(c => c.Status, entity.Status)
                .Set(c => c.PaymentStatus, entity.PaymentStatus)
                .Set(c => c.AdditionalService, entity.AdditionalService);

            await _collection.UpdateOneAsync(filter, update);
            var updatedBooking = await _collection.Find(filter).FirstOrDefaultAsync();
            return updatedBooking!;
        }
    }
}
