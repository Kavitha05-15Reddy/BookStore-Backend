using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IOrderBL
    {
        public object PlaceOrder(int cartId);
        public object GetAllOrders();
        public object DeleteOrder(int orderId);
        public object GetAllOrdersByUserId(int userId);
    }
}
