using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RepositoryLayer.Entity
{
    public class BookEntity
    {
		public int BookId { get; set; }
		public string Title { get; set; }
        public string Author { get; set; }
		public float Rating { get; set; }
		public int RatingCount { get; set; }
		public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public float DiscountPercentage { get; set; }
        public string Description { get; set; }
		public string Image { get; set; }
		public int Quantity { get; set; }
    }
}
