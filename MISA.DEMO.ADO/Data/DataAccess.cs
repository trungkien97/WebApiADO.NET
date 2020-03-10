using MISA.DEMO.ADO.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MISA.DEMO.ADO.Data
{
    public class DataAccess
    {
        private readonly string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=MISAWebAPI;Integrated Security=True";
        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;

        public DataAccess()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlCommand = _sqlConnection.CreateCommand();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlConnection.Open();
        }

        private IEnumerable<Customer> ExecuteDataReader(string storeName)
        {
            _sqlCommand.CommandText = storeName;
            SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                var customer = new Customer();
                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    var columnName = sqlDataReader.GetName(i);
                    var cellValue = sqlDataReader.GetValue(i);
                    var propertyInfo = customer.GetType().GetProperty(columnName);
                    if (propertyInfo != null && cellValue != DBNull.Value)
                    {
                        propertyInfo.SetValue(customer, cellValue);
                    }
                }
                yield return customer;
            }
            _sqlConnection.Close();
            //return customers;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            //List<Customer> customers = new List<Customer>();
            //sqlCommand.CommandText = "SELECT * FROM Customer";
            return ExecuteDataReader("[dbo].[Proc_GetCustomers]");
        }

        public IEnumerable<Customer> GetCustomerById(Guid customerID)
        {
            //Customer customer = new Customer();
            _sqlCommand.Parameters.AddWithValue("@customerID", customerID);
            return ExecuteDataReader("[dbo].[Proc_GetCustomerById]");
        }

        public int InsertCustomer(Customer customer)
        {
            _sqlCommand.CommandText = "[dbo].[Proc_InsertCustomer]";
            _sqlCommand.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
            _sqlCommand.Parameters.AddWithValue("@FullName", customer.FullName != null ? customer.FullName : string.Empty);
            _sqlCommand.Parameters.AddWithValue("@Mobile", customer.Mobile);
            _sqlCommand.Parameters.AddWithValue("@Sex", customer.Sex);
            _sqlCommand.Parameters.AddWithValue("@Birthday", customer.Birthday);
            _sqlCommand.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
            _sqlCommand.Parameters.AddWithValue("@CompanyTax", customer.CompanyTax);
            _sqlCommand.Parameters.AddWithValue("@Email", customer.Email);
            _sqlCommand.Parameters.AddWithValue("@Address", customer.Address);
            _sqlCommand.Parameters.AddWithValue("@Description", customer.Description);
            _sqlCommand.Parameters.AddWithValue("@CreatedBy", customer.CreatedBy);
            _sqlCommand.Parameters.AddWithValue("@GroupCustomerID", customer.GroupCustomerID);
            int result = _sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            return result;
        }
        public int DeleteCustomer(Guid customerID)
        {
            _sqlCommand.Parameters.AddWithValue("@customerID", customerID);
            _sqlCommand.CommandText = "[dbo].[Proc_DeleteCustomer]";
            int result = _sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            return result;
        }
    }
}