using Bank.Objects;
using Bank.Types;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace Bank.ORM
{
    public partial class UserORM
    {
        private  String selectCustomerCount =
            "select count(*) from users where UserPassword=@UserPassword and SSN=@SSN";

        private  String selectCustomerByLogin =
            "select * from users where UserPassword=@UserPassword and SSN=@SSN";

        private  String insertNewCustomer =
            "insert into Users values (@GUID,@Name,@Surname,@Address,@Mail,@Phone,@Valid,NULL,NULL,@UserPassword,NULL,@SSN,@CustomerType,NULL,NULL)";

        public  Customer GetCustomer(string Login, string Password)
        {
            int count = 0;

            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectCustomerByLogin);
            command.Parameters.AddWithValue("@SSN", Login);
            command.Parameters.AddWithValue("@UserPassword", Password);

            SqlCommand commandCount = connection.CreateCommand(selectCustomerCount);
            commandCount.Parameters.AddWithValue("@SSN", Login);
            commandCount.Parameters.AddWithValue("@UserPassword", Password);
            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 0:
                    MessageBox.Show("You have entered a wrong credentials (customer)");
                    return null;
                case 1:
                    Customer customer = new Customer();
                    while (reader.Read())
                    {
                        customer.Guid = reader.GetGuid(0);
                        customer.Name = reader.GetString(1);
                        customer.SurName = reader.GetString(2);
                        customer.address = new Address();
                        customer.address.Id = reader.GetInt32(3);
                        customer.address = GetAddress(customer.address.Id);
                        customer.Mail = reader.GetString(4);
                        customer.Phone = reader.GetString(5);
                        customer.Valid = reader.GetBoolean(6);
                        customer.Password = reader.GetString(9);
                        customer.SSN = reader.GetString(11);
                        customer.CustomerType = (CustomerType)reader.GetInt32(12);

                    }
                    connection.CloseConnection();
                    return customer;
                default:
                    MessageBox.Show("Credentials suit more than one customer");
                    return null;
            }
        }

        public bool CreateNewCustomer(Customer customer)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(insertNewCustomer);

            command.Parameters.AddWithValue("@GUID", Guid.NewGuid());
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Surname", customer.SurName);
            command.Parameters.AddWithValue("@Address", customer.address.Id);
            command.Parameters.AddWithValue("@Mail", customer.Mail);
            command.Parameters.AddWithValue("@Phone", customer.Phone);
            command.Parameters.AddWithValue("@Valid", 1);
            command.Parameters.AddWithValue("@UserPassword", customer.Password);
            command.Parameters.AddWithValue("@SSN", customer.SSN);
            command.Parameters.AddWithValue("@CustomerType", customer.CustomerType);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }
    }
}
