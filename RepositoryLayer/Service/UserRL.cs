using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public UserRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //Registration
        public UserModel UserRegistration(UserModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("User_Register_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@EmailId",model.EmailId);
                    cmd.Parameters.AddWithValue("@Password",model.Password);
                    cmd.Parameters.AddWithValue("@MobileNo",model.MobileNo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        
        //GetAllUsers
        public object GetAllUsers()
        {
            List<UserEntity> users = new List<UserEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAll_Users_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserEntity user = new UserEntity();
                        user.UserId = (int)reader["UserId"];
                        user.FullName = (string)reader["FullName"];
                        user.EmailId = (string)reader["EmailId"];
                        user.Password = (string)reader["Password"];
                        user.MobileNo = (string)reader["MobileNo"];

                        users.Add(user);
                    }
                    return users;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        //Update
        public object UpdateUser(int userId, UserModel model)
        {
            using(SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Update_User_SP", conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@MobileNo", model.MobileNo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        //Delete
        public object DeleteUser(int userId)
        {
            using(SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete_User_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId",userId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return userId;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        //GenerateToken
        private string GenerateToken(int userId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim("EmailId",userEmail),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Login
        public LoginTokenModel UserLogin(LoginModel model)
        {
            UserEntity user = new UserEntity();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("User_Login_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId",model.EmailId);
                    cmd.Parameters.AddWithValue("@Password", model.Password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserId = (int)reader["UserId"];
                        user.FullName = (string)reader["FullName"];
                        user.EmailId = (string)reader["EmailId"];
                        user.Password = (string)reader["Password"];
                        user.MobileNo = (string)reader["MobileNo"];
                    }
                    if (user.EmailId == model.EmailId && user.Password == model.Password)
                    {
                        LoginTokenModel login = new LoginTokenModel();
                        var token = GenerateToken(user.UserId, user.EmailId);
                        login.Id = user.UserId;
                        login.FullName = user.FullName;
                        login.EmailId = user.EmailId;
                        login.Password = user.Password;
                        login.MobileNo = user.MobileNo;
                        login.Token = token;

                        return login;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        //ForgotPassword
        public ForgotPasswordModel ForgotPassword(string emailId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                UserEntity user = new UserEntity();
                try
                {
                    SqlCommand cmd = new SqlCommand("Forgot_PassWord_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", emailId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserId = (int)reader["UserId"];
                        user.FullName = (string)reader["FullName"];
                        user.EmailId = (string)reader["EmailId"];
                        user.Password = (string)reader["Password"];
                        user.MobileNo = (string)reader["MobileNo"];
                    }
                    if (emailId == user.EmailId)
                    {
                        ForgotPasswordModel model = new ForgotPasswordModel();
                        model.EmailId = user.EmailId;
                        model.UserId = user.UserId;
                        model.Token = GenerateToken(user.UserId, user.EmailId);
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        //ResetPassword
        public bool ResetPassword(string email, string password)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Reset_Password_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId",email);
                    cmd.Parameters.AddWithValue("@NewPassword", password);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }
    }
}
