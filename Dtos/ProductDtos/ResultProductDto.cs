using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDbECommerce.Entity;

namespace MongoDbECommerce.Dtos.ProductDtos
{
    public class ResultProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public bool ProductStatus { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}