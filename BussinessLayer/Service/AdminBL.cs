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
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL iadminRL;
        public AdminBL(IAdminRL iadminRL)
        {
            this.iadminRL = iadminRL;
        }

        //Login
        public LoginTokenModel AdminLogin(LoginModel model)
        {
            return iadminRL.AdminLogin(model);
        }
    }
}
