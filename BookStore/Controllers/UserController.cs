using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }

        //Registration
        [HttpPost]
        [Route("Register")]
        public IActionResult UserRegistration(UserModel model)
        {
            try
            {
                var result = iuserBL.UserRegistration(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserModel> { IsSuccess = true, Message = "Registration Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserModel> { IsSuccess = false, Message = "Registration failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllUsers
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = iuserBL.GetAllUsers();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Users Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get All Users failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Update
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateUser(int userId, UserModel model)
        {
            try
            {
                var result = iuserBL.UpdateUser(userId, model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Update User Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Update User failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Delete
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                var result = iuserBL.DeleteUser(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Delete User Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Delete User failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Login
        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(LoginModel model)
        {
            try
            {
                var result = iuserBL.UserLogin(model);
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

        //ForgotPassword
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string emailId)
        {
            try
            {
                var password = iuserBL.ForgotPassword(emailId);

                if (password != null)
                {
                    Send send = new Send();
                    ForgotPasswordModel forgotPasswordModel = iuserBL.ForgotPassword(emailId);
                    send.SendMail(forgotPasswordModel.EmailId, forgotPasswordModel.Token);
                    Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
                    //var endPoint = await bus.GetSendEndpoint(uri);
                    //await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Mail sent Successfully", Data = password.Token });
                }
                else
                {
                    return NotFound(new ResponseModel<string> { IsSuccess = false, Message = "Email Does not Exist" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //ResetPassword
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string password)
        {
            try
            {
                string email = User.Claims.FirstOrDefault(x => x.Type == "EmailId").Value;
                var result = iuserBL.ResetPassword(email, password);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Password Reset Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Password Reset failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
