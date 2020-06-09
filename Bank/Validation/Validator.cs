using Bank.Objects;
using Bank.ORM;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Bank.Validation
{
    public class Validator
    {
        private String selectUserCount =
            "select count(*) from users where SSN=@input or CompanyNumber=@input or AdminLogin=@input";
        public bool isNumeric(string number)
        {
            bool validation = int.TryParse(number, out int numLogin);

            return (validation && numLogin > 0);

        }

        public bool isSSN(string SSN)
        {
            return (SSN.Length == 10);
        }

        public bool isChar(string text) 
        {
            return text.All(Char.IsLetter);
        }

        public bool isEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool isPhone(string telNo)
        {
            return Regex.Match(telNo, @"^\+\d{6,12}$").Success;
        }

        public bool isEmpty(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public bool isExisting(string input)
        {
            int count = 0;

            DBConnection connection = new DBConnection();
            connection.OpenConnection();

            SqlCommand commandCount = connection.CreateCommand(selectUserCount);
            commandCount.Parameters.AddWithValue("@input", input);
            count = (int)commandCount.ExecuteScalar();

            if (count == 0) return false;
            return true;
        }

        public bool validateCustomer(Customer customer)
        {
            if (!isChar(customer.Name) || isEmpty(customer.Name))
            { MessageBox.Show("Invalid name"); return false; }

            if (!(isChar(customer.SurName)) || isEmpty(customer.SurName))
            { MessageBox.Show("Invalid surname"); return false; }

            if (!(isEmail(customer.Mail)) || isEmpty(customer.Mail))
            { MessageBox.Show("Invalid mail format"); return false; }

            if (!(isPhone(customer.Phone)) || isEmpty(customer.Phone))
            { MessageBox.Show("Invalid phone format"); return false; }

            if (!(isSSN(customer.SSN)) || isEmpty(customer.SSN))
            { MessageBox.Show("Invalid SSN number"); return false; }
            else if (isExisting(customer.SSN))
            { MessageBox.Show("SSN already exists"); return false; }

            if (isEmpty(customer.Password))
            { MessageBox.Show("Invalid password"); return false; }

            if (!(isChar(customer.address.Street)) || isEmpty(customer.address.Street))
            { MessageBox.Show("Invalid street"); return false; }

            if (!(isNumeric(customer.address.StreetNumber)) || isEmpty(customer.address.StreetNumber))
            { MessageBox.Show("Invalid street no"); return false; }

            if (!(isChar(customer.address.City)) || isEmpty(customer.address.City))
            { MessageBox.Show("Invalid city"); return false; }

            if (!(isChar(customer.address.Country)) || isEmpty(customer.address.Country))
            { MessageBox.Show("Invalid country"); return false; }

            if (!(isNumeric(customer.address.PostalCode)) || isEmpty(customer.address.PostalCode))
            { MessageBox.Show("Invalid post code"); return false; }
            return true;
        }

        public bool validateOfficial(Official official)
        {
            if (!isChar(official.Name) || isEmpty(official.Name))
            { MessageBox.Show("Invalid name"); return false; }

            if (!(isChar(official.SurName)) || isEmpty(official.SurName))
            { MessageBox.Show("Invalid surname"); return false; }

            if (!(isEmail(official.Mail)) || isEmpty(official.Mail))
            { MessageBox.Show("Invalid mail format"); return false; }

            if (!(isPhone(official.Phone)) || isEmpty(official.Phone))
            { MessageBox.Show("Invalid phone format"); return false; }

            if (!(isNumeric(official.CompanyNumber)) || isEmpty(official.CompanyNumber))
            { MessageBox.Show("Invalid company number"); return false; }
            else if (isExisting(official.CompanyNumber))
            { MessageBox.Show("Company number already exists"); return false; }

            if (isEmpty(official.Password))
            { MessageBox.Show("Invalid password"); return false; }

            if (!(isChar(official.address.Street)) || isEmpty(official.address.Street))
            { MessageBox.Show("Invalid street"); return false; }

            if (!(isNumeric(official.address.StreetNumber)) || isEmpty(official.address.StreetNumber))
            { MessageBox.Show("Invalid street no"); return false; }

            if (!(isChar(official.address.City)) || isEmpty(official.address.City))
            { MessageBox.Show("Invalid city"); return false; }

            if (!(isChar(official.address.Country)) || isEmpty(official.address.Country))
            { MessageBox.Show("Invalid country"); return false; }

            if (!(isNumeric(official.address.PostalCode)) || isEmpty(official.address.PostalCode))
            { MessageBox.Show("Invalid post code"); return false; }

            return true;
        }

        public bool validateAdmin(Admin admin)
        {
            if (!isChar(admin.Name) || isEmpty(admin.Name))
            { MessageBox.Show("Invalid name"); return false; }

            if (!(isChar(admin.SurName)) || isEmpty(admin.SurName))
            { MessageBox.Show("Invalid surname"); return false; }

            if (!(isEmail(admin.Mail)) || isEmpty(admin.Mail))
            { MessageBox.Show("Invalid mail format"); return false; }

            if (!(isPhone(admin.Phone)) || isEmpty(admin.Phone))
            { MessageBox.Show("Invalid phone format"); return false; }

            if (isEmpty(admin.Login))
            { MessageBox.Show("Invalid login"); return false; }
            else if (isExisting(admin.Login))
            { MessageBox.Show("Login already exists"); return false; }

            if (isEmpty(admin.Password))
            { MessageBox.Show("Invalid password"); return false; }

            if (!(isChar(admin.address.Street)) || isEmpty(admin.address.Street))
            { MessageBox.Show("Invalid street"); return false; }

            if (!(isNumeric(admin.address.StreetNumber)) || isEmpty(admin.address.StreetNumber))
            { MessageBox.Show("Invalid street no"); return false; }

            if (!(isChar(admin.address.City)) || isEmpty(admin.address.City))
            { MessageBox.Show("Invalid city"); return false; }

            if (!(isChar(admin.address.Country)) || isEmpty(admin.address.Country))
            { MessageBox.Show("Invalid country"); return false; }

            if (!(isNumeric(admin.address.PostalCode)) || isEmpty(admin.address.PostalCode))
            { MessageBox.Show("Invalid post code"); return false; }

            return true;
        }
    }
}
