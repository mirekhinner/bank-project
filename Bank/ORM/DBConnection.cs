using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Bank.ORM
{
    class DBConnection
    {
         string connectionString = @"Server=localhost\SQLEXPRESS;Database=Bank;Trusted_Connection=True;";

        private SqlConnection Cnn { get; set; }

        public DBConnection()
        {
            Cnn = new SqlConnection();
        }

        public bool OpenConnection()
        { 
            if (Cnn.State != ConnectionState.Open)
                try
                {
                    Cnn.ConnectionString = connectionString;
                    Cnn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection ! ");
                    return false;
                }
            MessageBox.Show("Connection is already oppened ! ");

            return false;
        }

        public bool CloseConnection()
        {
            Cnn.Close();
            return true;
        }

        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, Cnn);
            return command;
        }
    }
}
