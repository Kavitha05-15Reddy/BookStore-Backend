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
    public class FeedbackRL : IFeedbackRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public FeedbackRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //AddFeedback
        public object AddFeedback(FeedbackModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Add_Feedback_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@Rating", model.Rating);
                    cmd.Parameters.AddWithValue("@Review", model.Review);
                    
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

        //GetAllFeedbacks
        public object GetAllFeedbacks()
        {
            List<FeedbackEntity> feedbacks = new List<FeedbackEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAll_Feedbacks_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FeedbackEntity feedback = new FeedbackEntity();
                        feedback.FeedbackId = (int)reader["FeedbackId"];
                        feedback.BookId = (int)reader["BookId"];
                        feedback.UserName = (string)reader["UserName"];
                        feedback.Rating = (float)(double)reader["Rating"];
                        feedback.Review = (string)reader["Review"];

                        feedbacks.Add(feedback);
                    }
                    return feedbacks;
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

        //UpdateFeedback
        public object UpdateFeedback(UpdateFeebackModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Update_Feedback_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeedbackId", model.FeedbackId);
                    cmd.Parameters.AddWithValue("@Rating", model.Rating);
                    cmd.Parameters.AddWithValue("@Review", model.Review);

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

        //DeleteFeedback
        public object DeleteFeedback(int feedbackId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete_Feedback_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeedbackId", feedbackId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return feedbackId;
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

        //GetAllFeedbacks_ByBookId
        public object GetAll_Feedbacks_ByBookId(int bookId)
        {
            List<FeedbackEntity> feedbacks = new List<FeedbackEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAll_Feedbacks_ByBookId_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FeedbackEntity feedback = new FeedbackEntity();
                        feedback.FeedbackId = (int)reader["FeedbackId"];
                        feedback.BookId = (int)reader["BookId"];
                        feedback.UserName = (string)reader["UserName"];
                        feedback.Rating = (float)(double)reader["Rating"];
                        feedback.Review = (string)reader["Review"];

                        feedbacks.Add(feedback);
                    }
                    return feedbacks;
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
