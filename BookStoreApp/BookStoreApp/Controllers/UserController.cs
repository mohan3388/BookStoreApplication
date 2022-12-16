using BusinessLayer.Interfaces;
using CommonLayer.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;

        }
        [HttpPost("Register")]
        public IActionResult userRegister(UserRegisterModel model)
        {
            try
            {
                var result = userBL.userRegistration(model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registered"});
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(UserLoginModel model)
        {
            try
            {
                var result = userBL.UserLogin(model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "login successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("Forgetpass")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = userBL.ForgetPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "sent successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost("ResetPass")]
        public IActionResult ResetPassword(string Email, ResetPasswordModel resetModel)
        {
            try
            {
                var result = userBL.ResetPassword(Email, resetModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "reset pass successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
