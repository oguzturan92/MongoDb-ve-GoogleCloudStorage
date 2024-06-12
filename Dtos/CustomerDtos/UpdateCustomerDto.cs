using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDbECommerce.Entity;

namespace MongoDbECommerce.Dtos.CustomerDtos
{
    public class UpdateCustomerDto
    {
        public string CustomerId { get; set; }
        public string CustomerFullname { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public List<Order> Orders { get; set; }
    }
}