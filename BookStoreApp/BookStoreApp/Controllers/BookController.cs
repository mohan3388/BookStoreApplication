using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
       public IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookPostModel bookPostModel)
        {
            try
            {
                var result = bookBL.AddBook(bookPostModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully...!" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Added UnSuccessfully..." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Get All Books Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Get All Book Unsuccessfull", });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateBook")]
        public IActionResult updateBook(int BookId, BookPostModel bookPostModel)
        {
            try
            {
                var result = this.bookBL.UpdateBook(BookId, bookPostModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Book Not Updated " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(BookId);
                if (result != 0)
                {
                    return this.Ok(new { success = true, message = "Books Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Books Deleted UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
