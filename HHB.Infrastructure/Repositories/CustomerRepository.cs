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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _collection;

        public CustomerRepository(MongoDbContext context)
        {
            _collection = context.Customers;
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new ArgumentException("ID inválido.", nameof(id));

            await _collection.DeleteOneAsync(c => c.Id == objectId);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            var customer = await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            var customer = await _collection.Find(c => c.Id == objectId).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Customer> InsertAsync(Customer entity)
        {            
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<Customer> UpdateAsync(string id, Customer entity)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, entity.Id);
            var update = Builders<Customer>.Update
                .Set(c => c.Name, entity.Name)
                .Set(c => c.Email, entity.Email)
                .Set(c => c.Phone, entity.Phone)
                .Set(c => c.Bookings, entity.Bookings);

            await _collection.UpdateOneAsync(filter, update);
           
            var updatedCustomer = await _collection.Find(filter).FirstOrDefaultAsync();
            return updatedCustomer!;
        }
    }
}
