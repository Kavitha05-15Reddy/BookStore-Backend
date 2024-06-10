using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL iaddressBL;
        public AddressController(IAddressBL iaddressBL)
        {
            this.iaddressBL = iaddressBL;
        }

        //AddAddress
        [HttpPost]
        [Route("AddAddress")]
        [Authorize(Roles = Role.User)]
        public IActionResult AddAddress(AddressModel model)
        {
            try
            {
                var result = iaddressBL.AddAddress(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Address Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Add Address failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllAddresses
        [HttpGet]
        [Route("GetAllAddresses")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                var result = iaddressBL.GetAllAddresses();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Addresses Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get All Addresses failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
 
        //UpdateAddress
        [HttpPut]
        [Route("UpdateAddress")]
        [Authorize(Roles = Role.User)]
        public IActionResult UpdateAddress(AddressEntity address)
        {
            try
            {
                var result = iaddressBL.UpdateAddress(address);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Update Address Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Update Address failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //DeleteAddress
        [HttpDelete]
        [Route("DeleteAddress")]
        [Authorize(Roles = Role.User)]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                var result = iaddressBL.DeleteAddress(addressId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Delete Address Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Delete Address failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        //GetAddressByUserId
        [HttpGet]
        [Route("GetAddress_ByUserId")]
        [Authorize(Roles = Role.User)]
        public IActionResult GetAddressByUserId(int userId)
        {
            try
            {
                var result = iaddressBL.GetAddressByUserId(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get Address By UserId Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get Address By UserId failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
