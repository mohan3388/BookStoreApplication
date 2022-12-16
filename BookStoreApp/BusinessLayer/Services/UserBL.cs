using BusinessLayer.Interfaces;
using CommonLayer.UserModels;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL:IUserBL
    {
        public IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserRegisterModel userRegistration(UserRegisterModel model)
        {
            try
            {
                return userRL.userRegistration(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UserLogin(UserLoginModel userLogin)
        {
            try
            {
                return userRL.UserLogin(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                return userRL.ForgetPassword(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string Email, ResetPasswordModel resetModel)
        {
            try
            {
                return userRL.ResetPassword(Email, resetModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
