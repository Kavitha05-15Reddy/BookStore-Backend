﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class FeedbackModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; }
    }
}
