using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IAddressBL
    {
        public object AddAddress(AddressModel model);
        public object GetAllAddresses();
        public object UpdateAddress(AddressEntity address);
        public object DeleteAddress(int addressId);
        public object GetAddressByUserId(int userId);
    }
}
