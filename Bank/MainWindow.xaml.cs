using Bank.Logger;
using Bank.Objects;
using Bank.ORM;
using Bank.Validation;
using System.Windows;

namespace Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserORM UserORM { get; set; }
        private Validator Validator { get; set; }
        private Admin Admin { get; set; }
        private Official Official { get; set; }
        private Customer Customer { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Validator = new Validator();
            Official = new Official();
            Admin = new Admin();
            Customer = new Customer();
            UserORM = new UserORM();
            Log logger = new Log();
            logger.Info("Main window opened");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool numericValid = Validator.isNumeric(Login.Text);

            // SSN's length must be 10
            bool SSNValid = Validator.isSSN(Login.Text);

            if (numericValid && !SSNValid)
            {
                //official login
                Official.CompanyNumber = Login.Text;
                Official.Password = Password.Password;

                if (UserORM.GetOfficialById(Official) != null)
                {
                    OfficialWindow officialWindow = new OfficialWindow(Official);
                    officialWindow.Show();

                    Close();
                }
            }
            else if (numericValid && SSNValid)
            {
                //customer login
                Customer = UserORM.GetCustomer(Login.Text, Password.Password);

                if(Customer != null) 
                {
                    CustomerWindow customerWindow = new CustomerWindow(Customer);
                    customerWindow.Show();

                    Close();
                }
            }
            else
            {
                //admin login
                Admin = UserORM.GetAdmin(Login.Text, Password.Password);

                if (Admin != null)
                {
                    AdminWindow adminWindow = new AdminWindow(Admin);
                    adminWindow.Show();

                    Close();
                }
            }
        }
    }
}
