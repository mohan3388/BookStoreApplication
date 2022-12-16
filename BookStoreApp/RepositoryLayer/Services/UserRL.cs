using CommonLayer.UserModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL:IUserRL
    {
        private readonly string ConnectionString;
        public UserRL(IConfiguration configuration)
        {

            ConnectionString = configuration.GetConnectionString("BookStoreApp");

        }
        string con = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True";
        public UserRegisterModel userRegistration(UserRegisterModel userRegister)
        {
           
            SqlConnection connection = new SqlConnection(con);
            try
            {
                var Encrypted = EncryptPassword(userRegister.Password);
                SqlCommand command = new SqlCommand("spAddUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FullName", userRegister.FullName);
                command.Parameters.AddWithValue("@Email", userRegister.Email);
                command.Parameters.AddWithValue("@Password", Encrypted);
                command.Parameters.AddWithValue("@Mobile", userRegister.Mobile);
                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();
                if (result != null)
                {
                    return userRegister;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UserLogin(UserLoginModel userLogin)
        {
            SqlConnection sqlConnection = new SqlConnection(con);
            try
            {
                string Password = "";
                long Id = 0;
                SqlCommand cmd = new SqlCommand("spLoginUser", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", userLogin.Email);
                sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                // sqlConnection.Close();
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                    //  string FirstName = Convert.ToString(Dr["FirstName"]);
                    string Email = Convert.ToString(Dr["Email"]);
                    //   Id = Convert.ToInt32(Dr["Id"]);
                    Password = Convert.ToString(Dr["Password"]);


                }
                sqlConnection.Close();
                // var pass = Decrypt_Password(Password);
                // var email = userLogin.Email;
                if (DecryptedPassword(Password) == userLogin.Password)
                {

                    return GenerateJWTToken(userLogin.Email, Id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GenerateJWTToken(string email, long Id)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("ThisismySecretKey");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),

                    new Claim("UserId", Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),

                    SigningCredentials =
                    new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ForgetPassword(string Email)
        {
            SqlConnection connection = new SqlConnection(con);
            try
            {
                long Id = 0;
                SqlCommand cmd = new SqlCommand("spForgetPasswordUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);
                connection.Open();
                var result = cmd.ExecuteNonQuery();
                SqlDataReader sqlData = cmd.ExecuteReader();
                ForgetPasswordModel forgetPass = new ForgetPasswordModel();
                // UserRegisterModel userRegisterModels = new UserRegisterModel();
                if (sqlData.Read())
                {
                    forgetPass.UserId = sqlData.GetInt32("UserId");
                 
                    forgetPass.FullName = sqlData.GetString("FullName");
                    forgetPass.Email = sqlData.GetString("Email");

                }
                if (forgetPass.Email != null)
                {
                    MSMQModel mSMQModel = new MSMQModel();
                    var token = GenerateJWTToken(Email, Id);
                    mSMQModel.sendData2Queue(token);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string Email, ResetPasswordModel resetModel)
        {
            SqlConnection connection = new SqlConnection(con);
            try
            {
                SqlCommand cmd = new SqlCommand("spResetPassword", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (resetModel.Password == resetModel.ConfirmPassword)
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", EncryptPassword(resetModel.Password));
                }
                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string EncryptPassword(string password)
        {
            try
            {
                if (password == null)
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encryptedPassword == null)
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
