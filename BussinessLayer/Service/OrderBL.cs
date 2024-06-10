using BussinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL iorderRL;
        public OrderBL(IOrderRL iorderRL)
        {
            this.iorderRL = iorderRL;   
        }

        //PlaceOrder
        public object PlaceOrder(int cartId)
        {
            return iorderRL.PlaceOrder(cartId); 
        }

        //GetAllOrders
        public object GetAllOrders()
        {
            return iorderRL.GetAllOrders();
        }

        //DeleteOrder
        public object DeleteOrder(int orderId)
        {
            return iorderRL.DeleteOrder(orderId);   
        }

        //GetAllOrdersByUserId
        public object GetAllOrdersByUserId(int userId)
        {
            return iorderRL.GetAllOrdersByUserId(userId);
        }
    }
}
