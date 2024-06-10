using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Reflection;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL iwishlistBL;
        public WishlistController(IWishlistBL iwishlistBL)
        {
            this.iwishlistBL = iwishlistBL;
        }

        //AddBookToWishlist
        [HttpPost]
        [Route("AddBook_ToWishlist")]
        [Authorize(Roles = Role.User)]
        public IActionResult AddBookToWishlist(CWModel model)
        {
            try
            {
                var result = iwishlistBL.AddBookToWishlist(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Book To Wishlist Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Add Book To Wishlist failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //ViewWishlist
        [HttpGet]
        [Route("ViewWishlist")]
        public IActionResult ViewWishlist()
        {
            try
            {
                var result = iwishlistBL.ViewWishlist();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "View Wishlist Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "View Wishlist failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //RemoveBookFromWishlist
        [HttpDelete]
        [Route("RemoveBook_FromWishlist")]
        [Authorize(Roles = Role.User)]
        public IActionResult RemoveBookFromWishlist(int wishlistId)
        {
            try
            {
                var result = iwishlistBL.RemoveBookFromWishlist(wishlistId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Remove Book From Wishlist Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Remove Book From Wishlist failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //ViewWishlistByUserId
        [HttpGet]
        [Route("ViewWishlist_ByUserId")]
        [Authorize(Roles = Role.User)]
        public IActionResult ViewWishlistByUserId(int userId)
        {
            try
            {
                var result = iwishlistBL.ViewWishlistByUserId(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "View Wishlist By UserId Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "View Wishlist By UserId failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
