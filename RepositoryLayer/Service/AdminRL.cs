using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class AdminRL : IAdminRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public AdminRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //GenerateToken
        private string GenerateToken(int adminId, string adminEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("EmailId",adminEmail),
                new Claim("AdminId", adminId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Login
        public LoginTokenModel AdminLogin(LoginModel model)
        {
            AdminEntity admin = new AdminEntity();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Admin_Login_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Password", model.Password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        admin.AdminId = (int)reader["AdminId"];
                        admin.FullName = (string)reader["FullName"];
                        admin.EmailId = (string)reader["EmailId"];
                        admin.Password = (string)reader["Password"];
                        admin.MobileNo = (string)reader["MobileNo"];
                    }
                    if (admin.EmailId == model.EmailId && admin.Password == model.Password)
                    {
                        LoginTokenModel login = new LoginTokenModel();
                        var token = GenerateToken(admin.AdminId, admin.EmailId);
                        login.Id = admin.AdminId;
                        login.FullName = admin.FullName;
                        login.EmailId = admin.EmailId;
                        login.Password = admin.Password;
                        login.MobileNo = admin.MobileNo;
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
    }
}
