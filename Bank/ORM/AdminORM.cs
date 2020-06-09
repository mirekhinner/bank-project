using Bank.Objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Bank.Types;

namespace Bank.ORM
{
    public partial class UserORM
    {
        private String selectAdminCount = 
            "select count(*) from users where WorkPassword=@WorkPassword and AdminLogin=@AdminLogin";
        
        private String selectByLogin = 
            "select * from users where WorkPassword=@WorkPassword and AdminLogin=@AdminLogin";
        
        private String getAllAdmins = 
            "select * from users where admintype is not null;";
        
        private String insertNewAdmin = 
            "insert into Users values (@GUID,@Name,@Surname,@Address,@Mail,@Phone,@Valid,NULL,NULL,NULL,@Password,NULL,NULL,@Type,@Login)";

        private String insertNewAddress = 
            "insert into Address values (@Id,@Street,@StreetNumber,@City,@PSC,@Country)";
        
        private String getMaxAddressId = 
            "select max(AddressId) from Address";
        
        private String updateAdminPassword = 
            "update Users set workpassword = @WorkPassword where adminlogin = @AdminLogin;";

        public Admin GetAdmin(string Login, string Password)
        {
            int count = 0;

            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectByLogin);
            command.Parameters.AddWithValue("@AdminLogin", Login);
            command.Parameters.AddWithValue("@WorkPassword", Password);

            SqlCommand commandCount = connection.CreateCommand(selectAdminCount);
            commandCount.Parameters.AddWithValue("@AdminLogin", Login);
            commandCount.Parameters.AddWithValue("@WorkPassword", Password);
            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 0:
                    MessageBox.Show("You have entered a wrong credentials (admin)");
                    return null;
                case 1:
                    Admin admin = new Admin();
                    while (reader.Read())
                    {
                        admin.Guid = reader.GetGuid(0);
                        admin.Name = reader.GetString(1);
                        admin.SurName = reader.GetString(2);
                        admin.address = new Address();
                        admin.address.Id = reader.GetInt32(3);
                        admin.address = GetAddress(admin.address.Id);
                        admin.Mail = reader.GetString(4);
                        admin.Phone = reader.GetString(5);
                        admin.Valid = reader.GetBoolean(6);
                        admin.Password = reader.GetString(10);
                        admin.AdminType = (AdminType)reader.GetInt32(13);
                        admin.Login = reader.GetString(14);
                    }
                    connection.CloseConnection();
                    return admin;
                default:
                    MessageBox.Show("Credentials suit more than one customer");
                    return null;
            }
        }

        private List<Admin> GetAdmins()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(getAllAdmins);

            SqlDataReader reader = command.ExecuteReader();

            List<Admin> admins = new List<Admin>();
            while (reader.Read())
            {
                Admin admin = new Admin();
                admin.Guid = reader.GetGuid(0);
                admin.Name = reader.GetString(1);
                admin.SurName = reader.GetString(2);
                admin.address = new Address();
                admin.address.Id = reader.GetInt32(3);
                admin.Valid = reader.GetBoolean(6);
                admin.AdminType = (AdminType)reader.GetInt32(13);
                admins.Add(admin);
            }

            connection.CloseConnection();
            return admins;
        }

        public bool CreateAdmin(Admin admin)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(insertNewAdmin);

            command.Parameters.AddWithValue("@GUID", Guid.NewGuid());
            command.Parameters.AddWithValue("@Name", admin.Name);
            command.Parameters.AddWithValue("@Surname", admin.SurName);
            command.Parameters.AddWithValue("@Address", admin.address.Id);
            command.Parameters.AddWithValue("@Mail", admin.Mail);
            command.Parameters.AddWithValue("@Phone", admin.Phone);
            command.Parameters.AddWithValue("@Valid", 1);
            command.Parameters.AddWithValue("@Password", admin.Password);
            command.Parameters.AddWithValue("@Type", admin.AdminType);
            command.Parameters.AddWithValue("@Login", admin.Login);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public bool CreateAddress(Address address)
        {
            if (address == null)
                return false;

            address.Id = GetMaxAddressId();
            DBConnection connection = new DBConnection();
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(insertNewAddress);
            command.Parameters.AddWithValue("@Id", address.Id);
            command.Parameters.AddWithValue("@Street", address.Street);
            command.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
            command.Parameters.AddWithValue("@City", address.City);
            command.Parameters.AddWithValue("@Country", address.Country);
            command.Parameters.AddWithValue("@PSC", address.PostalCode);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public int GetMaxAddressId()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(getMaxAddressId);
            
            if (command.ExecuteScalar() != null)
            { 
                return (int)command.ExecuteScalar() + 1; 
            }
            else
            { 
                return -1; 
            }
        }
        public bool UpdateAdmin(Admin admin)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(updateAdminPassword);
            command.Parameters.AddWithValue("@WorkPassword", admin.Password);
            command.Parameters.AddWithValue("@AdminLogin", admin.Login);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }
    }
}
