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
    public class CustomerReviewRepository : ICustomerReviewRepository
    {
        private readonly IMongoCollection<CustomerReview> _collection;

        public CustomerReviewRepository(MongoDbContext context)
        {
            _collection = context.CustomerReviews;
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("ID inválido.", nameof(id));

            await _collection.DeleteOneAsync(c => c.Id == objectId);
        }

        public async Task<IEnumerable<CustomerReview>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<CustomerReview> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            var customer = await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<IEnumerable<CustomerReview>> GetReviewHotelAsync(string hotelId)
        {
            var reviews = await _collection.Find(c => c.HotelId == hotelId).ToListAsync();
            return reviews;
        }

        public async Task<IEnumerable<CustomerReview>> GetReviewRoomAsync(string roomId)
        {
            var reviews = await _collection.Find(c => c.RoomId == roomId).ToListAsync();
            return reviews;
        }

        public async Task<CustomerReview> InsertAsync(CustomerReview entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<CustomerReview> UpdateAsync(string id, CustomerReview entity)
        {
            var filter = Builders<CustomerReview>.Filter.Eq(c => c.Id, entity.Id);
            var update = Builders<CustomerReview>.Update
                .Set(c => c.CustomerName, entity.CustomerName)
                .Set(c => c.ReviewDate, entity.ReviewDate)
                .Set(c => c.Rating, entity.Rating)
                .Set(c => c.Title, entity.Title)
                .Set(c => c.Comment, entity.Comment)
                .Set(c => c.ServiceRating, entity.ServiceRating)
                .Set(c => c.CleanlinessRating, entity.CleanlinessRating)
                .Set(c => c.WouldRecommend, entity.WouldRecommend);

            await _collection.UpdateOneAsync(filter, update);
            var updatedReview = await _collection.Find(filter).FirstOrDefaultAsync();
            return updatedReview!;
        }
    }
}
