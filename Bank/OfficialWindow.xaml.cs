using Bank.Logger;
using Bank.Objects;
using Bank.ORM;
using Bank.Types;
using Bank.Validation;
using System;
using System.Collections.Generic;
using System.Windows;


namespace Bank
{
    /// <summary>
    /// Interaction logic for OfficialWindow.xaml
    /// </summary>
    public partial class OfficialWindow : Window
    {
        public Official Official { get; set; }
        public UserORM UserORM { get; set; }
        public Validator Validator { get;set; }
        public Customer Customer { get; set; }

        public OfficialWindow(Official official)
        {            
            InitializeComponent();
            Official = official;
            UserORM = new UserORM();
            Validator = new Validator();

            Log logger = new Log();
            logger.Info("Official window opened. Official: " + official.CompanyNumber);
        }

        private void updatePassOffical_Click(object sender, RoutedEventArgs e)
        {
            Official.Password = changeOfPassBox.Text;

            if (UserORM.UpdateOfficial(Official))
            {
                MessageBox.Show("Password changed successfully");
                changeOfPassBox.Text = String.Empty;
            }
            else
            {
                MessageBox.Show("Password change failed");
                changeOfPassBox.Text = String.Empty;
            }
        }

        private void createOfficial_Click(object sender, RoutedEventArgs e)
        {
            Official official = new Official();

            official.address = new Address();

            if (UserORM.GetMaxAddressId() != -1)
            {
                official.address.Id = UserORM.GetMaxAddressId();
            }
            else
            {
                MessageBox.Show("Address ID is not correct.");
                return;
            }

            official.address.Street = streetBox.Text;
            official.address.StreetNumber = streetNoBox.Text;
            official.address.City = cityBox.Text;
            official.address.Country = countryBox.Text;
            official.address.PostalCode = postalBox.Text;

            official.Name = nameOfBox.Text;
            official.SurName = surnameOfBox.Text;
            official.Mail = mailOfBox.Text;
            official.Phone = phoneOfBox.Text;
            official.CompanyNumber = loginOfBox.Text;
            official.Password = passwordOfBox.Text;

            if (typeComboOfBox.SelectedIndex == -1)
            { 
                MessageBox.Show("Select the type"); 
                return; 
            }

            official.OfficialType = (OfficialType)typeComboOfBox.SelectedItem;

            if (!Validator.validateOfficial(official)) 
                return;

            if (UserORM.CreateAddress(official.address))
            {
                if (UserORM.CreateNewOfficial(official))
                {
                    MessageBox.Show("Official " + official.CompanyNumber + " was successfully created");
                }
                else
                { 
                    MessageBox.Show("Official creation failed");
                }
            }
            else
            { 
                MessageBox.Show("Address creation failed");
            }
        }

