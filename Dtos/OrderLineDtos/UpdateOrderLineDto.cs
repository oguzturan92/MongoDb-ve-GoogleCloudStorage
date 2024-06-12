using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDbECommerce.Entity;

namespace MongoDbECommerce.Dtos.OrderLineDtos
{
    public class UpdateOrderLineDto
    {
        public string OrderLineId { get; set; }
        public int OrderLineCount { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}