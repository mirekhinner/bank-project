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
        private String selectCustomerInfoCount =
            "select count(*) from users where SSN=@SSN and Valid = 1";

        private String selectCustomerInfoByLogin =
            "select * from users where SSN=@SSN and Valid = 1";

        private String selectTransactionByNumber =
            "select * from Transactions where Payer = @BillNumber or Recipient = @BillNumber and Valid = 1;";

        private String selectBillById =
            "select * from Bill where id = @BillId and Valid = 1;";

        private String selectBillByIdCount =
            "select count(*) from Bill where id = @BillId and Valid = 1;";

        public Customer findCustomerInfo(string Login)
        {
            int count = 0;

            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectCustomerInfoByLogin);
            command.Parameters.AddWithValue("@SSN", Login);

            SqlCommand commandCount = connection.CreateCommand(selectCustomerInfoCount);
            commandCount.Parameters.AddWithValue("@SSN", Login);
            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 0:
                    MessageBox.Show("There is no customer with such SSN");
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
                    MessageBox.Show("There is more than once customer with such SSN");
                    return null;
            }
        }
        public Bill GetBill(Customer customer)
        {
            int count = 0;
            Bill bill = new Bill();

            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectBillById);
            command.Parameters.AddWithValue("@BillId", customer.Guid);

            SqlCommand commandCount = connection.CreateCommand(selectBillByIdCount);
            commandCount.Parameters.AddWithValue("@BillId", customer.Guid);

            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 1:
                    while (reader.Read())
                    {
                        bill.BillNumber = reader.GetInt32(0);
                        bill.Guid = reader.GetGuid(1);
                        bill.Balance = reader.GetInt32(2);
                        bill.Valid = reader.GetBoolean(3);
                    }

                    connection.CloseConnection();
                    return bill;
                default:
                    MessageBox.Show("More than one account for the customer!");
                    connection.CloseConnection();
                    return null;
            }
        }

        public List<Transaction> GetMyTransactions(Bill bill)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand command = connection.CreateCommand(selectTransactionByNumber);
            command.Parameters.AddWithValue("@BillNumber", bill.BillNumber);

            SqlDataReader reader = command.ExecuteReader();

            List<Transaction> transactions = new List<Transaction>();
            while (reader.Read())
            {
                Transaction transaction = new Transaction();
                transaction.Id = reader.GetInt32(0);
                transaction.VariableSymbol = reader.GetInt32(1);
                transaction.DateTransaction = reader.GetDateTime(2);
                transaction.Payer = reader.GetInt32(3);
                transaction.Recipient = reader.GetInt32(4);
                transaction.Amount = reader.GetInt32(5);
                transaction.Valid = reader.GetBoolean(6);

                transactions.Add(transaction);
            }

            connection.CloseConnection();
            return transactions;
        }
    }
}
