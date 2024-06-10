using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserModel UserRegistration(UserModel model);
        public object GetAllUsers();
        public object UpdateUser(int userId, UserModel model);
        public object DeleteUser(int userId);
        public LoginTokenModel UserLogin(LoginModel model);
        public ForgotPasswordModel ForgotPassword(string emailId);
        public bool ResetPassword(string email, string password);

    }
}
