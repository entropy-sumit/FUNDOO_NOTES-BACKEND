﻿using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult addUser(UserRegModel userRegModel)
        {
            try
            {
                var result = userBL.Registration(userRegModel);
                if (result != null) 
                {
                    return this.Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("AllLogin")]
        public IActionResult UserLogin(UserLoginmodel logindata)
        {
            try
            {
                var result = userBL.UserLogin(logindata);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Login Unsuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = userBL.ForgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new { isSuccess = true, message = "Send Forget Password Link" });
                }
                else
                    return this.BadRequest(new { isSuccess = false, message = "Email not Found" });
            }
            catch (Exception e)
            {

                return this.BadRequest(new { isSuccess = false, message = e.InnerException.Message });
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]

        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                //var email = User.Claims.First(e => e.Type == "Email").Value;
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(email, password, confirmPassword);
                return this.Ok(new { isSuccess = true, message = "Reset Password Successfully" });

            }
            catch (Exception e)
            {

                return this.BadRequest(new { isSuccess = false, message = e.InnerException.Message });
            }
        }

    }
}