        private void createCustomer_Click (object sender, RoutedEventArgs e)
        {
            Customer.address = new Address();

            if (UserORM.GetMaxAddressId() != -1)
            {
                Customer.address.Id = UserORM.GetMaxAddressId();
            }
            else
            {
                MessageBox.Show("Address ID is not correct.");
                return;
            }

            Customer.address.Street = streetBox.Text;
            Customer.address.StreetNumber = streetNoBox.Text;
            Customer.address.City = cityBox.Text;
            Customer.address.Country = countryBox.Text;
            Customer.address.PostalCode = postalBox.Text;

            Customer.Name = nameCusBox.Text;
            Customer.SurName = surnameCusBox.Text;
            Customer.Mail = mailCusBox.Text;
            Customer.Phone = phoneCusBox.Text;
            Customer.SSN = loginCusBox.Text;
            Customer.Password = passwordCusBox.Text;
            
            if (typeComboCusBox.SelectedIndex == -1)
            { 
                MessageBox.Show("Select the type"); 
                return; 
            }

            Customer.CustomerType = (CustomerType)typeComboCusBox.SelectedItem;

            if (!Validator.validateCustomer(Customer)) return;

            if (UserORM.CreateAddress(Customer.address))
            {
                if (UserORM.CreateNewCustomer(Customer))
                {
                    MessageBox.Show("Customer " + Customer.SSN + " was successfully created");

                    //empty the boxes
                    streetBox.Text = "";
                    streetNoBox.Text = "";
                    cityBox.Text = "";
                    countryBox.Text = "";
                    postalBox.Text = "";
                    nameCusBox.Text = "";
                    surnameCusBox.Text = "";
                    mailCusBox.Text = "";
                    phoneCusBox.Text = "";
                    loginCusBox.Text = "";
                    passwordCusBox.Text = "";
                    typeComboCusBox.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Customer creation failed");
                }
            }
            else
            {
                MessageBox.Show("Address creation failed");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }

        private void itemCusAccInfo_Click(object sender, RoutedEventArgs e)
        {
            TransactionWindow transactionWindow = new TransactionWindow(Official);
            transactionWindow.Show();
        }

        private void itemOfAccPass_Click(object sender, RoutedEventArgs e)
        {
            //change password - visible
            changeOfPassLabel.Visibility = Visibility.Visible;
            changeOfPassLabel2.Visibility = Visibility.Visible;
            changeOfPassBox.Visibility = Visibility.Visible;
            updatePassOffical.Visibility = Visibility.Visible;

            // create official - invisible
            newOfLabel.Visibility = Visibility.Hidden;
            newOfLabel1.Visibility = Visibility.Hidden;
            newOfLabel2.Visibility = Visibility.Hidden;
            newOfLabel3.Visibility = Visibility.Hidden;
            newOfLabel4.Visibility = Visibility.Hidden;
            newOfLabel5.Visibility = Visibility.Hidden;
            newOfLabel6.Visibility = Visibility.Hidden;
            newOfLabel7.Visibility = Visibility.Hidden;
            nameOfBox.Visibility = Visibility.Hidden;
            surnameOfBox.Visibility = Visibility.Hidden;
            addressOfBox.Visibility = Visibility.Hidden;
            mailOfBox.Visibility = Visibility.Hidden;
            phoneOfBox.Visibility = Visibility.Hidden;
            loginOfBox.Visibility = Visibility.Hidden;
            passwordOfBox.Visibility = Visibility.Hidden;
            typeComboOfBox.Visibility = Visibility.Hidden;
            newOfLabel8.Visibility = Visibility.Hidden;
            createOfficial.Visibility = Visibility.Hidden;
            newAddLabel1.Visibility = Visibility.Hidden;
            newAddLabel2.Visibility = Visibility.Hidden;
            newAddLabel3.Visibility = Visibility.Hidden;
            newAddLabel4.Visibility = Visibility.Hidden;
            newAddLabel5.Visibility = Visibility.Hidden;
            streetBox.Visibility = Visibility.Hidden;
            streetNoBox.Visibility = Visibility.Hidden;
            cityBox.Visibility = Visibility.Hidden;
            postalBox.Visibility = Visibility.Hidden;
            countryBox.Visibility = Visibility.Hidden;

            //create customer - invisible
            newCusLabel.Visibility = Visibility.Hidden;
            newCusLabel1.Visibility = Visibility.Hidden;
            newCusLabel2.Visibility = Visibility.Hidden;
            newCusLabel3.Visibility = Visibility.Hidden;
            newCusLabel4.Visibility = Visibility.Hidden;
            newCusLabel5.Visibility = Visibility.Hidden;
            newCusLabel6.Visibility = Visibility.Hidden;
            newCusLabel7.Visibility = Visibility.Hidden;
            nameCusBox.Visibility = Visibility.Hidden;
            surnameCusBox.Visibility = Visibility.Hidden;
            addressCusBox.Visibility = Visibility.Hidden;
            mailCusBox.Visibility = Visibility.Hidden;
            phoneCusBox.Visibility = Visibility.Hidden;
            loginCusBox.Visibility = Visibility.Hidden;
            passwordCusBox.Visibility = Visibility.Hidden;
            typeComboCusBox.Visibility = Visibility.Hidden;
            newCusLabel8.Visibility = Visibility.Hidden;
            createCustomer.Visibility = Visibility.Hidden;


        }

        private void itemOfAccNew_Click(object sender, RoutedEventArgs e)
        {
            //change password - invisible
            changeOfPassLabel.Visibility = Visibility.Hidden;
            changeOfPassLabel2.Visibility = Visibility.Hidden;
            changeOfPassBox.Visibility = Visibility.Hidden;
            updatePassOffical.Visibility = Visibility.Hidden;

            // create official - invisible
            newOfLabel.Visibility = Visibility.Visible;
            newOfLabel1.Visibility = Visibility.Visible;
            newOfLabel2.Visibility = Visibility.Visible;
            newOfLabel3.Visibility = Visibility.Visible;
            newOfLabel4.Visibility = Visibility.Visible;
            newOfLabel5.Visibility = Visibility.Visible;
            newOfLabel6.Visibility = Visibility.Visible;
            newOfLabel7.Visibility = Visibility.Visible;
            nameOfBox.Visibility = Visibility.Visible;
            surnameOfBox.Visibility = Visibility.Visible;
            addressOfBox.Visibility = Visibility.Visible;
            mailOfBox.Visibility = Visibility.Visible;
            phoneOfBox.Visibility = Visibility.Visible;
            loginOfBox.Visibility = Visibility.Visible;
            passwordOfBox.Visibility = Visibility.Visible;
            typeComboOfBox.Visibility = Visibility.Visible;
            newOfLabel8.Visibility = Visibility.Visible;
            createOfficial.Visibility = Visibility.Visible;
            newAddLabel1.Visibility = Visibility.Visible;
            newAddLabel2.Visibility = Visibility.Visible;
            newAddLabel3.Visibility = Visibility.Visible;
            newAddLabel4.Visibility = Visibility.Visible;
            newAddLabel5.Visibility = Visibility.Visible;
            streetBox.Visibility = Visibility.Visible;
            streetNoBox.Visibility = Visibility.Visible;
            cityBox.Visibility = Visibility.Visible;
            postalBox.Visibility = Visibility.Visible;
            countryBox.Visibility = Visibility.Visible;

            //create customer - invisible
            newCusLabel.Visibility = Visibility.Hidden;
            newCusLabel1.Visibility = Visibility.Hidden;
            newCusLabel2.Visibility = Visibility.Hidden;
            newCusLabel3.Visibility = Visibility.Hidden;
            newCusLabel4.Visibility = Visibility.Hidden;
            newCusLabel5.Visibility = Visibility.Hidden;
            newCusLabel6.Visibility = Visibility.Hidden;
            newCusLabel7.Visibility = Visibility.Hidden;
            nameCusBox.Visibility = Visibility.Hidden;
            surnameCusBox.Visibility = Visibility.Hidden;
            addressCusBox.Visibility = Visibility.Hidden;
            mailCusBox.Visibility = Visibility.Hidden;
            phoneCusBox.Visibility = Visibility.Hidden;
            loginCusBox.Visibility = Visibility.Hidden;
            passwordCusBox.Visibility = Visibility.Hidden;
            typeComboCusBox.Visibility = Visibility.Hidden;
            newCusLabel8.Visibility = Visibility.Hidden;
            createCustomer.Visibility = Visibility.Hidden;

            //empty the boxes
            streetBox.Text = "";
            streetNoBox.Text = "";
            cityBox.Text = "";
            countryBox.Text = "";
            postalBox.Text = "";
            nameCusBox.Text = "";
            surnameCusBox.Text = "";
            mailCusBox.Text = "";
            phoneCusBox.Text = "";
            loginCusBox.Text = "";
            passwordCusBox.Text = "";
            typeComboCusBox.SelectedIndex = -1;

        }

        private void itemCusAccNew_Click(object sender, RoutedEventArgs e)
        {
            //change password - invisible
            changeOfPassLabel.Visibility = Visibility.Hidden;
            changeOfPassLabel2.Visibility = Visibility.Hidden;
            changeOfPassBox.Visibility = Visibility.Hidden;
            updatePassOffical.Visibility = Visibility.Hidden;

            // create official - invisible
            newOfLabel.Visibility = Visibility.Hidden;
            newOfLabel1.Visibility = Visibility.Hidden;
            newOfLabel2.Visibility = Visibility.Hidden;
            newOfLabel3.Visibility = Visibility.Hidden;
            newOfLabel4.Visibility = Visibility.Hidden;
            newOfLabel5.Visibility = Visibility.Hidden;
            newOfLabel6.Visibility = Visibility.Hidden;
            newOfLabel7.Visibility = Visibility.Hidden;
            nameOfBox.Visibility = Visibility.Hidden;
            surnameOfBox.Visibility = Visibility.Hidden;
            addressOfBox.Visibility = Visibility.Hidden;
            mailOfBox.Visibility = Visibility.Hidden;
            phoneOfBox.Visibility = Visibility.Hidden;
            loginOfBox.Visibility = Visibility.Hidden;
            passwordOfBox.Visibility = Visibility.Hidden;
            typeComboOfBox.Visibility = Visibility.Hidden;
            newOfLabel8.Visibility = Visibility.Hidden;
            createOfficial.Visibility = Visibility.Hidden;

            //create customer - visible
            newCusLabel.Visibility = Visibility.Visible;
            newCusLabel1.Visibility = Visibility.Visible;
            newCusLabel2.Visibility = Visibility.Visible;
            newCusLabel3.Visibility = Visibility.Visible;
            newCusLabel4.Visibility = Visibility.Visible;
            newCusLabel5.Visibility = Visibility.Visible;
            newCusLabel6.Visibility = Visibility.Visible;
            newCusLabel7.Visibility = Visibility.Visible;
            nameCusBox.Visibility = Visibility.Visible;
            surnameCusBox.Visibility = Visibility.Visible;
            addressCusBox.Visibility = Visibility.Visible;
            mailCusBox.Visibility = Visibility.Visible;
            phoneCusBox.Visibility = Visibility.Visible;
            loginCusBox.Visibility = Visibility.Visible;
            passwordCusBox.Visibility = Visibility.Visible;
            typeComboCusBox.Visibility = Visibility.Visible;
            newCusLabel8.Visibility = Visibility.Visible;
            createCustomer.Visibility = Visibility.Visible;
            newAddLabel1.Visibility = Visibility.Visible;
            newAddLabel2.Visibility = Visibility.Visible;
            newAddLabel3.Visibility = Visibility.Visible;
            newAddLabel4.Visibility = Visibility.Visible;
            newAddLabel5.Visibility = Visibility.Visible;
            streetBox.Visibility = Visibility.Visible;
            streetNoBox.Visibility = Visibility.Visible;
            cityBox.Visibility = Visibility.Visible;
            postalBox.Visibility = Visibility.Visible;
            countryBox.Visibility = Visibility.Visible;

            //empty the boxes
            streetBox.Text = "";
            streetNoBox.Text = "";
            cityBox.Text = "";
            countryBox.Text = "";
            postalBox.Text = "";
            nameOfBox.Text = "";
            surnameOfBox.Text = "";
            mailOfBox.Text = "";
            phoneOfBox.Text = "";
            loginOfBox.Text = "";
            passwordOfBox.Text = "";
            typeComboOfBox.SelectedIndex = -1;
        }
    }
}
