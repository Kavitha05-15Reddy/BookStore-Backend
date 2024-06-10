using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class OrderEntity
    {
        public int OrderId { get; set; }
        public int UserId { get; set;}
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalOriginalPrice { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
