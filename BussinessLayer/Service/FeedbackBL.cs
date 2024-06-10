using BussinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL ifeedbackRL;
        public FeedbackBL(IFeedbackRL ifeedbackRL)
        {
            this.ifeedbackRL = ifeedbackRL;
        }

        //AddFeedback
        public object AddFeedback(FeedbackModel model)
        {
            return ifeedbackRL.AddFeedback(model);
        }

        //GetAllFeedbacks
        public object GetAllFeedbacks()
        {
            return ifeedbackRL.GetAllFeedbacks();
        }

        //UpdateFeedback
        public object UpdateFeedback(UpdateFeebackModel model)
        {
            return ifeedbackRL.UpdateFeedback(model);
        }

        //DeleteFeedback
        public object DeleteFeedback(int feedbackId)
        {
            return ifeedbackRL.DeleteFeedback(feedbackId);
        }

        //GetAll_Feedbacks_ByBookId
        public object GetAll_Feedbacks_ByBookId(int bookId)
        {
            return ifeedbackRL.GetAll_Feedbacks_ByBookId(bookId);
        }
    }
}
