using Bank.Logger;
using Bank.Objects;
using Bank.ORM;
using System.Windows;


namespace Bank
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public Customer Customer { get; set; }

        public CustomerWindow(Customer customer)
        {            
            InitializeComponent();
            Customer = customer;

            Log logger = new Log();
            logger.Info("Customer window opened. Customer: " + customer.SSN);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }
    }
}
