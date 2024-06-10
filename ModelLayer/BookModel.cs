using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class BookModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal OriginalPrice { get; set; }
        public float DiscountPercentage { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
