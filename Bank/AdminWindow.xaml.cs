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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Admin Admin { get; set; }
        public UserORM UserORM { get; set; }
        private Validator Validator { get; set; }
        private Official Official { get; set; }
        public AdminWindow(Admin admin)
        {
            InitializeComponent();

            Log logger = new Log();
            UserORM = new UserORM();
            Admin = admin;
            Official = new Official();
            Validator = new Validator();

            logger.Info("Admin window opened. Admin: " + Admin.Login);
        }

        private void createAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin.address = new Address();

            // komentar abychom zjistili, jestli funguje na gitu

            if (UserORM.GetMaxAddressId() != -1)
            {
                Admin.address.Id = UserORM.GetMaxAddressId();
            }
            else
            {
                MessageBox.Show("Address ID is not correct.");
                return;
            }

            Admin.address.Street = streetBox.Text;
            Admin.address.StreetNumber = streetNoBox.Text;
            Admin.address.City = cityBox.Text;
            Admin.address.Country = countryBox.Text;
            Admin.address.PostalCode = postalBox.Text;

            Admin.Name = nameBox.Text;
            Admin.SurName = surnameBox.Text;
            Admin.Mail = mailBox.Text;
            Admin.Phone = phoneBox.Text;
            Admin.Login = loginBox.Text;
            Admin.Password = passwordBox.Text;

            if (typeComboBox.SelectedIndex == -1)
            { 
                MessageBox.Show("Select the type"); 
                return; 
            }

            Admin.AdminType = (AdminType)typeComboBox.SelectedItem;

            if (!Validator.validateAdmin(Admin)) return;

            if (UserORM.CreateAddress(Admin.address))
            {
                if (UserORM.CreateAdmin(Admin))
                {
                    MessageBox.Show("Admin " + Admin.Login + " was successfully created");

                    streetBox.Text = "";
                    streetNoBox.Text = "";
                    cityBox.Text = "";
                    countryBox.Text = "";
                    postalBox.Text = "";
                    nameBox.Text = "";
                    surnameBox.Text = "";
                    mailBox.Text = "";
                    phoneBox.Text = "";
                    loginBox.Text = "";
                    passwordBox.Text = "";
                }
                else
                { 
                    MessageBox.Show("Admin creation failed"); 
                }
            }
            else
                MessageBox.Show("Address creation failed");
        }

        private void createOfficial_Click(object sender, RoutedEventArgs e)
        {
            Official.address = new Address();

            if (UserORM.GetMaxAddressId() != -1)
            {
                Official.address.Id = UserORM.GetMaxAddressId();
            }
            else
            {
                MessageBox.Show("Address ID is not correct.");
                return;
            }

            Official.address.Street = streetBox.Text;
            Official.address.StreetNumber = streetNoBox.Text;
            Official.address.City = cityBox.Text;
            Official.address.Country = countryBox.Text;
            Official.address.PostalCode = postalBox.Text;

            Official.Name = nameBoxOff.Text;
            Official.SurName = surnameBoxOff.Text;
            Official.Mail = mailBoxOff.Text;
            Official.Phone = phoneBoxOff.Text;
            Official.CompanyNumber = companyBoxOff.Text;
            Official.Password = passwordBoxOff.Text;

            if (typeComboBoxOff.SelectedIndex == -1)
            {
                MessageBox.Show("Select the type");
                return;
            }
            Official.OfficialType = (OfficialType)typeComboBoxOff.SelectedItem;

            if (!Validator.validateOfficial(Official)) return;

            if (UserORM.CreateAddress(Official.address))
            {
                if (UserORM.CreateNewOfficial(Official))
                { 
                    MessageBox.Show("Official " + Official.CompanyNumber + " was successfully created");

                    streetBox.Text = "";
                    streetNoBox.Text = "";
                    cityBox.Text = "";
                    countryBox.Text = "";
                    postalBox.Text = "";
                    nameBoxOff.Text = "";
                    surnameBoxOff.Text = "";
                    mailBoxOff.Text = "";
                    phoneBoxOff.Text = "";
                    companyBoxOff.Text = "";
                    passwordBoxOff.Text = "";
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

        private void updatePassAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin.Password = changePassBox.Text;

            if (UserORM.UpdateAdmin(Admin))
            {
                MessageBox.Show("Password changed successfully");
                changePassBox.Text = String.Empty;
            }
            else
            {
                MessageBox.Show("Password change failed");
                changePassBox.Text = String.Empty;
            }
        }

        private void itemAccPass_Click(object sender, RoutedEventArgs e)
        {
            //change password - visible
            changePassLabel.Visibility  = Visibility.Visible;
            changePassLabel2.Visibility = Visibility.Visible;
            changePassBox.Visibility    = Visibility.Visible;
            updatePassAdmin.Visibility  = Visibility.Visible;

            //create admin - invisible
            newAdmLabel.Visibility  = Visibility.Hidden;
            newAdmLabel1.Visibility = Visibility.Hidden;
            newAdmLabel2.Visibility = Visibility.Hidden;
            newAdmLabel3.Visibility = Visibility.Hidden;
            newAdmLabel4.Visibility = Visibility.Hidden;
            newAdmLabel5.Visibility = Visibility.Hidden;
            newAdmLabel6.Visibility = Visibility.Hidden;
            newAdmLabel7.Visibility = Visibility.Hidden;
            newAdmLabel8.Visibility = Visibility.Hidden;
            nameBox.Visibility      = Visibility.Hidden;
            surnameBox.Visibility   = Visibility.Hidden;
            addressBox.Visibility   = Visibility.Hidden;
            mailBox.Visibility      = Visibility.Hidden;
            phoneBox.Visibility     = Visibility.Hidden;
            loginBox.Visibility     = Visibility.Hidden;
            passwordBox.Visibility  = Visibility.Hidden;
            typeComboBox.Visibility = Visibility.Hidden;
            newAddLabel1.Visibility = Visibility.Hidden;
            newAddLabel2.Visibility = Visibility.Hidden;
            newAddLabel3.Visibility = Visibility.Hidden;
            newAddLabel4.Visibility = Visibility.Hidden;
            newAddLabel5.Visibility = Visibility.Hidden;
            streetBox.Visibility    = Visibility.Hidden;
            streetNoBox.Visibility  = Visibility.Hidden;
            cityBox.Visibility      = Visibility.Hidden;
            postalBox.Visibility    = Visibility.Hidden;
            countryBox.Visibility   = Visibility.Hidden;
            createAdmin.Visibility  = Visibility.Hidden;

            //create official - invisible
            newOffLabel.Visibility      = Visibility.Hidden;
            newOffLabel1.Visibility     = Visibility.Hidden;
            newOffLabel2.Visibility     = Visibility.Hidden;
            newOffLabel3.Visibility     = Visibility.Hidden;
            newOffLabel4.Visibility     = Visibility.Hidden;
            newOffLabel5.Visibility     = Visibility.Hidden;
            newOffLabel6.Visibility     = Visibility.Hidden;
            newOffLabel7.Visibility     = Visibility.Hidden;
            newOffLabel8.Visibility     = Visibility.Hidden;
            nameBoxOff.Visibility       = Visibility.Hidden;
            surnameBoxOff.Visibility    = Visibility.Hidden;
            addressBoxOff.Visibility    = Visibility.Hidden;
            mailBoxOff.Visibility       = Visibility.Hidden;
            phoneBoxOff.Visibility      = Visibility.Hidden;
            companyBoxOff.Visibility    = Visibility.Hidden;
            passwordBoxOff.Visibility   = Visibility.Hidden;
            typeComboBoxOff.Visibility  = Visibility.Hidden;
            newAddLabel1.Visibility     = Visibility.Hidden;
            newAddLabel2.Visibility     = Visibility.Hidden;
            newAddLabel3.Visibility     = Visibility.Hidden;
            newAddLabel4.Visibility     = Visibility.Hidden;
            newAddLabel5.Visibility     = Visibility.Hidden;
            streetBox.Visibility        = Visibility.Hidden;
            streetNoBox.Visibility      = Visibility.Hidden;
            cityBox.Visibility          = Visibility.Hidden;
            postalBox.Visibility        = Visibility.Hidden;
            countryBox.Visibility       = Visibility.Hidden;
            createOfficial.Visibility   = Visibility.Hidden;
        }

        private void itemAccNew_Click(object sender, RoutedEventArgs e)
        {
            //change password - invisible
            changePassLabel.Visibility  = Visibility.Hidden;
            changePassLabel2.Visibility = Visibility.Hidden;
            changePassBox.Visibility    = Visibility.Hidden;
            updatePassAdmin.Visibility  = Visibility.Hidden;

            //create admin - visible
            newAdmLabel.Visibility  = Visibility.Visible;
            newAdmLabel1.Visibility = Visibility.Visible;
            newAdmLabel2.Visibility = Visibility.Visible;
            newAdmLabel3.Visibility = Visibility.Visible;
            newAdmLabel4.Visibility = Visibility.Visible;
            newAdmLabel5.Visibility = Visibility.Visible;
            newAdmLabel6.Visibility = Visibility.Visible;
            newAdmLabel7.Visibility = Visibility.Visible;
            newAdmLabel8.Visibility = Visibility.Visible;
            nameBox.Visibility      = Visibility.Visible;
            surnameBox.Visibility   = Visibility.Visible;
            addressBox.Visibility   = Visibility.Visible;
            mailBox.Visibility      = Visibility.Visible;
            phoneBox.Visibility     = Visibility.Visible;
            loginBox.Visibility     = Visibility.Visible;
            passwordBox.Visibility  = Visibility.Visible;
            typeComboBox.Visibility = Visibility.Visible;
            newAddLabel1.Visibility = Visibility.Visible;
            newAddLabel2.Visibility = Visibility.Visible;
            newAddLabel3.Visibility = Visibility.Visible;
            newAddLabel4.Visibility = Visibility.Visible;
            newAddLabel5.Visibility = Visibility.Visible;
            streetBox.Visibility    = Visibility.Visible;
            streetNoBox.Visibility  = Visibility.Visible;
            cityBox.Visibility      = Visibility.Visible;
            postalBox.Visibility    = Visibility.Visible;
            countryBox.Visibility   = Visibility.Visible;
            createAdmin.Visibility  = Visibility.Visible;

            //create official - invisible
            newOffLabel.Visibility      = Visibility.Hidden;
            newOffLabel1.Visibility     = Visibility.Hidden;
            newOffLabel2.Visibility     = Visibility.Hidden;
            newOffLabel3.Visibility     = Visibility.Hidden;
            newOffLabel4.Visibility     = Visibility.Hidden;
            newOffLabel5.Visibility     = Visibility.Hidden;
            newOffLabel6.Visibility     = Visibility.Hidden;
            newOffLabel7.Visibility     = Visibility.Hidden;
            newOffLabel8.Visibility     = Visibility.Hidden;
            nameBoxOff.Visibility       = Visibility.Hidden;
            surnameBoxOff.Visibility    = Visibility.Hidden;
            addressBoxOff.Visibility    = Visibility.Hidden;
            mailBoxOff.Visibility       = Visibility.Hidden;
            phoneBoxOff.Visibility      = Visibility.Hidden;
            companyBoxOff.Visibility    = Visibility.Hidden;
            passwordBoxOff.Visibility   = Visibility.Hidden;
            typeComboBoxOff.Visibility  = Visibility.Hidden;
            createOfficial.Visibility   = Visibility.Hidden;

            //empty the boxes
            streetBox.Text      = "";
            streetNoBox.Text    = "";
            cityBox.Text        = "";
            countryBox.Text     = "";
            postalBox.Text      = "";
            nameBoxOff.Text     = "";
            surnameBoxOff.Text  = "";
            mailBoxOff.Text     = "";
            phoneBoxOff.Text    = "";
            companyBoxOff.Text  = "";
            passwordBoxOff.Text = "";
            typeComboBoxOff.SelectedIndex = -1;
        }

        private void itemAccNewOff_Click(object sender, RoutedEventArgs e)
        {
            //change password - invisible
            changePassLabel.Visibility  = Visibility.Hidden;
            changePassLabel2.Visibility = Visibility.Hidden;
            changePassBox.Visibility    = Visibility.Hidden;
            updatePassAdmin.Visibility  = Visibility.Hidden;

            //create admin - invisible
            newAdmLabel.Visibility  = Visibility.Hidden;
            newAdmLabel1.Visibility = Visibility.Hidden;
            newAdmLabel2.Visibility = Visibility.Hidden;
            newAdmLabel3.Visibility = Visibility.Hidden;
            newAdmLabel4.Visibility = Visibility.Hidden;
            newAdmLabel5.Visibility = Visibility.Hidden;
            newAdmLabel6.Visibility = Visibility.Hidden;
            newAdmLabel7.Visibility = Visibility.Hidden;
            newAdmLabel8.Visibility = Visibility.Hidden;
            nameBox.Visibility      = Visibility.Hidden;
            surnameBox.Visibility   = Visibility.Hidden;
            addressBox.Visibility   = Visibility.Hidden;
            mailBox.Visibility      = Visibility.Hidden;
            phoneBox.Visibility     = Visibility.Hidden;
            loginBox.Visibility     = Visibility.Hidden;
            passwordBox.Visibility  = Visibility.Hidden;
            typeComboBox.Visibility = Visibility.Hidden;
            createAdmin.Visibility  = Visibility.Hidden;

            //create official - visible
            newOffLabel.Visibility      = Visibility.Visible;
            newOffLabel1.Visibility     = Visibility.Visible;
            newOffLabel2.Visibility     = Visibility.Visible;
            newOffLabel3.Visibility     = Visibility.Visible;
            newOffLabel4.Visibility     = Visibility.Visible;
            newOffLabel5.Visibility     = Visibility.Visible;
            newOffLabel6.Visibility     = Visibility.Visible;
            newOffLabel7.Visibility     = Visibility.Visible;
            newOffLabel8.Visibility     = Visibility.Visible;
            nameBoxOff.Visibility       = Visibility.Visible;
            surnameBoxOff.Visibility    = Visibility.Visible;
            addressBoxOff.Visibility    = Visibility.Visible;
            mailBoxOff.Visibility       = Visibility.Visible;
            phoneBoxOff.Visibility      = Visibility.Visible;
            companyBoxOff.Visibility    = Visibility.Visible;
            passwordBoxOff.Visibility   = Visibility.Visible;
            typeComboBoxOff.Visibility  = Visibility.Visible;
            newAddLabel1.Visibility     = Visibility.Visible;
            newAddLabel2.Visibility     = Visibility.Visible;
            newAddLabel3.Visibility     = Visibility.Visible;
            newAddLabel4.Visibility     = Visibility.Visible;
            newAddLabel5.Visibility     = Visibility.Visible;
            streetBox.Visibility        = Visibility.Visible;
            streetNoBox.Visibility      = Visibility.Visible;
            cityBox.Visibility          = Visibility.Visible;
            postalBox.Visibility        = Visibility.Visible;
            countryBox.Visibility       = Visibility.Visible;
            createOfficial.Visibility   = Visibility.Visible;

            //empty the boxes
            streetBox.Text      = "";
            streetNoBox.Text    = "";
            cityBox.Text        = "";
            countryBox.Text     = "";
            postalBox.Text      = "";
            nameBox.Text        = "";
            surnameBox.Text     = "";
            mailBox.Text        = "";
            phoneBox.Text       = "";
            loginBox.Text       = "";
            passwordBox.Text    = "";
            typeComboBoxOff.SelectedIndex = -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }
    }
}
