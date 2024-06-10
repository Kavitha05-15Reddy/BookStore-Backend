using BussinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL iaddressRL;
        public AddressBL(IAddressRL iaddressRL)
        {
            this.iaddressRL = iaddressRL;
        }

        //AddAddress
        public object AddAddress(AddressModel model)
        {
            return iaddressRL.AddAddress(model);
        }

        //GetAllAddresses
        public object GetAllAddresses()
        {
            return iaddressRL.GetAllAddresses();
        }

        //UpdateAddress
        public object UpdateAddress(AddressEntity address)
        {
            return iaddressRL.UpdateAddress(address);
        }

        //DeleteAddress
        public object DeleteAddress(int addressId)
        {
            return iaddressRL.DeleteAddress(addressId);
        }

        //GetAddressByUserId
        public object GetAddressByUserId(int userId)
        {
            return iaddressRL.GetAddressByUserId(userId);
        }
    }
}
