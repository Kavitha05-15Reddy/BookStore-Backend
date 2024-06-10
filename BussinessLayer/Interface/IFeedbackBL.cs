﻿using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public object AddFeedback(FeedbackModel model);
        public object GetAllFeedbacks();
        public object UpdateFeedback(UpdateFeebackModel model);
        public object DeleteFeedback(int feedbackId);
        public object GetAll_Feedbacks_ByBookId(int bookId);
    }
}