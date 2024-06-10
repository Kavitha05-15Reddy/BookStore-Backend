using BussinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        //Registration
        public UserModel UserRegistration(UserModel model)
        {
            return iuserRL.UserRegistration(model);
        }

        //GetAllUsers
        public object GetAllUsers()
        {
            return iuserRL.GetAllUsers();
        }

        //UserUpdate
        public object UpdateUser(int userId, UserModel model)
        {
            return iuserRL.UpdateUser(userId, model);
        }

        //UserDelete
        public object DeleteUser(int userId)
        {
            return iuserRL.DeleteUser(userId);
        }

        //Login
        public LoginTokenModel UserLogin(LoginModel model)
        {
            return iuserRL.UserLogin(model);
        }

        //ForgotPassword
        public ForgotPasswordModel ForgotPassword(string emailId)
        {
            return iuserRL.ForgotPassword(emailId);
        }

        //ResetPassword
        public bool ResetPassword(string email, string password)
        {
            return iuserRL.ResetPassword(email, password);
        }


    }
}
