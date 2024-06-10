using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL ibookBL;
        public BookController(IBookBL ibookBL)
        {
            this.ibookBL = ibookBL;
        }

        //AddBook
        [HttpPost]
        [Route("AddBook")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult AddBook(BookModel model)
        {
            try
            {
                var result = ibookBL.AddBook(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Book Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Add Book failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllBooks
        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = ibookBL.GetAllBooks();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Books Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get All Books failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //UpdateBook
        [HttpPut]
        [Route("UpdateBook")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult UpdateBook(int bookId, BookModel model)
        {
            try
            {
                var result = ibookBL.UpdateBook(bookId, model);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Update Book Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Update Book failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //DeleteBook
        [HttpDelete]
        [Route("DeleteBook")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = ibookBL.DeleteBook(bookId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Delete Book Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Delete Book failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetBookByBookId
        [HttpGet]
        [Route("GetBook_ByBookId")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var result = ibookBL.GetBookByBookId(bookId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get Book By BookId Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get Book By BookId failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Review
        //GetBookByName
        [HttpGet]
        [Route("GetBook_ByName")]
        public IActionResult GetBookByName(string bookName, string authorName)
        {
            try
            {
                var result = ibookBL.GetBookByName(bookName, authorName);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get Book By Name Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Get Book By Name failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
