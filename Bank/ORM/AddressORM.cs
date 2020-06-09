using System;
using Bank.Objects;
using System.Data.SqlClient;


namespace Bank.ORM
{
    public class AddressORM
    {
        private String createAddress = "insert into address values (@ID,@Street,@StreetNumber,@City,@PostalCode,@Country)";

        public bool CreateNewAddress(Address address)
        {

            DBConnection connection = new DBConnection();
            connection.OpenConnection();
            SqlCommand command = connection.CreateCommand(createAddress);

            command.Parameters.AddWithValue("@ID", address.Id);
            command.Parameters.AddWithValue("@Street", address.Street);
            command.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
            command.Parameters.AddWithValue("@City", address.City);
            command.Parameters.AddWithValue("@PostalCode", address.Country);
            command.Parameters.AddWithValue("@Country", address.PostalCode);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }
    }
}
