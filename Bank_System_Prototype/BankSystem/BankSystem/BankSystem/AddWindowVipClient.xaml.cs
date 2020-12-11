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
    /// Логика взаимодействия для AddWindowVipClient.xaml
    /// </summary>
    public partial class AddWindowVipClient : Window
    {
        private Bank<NaturalClient, LegalEnity, VipClient> Bank;
        public AddWindowVipClient(Bank<NaturalClient, LegalEnity, VipClient> Bank)
        {
            this.Bank = Bank;
            InitializeComponent();
            ButAdd.Click += delegate { AddClient(); };
            ButCancel.Click += delegate { Close(); };
        }

        /// <summary>
        /// Переключение на физическое лицо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioNatural_Checked(object sender, RoutedEventArgs e)
        {
            BoxName.IsEnabled = false;
            BoxCreateDate.IsEnabled = false;
            BoxSumLegal.IsEnabled = false;


            BoxFirstName.IsEnabled = true;
            BoxLastName.IsEnabled = true;
            BoxBirthday.IsEnabled = true;
            BoxSumNatur.IsEnabled = true;

        }

        /// <summary>
        /// Переключение на юридическое лицо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioLegal_Checked(object sender, RoutedEventArgs e)
        {
            BoxName.IsEnabled = true;
            BoxCreateDate.IsEnabled = true;
            BoxSumLegal.IsEnabled = true;


            BoxFirstName.IsEnabled = false;
            BoxLastName.IsEnabled = false;
            BoxBirthday.IsEnabled = false;
            BoxSumNatur.IsEnabled = false;

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
            if (RadioNatural.IsChecked == true)
            {
                if (date_time_of_create.Count > 0)
                {
                    this.BoxBirthday.Text = date_time_of_create[0].Date.ToShortDateString();
                }
            }
            else
            {
                {
                    if (date_time_of_create.Count > 0)
                    {
                        this.BoxCreateDate.Text = date_time_of_create[0].Date.ToShortDateString();
                    }
                }
            }
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        private void AddClient()
        {
            SQLDataBase dataBase = new SQLDataBase();
            if (RadioNatural.IsChecked == true)
            {
                if (TextIsDate(BoxBirthday.Text))
                {
                    if (BoxFirstName.Text != string.Empty && BoxLastName.Text != string.Empty &&
                        BoxBirthday.Text != string.Empty && BoxSumNatur.Text != string.Empty)
                    {
                        var temp= new VipNaturalClient(BoxFirstName.Text, BoxLastName.Text, "VIP",
                            DateTime.Parse(BoxBirthday.Text), decimal.Parse(BoxSumNatur.Text));
                        Bank.Add(temp);
                        dataBase.GoToDataBase(dataBase.CreateSQL(temp) + $@"
(
{temp.ID}, N'{temp.FirstName}', N'{temp.LastName}', '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}',
N'{temp.Department}', {temp.AccountNumber},
{temp.AmountOfMoney}, {temp.CheckContribution}, {temp.CheckDebt}
)
");
                        Close();
                    }
                    else
                        MessageBox.Show("Не все поля введены!", "WARNING", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                }
                else
                    MessageBox.Show("введите дату в формате dd.MM.yyyy", "WARNING", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                if (TextIsDate(BoxCreateDate.Text))
                {
                    if (BoxName.Text != string.Empty && 
                        BoxCreateDate.Text != string.Empty && BoxSumLegal.Text != string.Empty)
                    {
                        var temp = new VipLegalEnity(BoxName.Text, "VIP",
                            DateTime.Parse(BoxCreateDate.Text), decimal.Parse(BoxSumLegal.Text));
                        Bank.Add(temp);
                        dataBase.GoToDataBase(dataBase.CreateSQL(temp) + $@"
(
{temp.ID}, N'{temp.Name}', '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}', N'{temp.Department}', {temp.AccountNumber},
{temp.AmountOfMoney}, {temp.CheckContribution}, {temp.CheckDebt}
)
");
                        Close();
                    }
                    else
                        MessageBox.Show("Не все поля введены!", "WARNING", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                }
                else
                    MessageBox.Show("введите дату в формате dd.MM.yyyy", "WARNING", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Проверка на то, чтобы дата была по формату dd.mm.yyyy
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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


        private void BoxSumLegal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BoxSumNatur_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
