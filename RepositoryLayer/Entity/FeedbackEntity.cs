using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class FeedbackEntity
    {
        public int FeedbackId { get; set; }
        public int BookId { get; set; }
        public string UserName { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; }
    }
}
