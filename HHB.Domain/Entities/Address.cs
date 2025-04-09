using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Entities
{
    public class Address
    {
        [BsonElement("zipCode")]
        public string ZipCode { get; set; }
        [BsonElement("street")]
        public string Street { get; set; }
        [BsonElement("number")]
        public string Number { get; set; }
        [BsonElement("neighborhood")]
        public string Neighborhood { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("state")]
        public string State { get; set; }
    }
}
