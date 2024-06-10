using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL iorderBL;
        public OrderController(IOrderBL iorderBL)
        {
            this.iorderBL = iorderBL;
        }

        //PlaceOrder
        [HttpPost]
        [Route("PlaceOrder")]
        [Authorize(Roles = Role.User)]
        public IActionResult PlaceOrder(int cartId)
        {
            try
            {
                var result = iorderBL.PlaceOrder(cartId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Place Order Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Place Order failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllOrders
        [HttpGet]
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var result = iorderBL.GetAllOrders();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Orders Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get All Orders failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //DeleteOrder
        [HttpDelete]
        [Route("DeleteOrder")]
        [Authorize(Roles = Role.User)]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                var result = iorderBL.DeleteOrder(orderId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Delete Order Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Delete Order failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllOrdersByUserId
        [HttpGet]
        [Route("GetAllOrders_ByUserId")]
        [Authorize(Roles = Role.User)]
        public IActionResult GetAllOrdersByUserId(int userId)
        {
            try
            {
                var result = iorderBL.GetAllOrdersByUserId(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Orders By UserId Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get All Orders By UserId failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
