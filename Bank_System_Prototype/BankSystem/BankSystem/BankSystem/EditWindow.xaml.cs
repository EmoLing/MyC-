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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        object item;
        Bank<NaturalClient, LegalEnity, VipClient> Bank;
        public EditWindow(Bank<NaturalClient, LegalEnity, VipClient> Bank, object item)
        {
            this.item = item;
            this.Bank = Bank;
            InitializeComponent();
            ButCancel.Click += delegate { this.Close(); };
            ButEdit.Click += delegate { EditClient(); };
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


            BoxFirstName.IsEnabled = true;
            BoxLastName.IsEnabled = true;
            BoxBirthday.IsEnabled = true;

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


            BoxFirstName.IsEnabled = false;
            BoxLastName.IsEnabled = false;
            BoxBirthday.IsEnabled = false;

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
        /// Редактировать клиента
        /// </summary>
        private void EditClient()
        {
            SQLDataBase dataBase = new SQLDataBase();
            string sql = string.Empty;
            if (RadioNatural.IsChecked == true)
            {
                if (TextIsDate(BoxBirthday.Text))
                {
                    if (BoxFirstName.Text != string.Empty && BoxLastName.Text != string.Empty &&
                        BoxBirthday.Text != string.Empty)
                    {
                        if (item is SimpleNaturalClient)
                        {
                            var temp = item as NaturalClient;
                            Bank.Edit(item as NaturalClient, BoxFirstName.Text, BoxLastName.Text,
                                GetAge(BoxBirthday.Text));
                            sql = $@"
UPDATE AllNaturalClients SET [FirstName] = N'{temp.FirstName}', [LastName] = N'{temp.LastName}', 
[DateOfBirth] = '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}' 
WHERE Id = {temp.ID}
" + "\n";
                            dataBase.sql_upgade(sql);
                        }
                        else
                        {
                            var temp = item as VipNaturalClient;
                            Bank.Edit(item as VipNaturalClient, BoxFirstName.Text, BoxLastName.Text,
                                GetAge(BoxBirthday.Text));
                            sql = $@"
UPDATE AllVipNaturalClients SET [FirstName] = N'{temp.FirstName}', [LastName] = N'{temp.LastName}', 
[DateOfBirth] = '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}'
WHERE Id = {temp.ID}
" + "\n";
                            dataBase.sql_upgade(sql);
                        }
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
                    if (BoxName.Text != string.Empty  &&
                        BoxCreateDate.Text != string.Empty)
                    {
                        if (item is SimpleLegalEnity)
                        {
                            var temp = item as SimpleLegalEnity;
                            Bank.Edit(item as SimpleLegalEnity, BoxName.Text, GetAge(BoxCreateDate.Text));
                            sql = $@"
UPDATE AllLegalClients SET [Name] = N'{temp.Name}', 
[DateOfBirth] = '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}'
WHERE Id = {temp.ID}
" + "\n";
                            dataBase.sql_upgade(sql);
                        }
                        else
                        {
                            var temp = item as VipLegalEnity;
                            Bank.Edit(item as VipLegalEnity, BoxName.Text, GetAge(BoxCreateDate.Text));
                            sql = $@"
UPDATE AllVipLegalClients SET [Name] = N'{temp.Name}', 
[DateOfBirth] = '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}'
WHERE Id = {temp.ID}
" + "\n";
                            dataBase.sql_upgade(sql);
                        }
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

        /// <summary>
        /// ограничение на ввод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxSumLegal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// ограничение на ввод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxSumNatur_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Получение возраста
        /// </summary>
        /// <param name="DateOfBirth_string"></param>
        /// <returns></returns>
        private int GetAge(string DateOfBirth_string)
        {
            DateTime DateOfBirth = DateTime.Parse(DateOfBirth_string);
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year - DateOfBirth.Year;
            if (dateTime.Month < DateOfBirth.Month || (dateTime.Month == DateOfBirth.Month
                                                       && dateTime.Day < DateOfBirth.Day))
                year--;
            return year;
        }

        /// <summary>
        /// Инициализация окна, заполнение нужных полей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized(object sender, EventArgs e)
        {
            if (item is NaturalClient || item is VipNaturalClient)
            {
                RadioNatural.IsChecked = true;
                if (item is NaturalClient)
                {
                    BoxFirstName.Text = (item as NaturalClient).FirstName;
                    BoxLastName.Text = (item as NaturalClient).LastName;
                    BoxBirthday.Text = (item as NaturalClient).DateofBirth;
                }
                else
                {
                    BoxFirstName.Text = (item as VipNaturalClient).FirstName;
                    BoxLastName.Text = (item as VipNaturalClient).LastName;
                    BoxBirthday.Text = (item as VipNaturalClient).DateofBirth;
                }
            }
            else if (item is LegalEnity || item is VipLegalEnity)
            {
                RadioLegal.IsChecked = true;
                if (item is LegalEnity)
                {
                    BoxName.Text = (item as LegalEnity).Name;
                    BoxCreateDate.Text = (item as LegalEnity).DateofBirth;
                }
                else
                {
                    BoxName.Text = (item as VipLegalEnity).Name;
                    BoxCreateDate.Text = (item as VipLegalEnity).DateofBirth;
                }
            }
            RadioLegal.IsEnabled = false;
            RadioNatural.IsEnabled = false;
        }
    }
}

