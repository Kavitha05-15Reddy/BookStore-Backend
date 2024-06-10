using BussinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL iadminBL;
        public AdminController(IAdminBL iadminBL)
        {
            this.iadminBL = iadminBL;
        }

        //Login
        [HttpPost]
        [Route("Login")]
        public IActionResult AdminLogin(LoginModel model)
        {
            try
            {
                var result = iadminBL.AdminLogin(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<LoginTokenModel> { IsSuccess = true, Message = "Login Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<LoginTokenModel> { IsSuccess = false, Message = "Login failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
