using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace RepositoryLayer.Service
{
    public class BookRL : IBookRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public BookRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //AddBook
        public object AddBook(BookModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Add_Book_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Author", model.Author);
                    cmd.Parameters.AddWithValue("@OriginalPrice", model.OriginalPrice);
                    cmd.Parameters.AddWithValue("@DiscountPercentage", model.DiscountPercentage);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Image", model.Image);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);

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

        //GetAllBooks
        public object GetAllBooks()
        {
            List<BookEntity> books = new List<BookEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Get_AllBooks_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BookEntity book = new BookEntity();
                        book.BookId = (int)reader["BookId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0.0f : (float)(double)reader["Rating"];
                        book.RatingCount = reader.IsDBNull(reader.GetOrdinal("RatingCount")) ? 0 : (int)reader["RatingCount"];
                        book.Price = (decimal)reader["Price"];
                        book.OriginalPrice = (decimal)reader["OriginalPrice"];
                        book.DiscountPercentage = (float)(double)reader["DiscountPercentage"];
                        book.Description = (string)reader["Description"];
                        book.Image = (string)reader["Image"];
                        book.Quantity = (int)reader["Quantity"];
                        books.Add(book);
                    }
                    return books;
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

        //UpdateBook
        public object UpdateBook(int bookId, BookModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Update_Book_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Author", model.Author);
                    cmd.Parameters.AddWithValue("@OriginalPrice", model.OriginalPrice);
                    cmd.Parameters.AddWithValue("@DiscountPercentage", model.DiscountPercentage);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Image", model.Image);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);

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

        //DeleteBook
        public object DeleteBook(int bookId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete_Book_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return bookId;
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

        //GetBookByBookId
        public object GetBookByBookId(int bookId)
        {
            BookEntity book = new BookEntity();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetBook_ByBookId_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        book.BookId = (int)reader["BookId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Rating = (float)(double)reader["Rating"];
                        book.RatingCount = (int)reader["RatingCount"];
                        book.Price = (decimal)reader["Price"];
                        book.OriginalPrice = (decimal)reader["OriginalPrice"];
                        book.DiscountPercentage = (float)(double)reader["DiscountPercentage"];
                        book.Description = (string)reader["Description"];
                        book.Image = (string)reader["Image"];
                        book.Quantity = (int)reader["Quantity"];
                    }
                    return book;
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
        //GetBookByName
        public object GetBookByName(string bookName, string authorName)
        {
            BookEntity book = new BookEntity();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Get_Book_Byname_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookName", bookName);
                    cmd.Parameters.AddWithValue("@AuthorName", authorName);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        book.BookId = (int)reader["BookId"];
                        book.Title = (string)reader["Title"];
                        book.Author = (string)reader["Author"];
                        book.Rating = (float)(double)reader["Rating"];
                        book.RatingCount = (int)reader["RatingCount"];
                        book.Price = (decimal)reader["Price"];
                        book.OriginalPrice = (decimal)reader["OriginalPrice"];
                        book.DiscountPercentage = (float)(double)reader["DiscountPercentage"];
                        book.Description = (string)reader["Description"];
                        book.Image = (string)reader["Image"];
                        book.Quantity = (int)reader["Quantity"];
                    }
                    return book;
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
