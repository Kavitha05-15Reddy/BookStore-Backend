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
using System.Net;

namespace RepositoryLayer.Service
{
    public class AddressRL : IAddressRL
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;
        public AddressRL(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //AddAddress
        public object AddAddress(AddressModel model)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Add_Address_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                    cmd.Parameters.AddWithValue("@Address", model.Address);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);

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

        //GetAllAddresses
        public object GetAllAddresses()
        {
            List<AddressModel> addresses = new List<AddressModel>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAll_Addresses_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AddressModel address = new AddressModel();
                        address.AddressId = (int)reader["AddressId"];
                        address.UserId = (int)reader["UserId"];
                        address.FullName = (string)reader["FullName"];
                        address.MobileNo = (string)reader["MobileNo"];
                        address.Address = (string)reader["Address"];
                        address.City = (string)reader["City"];
                        address.State = (string)reader["State"];
                        address.Type = (string)reader["Type"];

                        addresses.Add(address);
                    }
                    return addresses;
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

        //UpdateAddress
        public object UpdateAddress(AddressEntity address)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Update_Address_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", address.AddressId);
                    cmd.Parameters.AddWithValue("@FullName", address.FullName);
                    cmd.Parameters.AddWithValue("@MobileNo", address.MobileNo);
                    cmd.Parameters.AddWithValue("@Address", address.Address);
                    cmd.Parameters.AddWithValue("@City", address.City);
                    cmd.Parameters.AddWithValue("@State", address.State);
                    cmd.Parameters.AddWithValue("@Type", address.Type);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return address;
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

        //DeleteAddress
        public object DeleteAddress(int addressId)
        {
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete_Address_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", addressId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return addressId;
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

        //GetAddressByUserId
        public object GetAddressByUserId(int userId)
        {
            List<AddressEntity> addresses = new List<AddressEntity>();
            using (SqlConnection conn = (SqlConnection)context.CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAddress_ByUserId_SP", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AddressEntity address = new AddressEntity();
                        address.AddressId = (int)reader["AddressId"];
                        address.FullName = (string)reader["FullName"];
                        address.MobileNo = (string)reader["MobileNo"];
                        address.Address = (string)reader["Address"];
                        address.City = (string)reader["City"];
                        address.State = (string)reader["State"];
                        address.Type = (string)reader["Type"];

                        addresses.Add(address);
                    }
                    return addresses;
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
