using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CartEntity
    {
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public int NumberOfBooksInCart { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set;}
        public string Author { get; set;}
        public string Image { get; set;}
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set;}
        public decimal TotalOriginalPrice { get; set; }


    }
}
