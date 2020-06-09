using Bank.Objects;
using Bank.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace Bank.ORM
{
    public partial class UserORM
    {
        private  String selectCountByCompanyNumber =
            "SELECT count(*) FROM users WHERE  WorkPassword=@WorkPassword and CompanyNumber=@CompanyNumber";

        private  String selectOfficialByLogin =
            "SELECT * from users where WorkPassword=@WorkPassword and CompanyNumber=@CompanyNumber";

        private  String selectOfficials =
            "SELECT * FROM users WHERE CompanyNumber is not null";

        private  String selectAddressById =
            "SELECT * FROM address WHERE AddressId = @AddressId";

        private  String updateOfficial =
            "update users set WorkPassword = @WorkPassword where CompanyNumber = @CompanyNumber";

        private  String deleteOfficial =
            "update users set valid = 0 where CompanyNumber = @CompanyNumber";

        private  String createOfficial =
            "insert into users values (@GUID,@Name,@Surname,@Address,@Mail,@Phone,@Valid,@OfficialType,@CompanyNumber,null,@Password,null,null,null,null)";

        public  Official GetOfficialById(Official official)
        {
            int count = 0;

            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectOfficialByLogin);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);
            command.Parameters.AddWithValue("@WorkPassword", official.Password);

            SqlCommand commandCount = connection.CreateCommand(selectCountByCompanyNumber);
            commandCount.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);
            commandCount.Parameters.AddWithValue("@WorkPassword", official.Password);

            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 0:
                    MessageBox.Show("You have entered a wrong credentials (official)");
                    connection.CloseConnection();

                    return null;
                case 1:
                    while (reader.Read())
                    {
                        official.Guid = reader.GetGuid(0);
                        official.Name = reader.GetString(1);
                        official.SurName = reader.GetString(2);
                        official.address = new Address();
                        official.address.Id = reader.GetInt32(3);
                        official.address = GetAddress(official.address.Id);
                        official.Mail = reader.GetString(4);
                        official.Phone = reader.GetString(5);
                        official.Valid = reader.GetBoolean(6);
                        official.OfficialType = (OfficialType)reader.GetInt32(7);
                        official.CompanyNumber = reader.GetString(8);
                        official.Password = reader.GetString(10);
                    }

                    connection.CloseConnection();
                    return official;
                default:
                    MessageBox.Show("Credentials suit more than one customer");
                    connection.CloseConnection();
                    return null;
            }
        }

        public  Address GetAddress(Int32 addressId)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectAddressById);
            command.Parameters.AddWithValue("@AddressId", addressId);

            Address address = new Address();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                address.Id = reader.GetInt32(0);
                address.Street = reader.GetString(1);
                address.StreetNumber = reader.GetString(2);
                address.City = reader.GetString(3);
                address.PostalCode = reader.GetString(4);
                address.Country = reader.GetString(5);
            }
            return address;
        }

        public  bool UpdateOfficial(Official official)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(updateOfficial);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);
            command.Parameters.AddWithValue("@WorkPassword", official.Password);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        private List<Official> GetAllOfficials()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectOfficials);

            SqlDataReader reader = command.ExecuteReader();
            List<Official> officials = new List<Official>();
            while (reader.Read())
            {
                Official official = new Official();
                official.Guid = reader.GetGuid(0);
                official.Name = reader.GetString(1);
                official.SurName = reader.GetString(2);
                official.address = new Address();
                official.address.Id = reader.GetInt32(3);
                official.Mail = reader.GetString(4);
                official.Phone = reader.GetString(5);
                official.Valid = reader.GetBoolean(6);
                official.OfficialType = (OfficialType)reader.GetInt32(7);
                official.CompanyNumber = reader.GetString(8);
                official.Password = reader.GetString(10);
                officials.Add(official);
            }

            return officials;
        }
        private bool DeleteOfficial(Official official)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(deleteOfficial);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public bool CreateNewOfficial(Official official)
        {
            DBConnection connection = new DBConnection(); 
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(createOfficial);

            command.Parameters.AddWithValue("@GUID", Guid.NewGuid());
            command.Parameters.AddWithValue("@Name", official.Name);
            command.Parameters.AddWithValue("@Surname", official.SurName);
            command.Parameters.AddWithValue("@Address", official.address.Id);
            command.Parameters.AddWithValue("@Mail", official.Mail);
            command.Parameters.AddWithValue("@Phone", official.Phone);
            command.Parameters.AddWithValue("@Valid", 1);
            command.Parameters.AddWithValue("@OfficialType", official.OfficialType);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);
            command.Parameters.AddWithValue("@Password", official.Password);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }
    }
}
