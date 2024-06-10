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
using ModelLayer;

namespace RepositoryLayer.Service
{
    public class WishlistRL : IWishlistRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public WishlistRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //AddBookToWishlist
        public object AddBookToWishlist(CWModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddBook_ToWishlist_SP", conn);
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

        //ViewWishlist
        public object ViewWishlist()
        {
            List<WishlistEntity> wishlist = new List<WishlistEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("View_Wishlist_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        WishlistEntity book = new WishlistEntity();
                        book.WishlistId = (int)reader["WishlistId"];
                        book.BookId = (int)reader["BookId"];
                        book.UserId = (int)reader["UserId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Image = (string)reader["Image"];
                        book.Price = (decimal)reader["Price"];
                        book.OriginalPrice = (decimal)reader["OriginalPrice"];

                        wishlist.Add(book);
                    }
                    return wishlist;
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

        //RemoveBookFromWishlist
        public object RemoveBookFromWishlist(int wishlistId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("RemoveBook_FromWishlist_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WishlistId", wishlistId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return wishlistId;
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

        //ViewWishlistByUserId
        public object ViewWishlistByUserId(int userId)
        {
            List<WishlistEntity> wishlist = new List<WishlistEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("View_Wishlist_ByUserId_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        WishlistEntity book = new WishlistEntity();
                        book.WishlistId = (int)reader["WishlistId"];
                        book.BookId = (int)reader["BookId"];
                        book.UserId = (int)reader["UserId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Image = (string)reader["Image"];
                        book.Price = (decimal)reader["Price"];
                        book.OriginalPrice = (decimal)reader["OriginalPrice"];

                        wishlist.Add(book);
                    }
                    return wishlist;
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
