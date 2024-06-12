using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbECommerce.Entity
{
    public class OrderLine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderLineId { get; set; }
        public int OrderLineCount { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}