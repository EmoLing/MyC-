using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BankSystem;
using Clients;
using Clients.VIP;


namespace Bank_System
{
    /// <summary>
    /// Логика взаимодействия для AddWindowLegalEnity.xaml
    /// </summary>
    public partial class AddWindowLegalEnity : Window
    {
        private Bank<NaturalClient, LegalEnity, VipClient> Bank;
        public AddWindowLegalEnity(Bank<NaturalClient, LegalEnity, VipClient> Bank)
        {
            this.Bank = Bank;
            InitializeComponent();
        }

        private void BoxSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        static bool TextIsDate(string text)
        {
            var dateFormat = "dd.MM.yyyy";
            var dateFormat2 = "dd,MM,yyyy";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate)
                || DateTime.TryParseExact(text, dateFormat2, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }

        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();
            if (TextIsDate(BoxBirthday.Text))
            {
                if (BoxName.Text != string.Empty&& BoxSum.Text != string.Empty)
                {
                    var temp = new SimpleLegalEnity(BoxName.Text, "Юридическое лицо",
                        DateTime.Parse(BoxBirthday.Text), decimal.Parse(BoxSum.Text));
                    Bank.Add(temp);
                    dataBase.GoToDataBase(dataBase.CreateSQL(temp) + $@"
(
{temp.ID}, N'{temp.Name}', '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}', N'{temp.reputation}',N'{temp.Department}', {temp.AccountNumber},
{temp.AmountOfMoney}, {temp.CheckContribution}, {temp.CheckDebt}
)
");
                    Close();
                }
                else MessageBox.Show("Не все поля заполнены", "WARNING", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

            }
            else
                MessageBox.Show("Введите корректную дату создания", "WARNING", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        /// <summary>
        /// Открытие календаря
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButOpenCalen_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> date_time_of_create = new List<DateTime>();
            WindowCalendar windowCalendar = new WindowCalendar(date_time_of_create);

            windowCalendar.Owner = this;
            this.IsEnabled = false;
            if (windowCalendar.ShowDialog() == true)
            {
                windowCalendar.Show();
                windowCalendar.Activate();
            }
            this.IsEnabled = true;
            if (date_time_of_create.Count > 0)
            {
                this.BoxBirthday.Text = date_time_of_create[0].Date.ToShortDateString();
            }
        }

        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
