using CommonLayer.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserRegisterModel userRegistration(UserRegisterModel userRegister);
        public string UserLogin(UserLoginModel userLogin);
        public string ForgetPassword(string Email);
        public bool ResetPassword(string Email, ResetPasswordModel resetModel);
    }
}
