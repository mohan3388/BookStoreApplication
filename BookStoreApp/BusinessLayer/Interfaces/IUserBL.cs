using CommonLayer.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserRegisterModel userRegistration(UserRegisterModel model);
        public string UserLogin(UserLoginModel userLogin);
        public string ForgetPassword(string Email);
        public bool ResetPassword(string Email, ResetPasswordModel resetModel);
    }
}
