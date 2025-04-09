using HHB.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customers");
        public IMongoCollection<Booking> Bookings => _database.GetCollection<Booking>("Bookings");
        public IMongoCollection<Hotel> Hotels => _database.GetCollection<Hotel>("Hotels");
        public IMongoCollection<Room> Rooms => _database.GetCollection<Room>("Rooms");
        public IMongoCollection<AdditionalService> AdditionalServices => _database.GetCollection<AdditionalService>("AdditionalServices");
        public IMongoCollection<CustomerReview> CustomerReviews => _database.GetCollection<CustomerReview>("CustomerReviews");
    }
}
