using Microsoft.Extensions.Configuration;
using ModelLayer;
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
    public class CartRL : ICartRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public CartRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //AddBookToCart
        public object AddBookToCart(CWModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddBook_ToCart_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
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

        //ViewCart
        public object ViewCart()
        {
            List<CartEntity> cart = new List<CartEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ViewCart_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CartEntity book = new CartEntity();
                        book.UserId = (int)reader["UserId"];
                        book.BookId = (int)reader["BookId"];
                        book.CartId = (int)reader["CartId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Image = (string)reader["Image"];
                        book.TotalPrice = (decimal)reader["TotalPrice"];
                        book.TotalOriginalPrice = (decimal)reader["TotalOriginalPrice"];
                        book.Quantity = (int)reader["Quantity"];

                        cart.Add(book);
                    }
                    return cart;
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

        //UpdateCart
        public bool UpdateCart(int cartId,int quantity)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Update_Cart_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    cmd.Parameters.AddWithValue("@Quantity",quantity);
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }

        //RemoveBookFromCart
        public object RemoveBookFromCart(int cartId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("RemoveBook_FromCart_SP", conn);
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

        //ViewCartByUserId
        public object ViewCartByUserId(int userId)
        {
            List<CartEntity> cart = new List<CartEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ViewCart_ByUserId_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CartEntity book = new CartEntity();
                        book.UserId = (int)reader["UserId"];
                        book.BookId = (int)reader["BookId"];
                        book.CartId = (int)reader["CartId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Image = (string)reader["Image"];
                        book.TotalPrice = (decimal)reader["TotalPrice"];
                        book.TotalOriginalPrice = (decimal)reader["TotalOriginalPrice"];
                        book.Quantity = (int)reader["Quantity"];

                        cart.Add(book);
                    }
                    return cart;
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

        //Review
        //CountOfBooksInCartByUserId
        public object CountOfBooksInCartByUserId(int userId)
        {
            List<CartEntity> cart = new List<CartEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("CountOfBooks_InCart_ByUserId_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CartEntity book = new CartEntity();
                        book.UserName = (string)reader["UserName"];
                        book.PhoneNo = (string)reader["PhoneNo"];
                        book.NumberOfBooksInCart = (int)reader["NumberOfBooksInCart"];
                        book.CartId = (int)reader["CartId"];
                        book.UserId = (int)reader["UserId"];
                        book.BookId = (int)reader["BookId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Image = (string)reader["Image"];
                        book.Quantity = (int)reader["Quantity"];
                        book.TotalPrice = (decimal)reader["TotalPrice"];
                        book.TotalOriginalPrice = (decimal)reader["TotalOriginalPrice"];

                        cart.Add(book);
                    }
                    return cart;
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
