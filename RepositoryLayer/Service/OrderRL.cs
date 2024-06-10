using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Service
{
    public class OrderRL : IOrderRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public OrderRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //PlaceOrder
        public object PlaceOrder(int cartId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Place_Order_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", cartId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return cartId;
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

        //GetAllOrders
        public object GetAllOrders()
        {
            List<OrderEntity> orders = new List<OrderEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAll_Orders_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderEntity order = new OrderEntity();
                        order.OrderId = (int)reader["OrderId"];
                        order.BookId = (int)reader["BookId"];
                        order.UserId = (int)reader["UserId"];
                        order.Title = (string)reader["Title"];
                        order.Author = (string)reader["Author"];
                        order.Image = (string)reader["Image"];
                        order.Quantity = (int)reader["Quantity"];
                        order.TotalPrice = (decimal)reader["TotalPrice"];
                        order.TotalOriginalPrice = (decimal)reader["TotalOriginalPrice"];
                        order.OrderDateTime = (DateTime)reader["OrderDateTime"];

                        orders.Add(order);
                    }
                    return orders;
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

        //DeleteOrder
        public object DeleteOrder(int orderId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete_Order_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return orderId;
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

        //GetAllOrdersByUserId
        public object GetAllOrdersByUserId(int userId)
        {
            List<OrderEntity> orders = new List<OrderEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllOrders_ByUserId_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderEntity order = new OrderEntity();
                        order.OrderId = (int)reader["OrderId"];
                        order.BookId = (int)reader["BookId"];
                        order.UserId = (int)reader["UserId"];
                        order.Title = (string)reader["Title"];
                        order.Author = (string)reader["Author"];
                        order.Image = (string)reader["Image"];
                        order.Quantity = (int)reader["Quantity"];
                        order.TotalPrice = (decimal)reader["TotalPrice"];
                        order.TotalOriginalPrice = (decimal)reader["TotalOriginalPrice"];
                        order.OrderDateTime = (DateTime)reader["OrderDateTime"];

                        orders.Add(order);
                    }
                    return orders;
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
    }
}
