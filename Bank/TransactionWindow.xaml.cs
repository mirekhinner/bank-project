using Bank.Logger;
using Bank.Objects;
using Bank.ORM;
using Bank.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bank
{
    /// <summary>
    /// Interaction logic for TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        private Official Official { get; set; }
        private UserORM UserORM { get; set; }
        private Validator Validator { get; set; }
        private List<Transaction> TransList { get; set; }
        private Bill Bill { get; set; }
        private Customer Customer { get; set; }

        public TransactionWindow(Official official)
        {
            InitializeComponent();

            Official = official;

            UserORM = new UserORM();
            Validator = new Validator();
            Log logger = new Log();
            logger.Info("Transaction window opened by official: " + official.CompanyNumber);
        }

        private void SSNSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.isEmpty(SSNSearchBox.Text) || !Validator.isSSN(SSNSearchBox.Text))
            {
                MessageBox.Show("Invalid SSN");
                return;
            }

            Customer = UserORM.findCustomerInfo(SSNSearchBox.Text);

            if (Customer != null)
            {
                PhoneLabel.Content = Customer.Phone;
                EmailLabel.Content = Customer.Mail;
                SSNLabel.Content = Customer.SSN;
                SurnameLabel.Content = Customer.SurName;
                NameLabel.Content = Customer.Name;

                StreetLabel.Content = Customer.address.Street;
                StreetNoLabel.Content = Customer.address.StreetNumber;
                CityLabel.Content = Customer.address.City;
                PostCodeLabel.Content = Customer.address.PostalCode;
                CountryLabel.Content = Customer.address.Country;
            }
            else return;

            Bill = UserORM.GetBill(Customer);

            if (Bill != null)
                BalanceLabel.Content = Bill.Balance;

            TransList = UserORM.GetMyTransactions(Bill);
            TransactionsListView.ItemsSource = TransList;

            incomingCheckbox.IsEnabled = true;
            outcomingCheckbox.IsEnabled = true;
            sortAmountButton.IsEnabled = true;
            sortDateButton.IsEnabled = true;
            rangeOneTextBox.IsEnabled = true;
            rangeTwoTextBox.IsEnabled = true;
            applyRangeButton.IsEnabled = true;
            refreshButton.IsEnabled = true;
        }

        private void incomingCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            TransactionsListView.ItemsSource = TransList.Where(x => x.Recipient == Bill.BillNumber);

            if (outcomingCheckbox.IsChecked == true)
            {
                TransactionsListView.ItemsSource = TransList;
            }
        }

        private void outcomingCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            TransactionsListView.ItemsSource = TransList.Where(x => x.Payer == Bill.BillNumber);

            if (incomingCheckbox.IsChecked == true)
            {
                TransactionsListView.ItemsSource = TransList;
            }
        }

        private void sortAmountButton_Click(object sender, RoutedEventArgs e)
        {
            if (TransList.SequenceEqual(TransList.OrderBy(x => x.Amount)))
            {
                TransList = TransList.OrderByDescending(x => x.Amount).ToList();
                TransactionsListView.ItemsSource = TransList;
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
                rangeOneTextBox.Text = null;
                rangeTwoTextBox.Text = null;
            }
            else if (TransList.SequenceEqual(TransList.OrderByDescending(x => x.Amount)))
            {
                TransList = TransList.OrderBy(x => x.Amount).ToList();
                TransactionsListView.ItemsSource = TransList;
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
                rangeOneTextBox.Text = null;
                rangeTwoTextBox.Text = null;
            }
            else
            {
                TransList = TransList.OrderByDescending(x => x.Amount).ToList();
                TransactionsListView.ItemsSource = TransList;
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
                rangeOneTextBox.Text = null;
                rangeTwoTextBox.Text = null;
            }
        }

        private void sortDateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TransList.SequenceEqual(TransList.OrderBy(x => x.DateTransaction)))
            {
                TransList = TransList.OrderByDescending(x => x.DateTransaction).ToList();
                TransactionsListView.ItemsSource = TransList;
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
            }
            else if (TransList.SequenceEqual(TransList.OrderByDescending(x => x.DateTransaction)))
            {
                TransList = TransList.OrderBy(x => x.DateTransaction).ToList();
                TransactionsListView.ItemsSource = TransList;
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
            }
            else
            {
                TransList = TransList.OrderByDescending(x => x.DateTransaction).ToList();
                TransactionsListView.ItemsSource = TransList;
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
            }
        }

        private void incomingCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (outcomingCheckbox.IsChecked == true)
            {
                TransactionsListView.ItemsSource = TransList.Where(x => x.Payer == Bill.BillNumber);
            }
            else
            {
                TransactionsListView.ItemsSource = null;
            }
        }

        private void outcomingCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (incomingCheckbox.IsChecked == true)
            {
                TransactionsListView.ItemsSource = TransList.Where(x => x.Recipient == Bill.BillNumber);
            }
            else
            {
                TransactionsListView.ItemsSource = null;
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            TransList = UserORM.GetMyTransactions(Bill);
            TransactionsListView.ItemsSource = TransList;

            rangeOneTextBox.Text = null;
            rangeTwoTextBox.Text = null;
            incomingCheckbox.IsChecked = false;
            outcomingCheckbox.IsChecked = false;
        }

        private void applyRangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.isEmpty(rangeOneTextBox.Text) && Validator.isEmpty(rangeTwoTextBox.Text))
            {
                TransactionsListView.ItemsSource = TransList;
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
                return;
            }
            else if (!Validator.isNumeric(rangeOneTextBox.Text) || !Validator.isNumeric(rangeTwoTextBox.Text))
            {
                MessageBox.Show("Enter correct numbers");
                return;
            }

            int rangeOne = Int32.Parse(rangeOneTextBox.Text);
            int rangeTwo = Int32.Parse(rangeTwoTextBox.Text);

            if (rangeOne < rangeTwo)
            {
                TransactionsListView.ItemsSource = TransList.Where(x => (x.Amount > rangeOne) && (x.Amount < rangeTwo));
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
            }
            else if (rangeOne > rangeTwo)
            {
                TransactionsListView.ItemsSource = TransList.Where(x => (x.Amount < rangeOne) && (x.Amount > rangeTwo));
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
            }
            else 
            {
                TransactionsListView.ItemsSource = TransList.Where(x => x.Amount == rangeOne);
                incomingCheckbox.IsChecked = false;
                outcomingCheckbox.IsChecked = false;
            }
        }
    }
}