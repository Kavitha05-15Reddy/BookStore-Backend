using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL icartBL;
        public CartController(ICartBL icartBL)
        {
            this.icartBL = icartBL;
        }

        //AddBookToCart
        [HttpPost]
        [Route("AddBook_ToCart")]
        [Authorize(Roles = Role.User)]
        public IActionResult AddBookToCart(CWModel model)
        {
            try
            {
                var result = icartBL.AddBookToCart(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Book To Cart Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Add Book To Cart failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //ViewCart
        [HttpGet]
        [Route("ViewCart")]
        public IActionResult ViewCart()
        {
            try
            {
                var result = icartBL.ViewCart();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "View Cart Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "View Cart failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //UpdateCart
        [HttpPut]
        [Route("UpdateCart")]
        [Authorize(Roles = Role.User)]
        public IActionResult UpdateCart(int cartId, int quantity)
        {
            try
            {
                var result = icartBL.UpdateCart(cartId, quantity);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Update Cart Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Update Cart failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //RemoveBookFromCart
        [HttpDelete]
        [Route("RemoveBook_FromCart")]
        [Authorize(Roles = Role.User)]
        public IActionResult RemoveBookFromCart(int cartId)
        {
            try
            {
                var result = icartBL.RemoveBookFromCart(cartId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Remove Book From Cart Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Remove Book From Cart failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //ViewCartByUserId
        [HttpGet]
        [Route("ViewCart_ByUserId")]
        [Authorize(Roles = Role.User)]
        public IActionResult ViewCartByUserId(int userId)
        {
            try
            {
                var result = icartBL.ViewCartByUserId(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "View Cart By UserId Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "View Cart By UserIdfailed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Review
        //CountOfBooksInCartByUserId
        [HttpGet]
        [Route("CountOfBooks_InCart_ByUserId")]
        public IActionResult CountOfBooksInCartByUserId(int userId)
        {
            try
            {
                var result = icartBL.CountOfBooksInCartByUserId(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "View Cart By UserId Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "View Cart By UserIdfailed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
