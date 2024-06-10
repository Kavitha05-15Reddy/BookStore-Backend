using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public object PlaceOrder(int cartId);
        public object GetAllOrders();
        public object DeleteOrder(int orderId);
        public object GetAllOrdersByUserId(int userId);
    }
}
