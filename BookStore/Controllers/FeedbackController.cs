using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL ifeedbackBL;
        public FeedbackController(IFeedbackBL ifeedbackBL)
        {
            this.ifeedbackBL = ifeedbackBL;
        }

        //AddFeedback
        [HttpPost]
        [Route("AddFeedback")]
        [Authorize(Roles = Role.User)]
        public IActionResult AddFeedback(FeedbackModel model)
        {
            try
            {
                var result = ifeedbackBL.AddFeedback(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Feedback Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Add Feedback failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllFeedbacks
        [HttpGet]
        [Route("GetAllFeedbacks")]
        public IActionResult GetAllFeedbacks()
        {
            try
            {
                var result = ifeedbackBL.GetAllFeedbacks();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Feedbacks Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get All Feedbacks failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //UpdateFeedback
        [HttpPut]
        [Route("UpdateFeedback")]
        [Authorize(Roles = Role.User)]
        public IActionResult UpdateFeedback(UpdateFeebackModel model)
        {
            try
            {
                var result = ifeedbackBL.UpdateFeedback(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Update Feeback Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Update Feeback failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //DeleteFeedback
        [HttpDelete]
        [Route("DeleteFeedback")]
        [Authorize(Roles = Role.User)]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            try
            {
                var result = ifeedbackBL.DeleteFeedback(feedbackId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Delete Feeback Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Delete Feeback failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAll_Feedbacks_ByBookId
        [HttpGet]
        [Route("GetAll_Feedbacks_ByBookId")]
        public IActionResult GetAll_Feedbacks_ByBookId(int bookId)
        {
            try
            {
                var result = ifeedbackBL.GetAll_Feedbacks_ByBookId(bookId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Feedbacks By BookId Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get All Feedbacks By BookId failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
