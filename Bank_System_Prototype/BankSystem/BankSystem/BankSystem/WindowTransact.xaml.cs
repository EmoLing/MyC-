using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
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
using InterfasesLib;


namespace Bank_System
{
    /// <summary>
    /// Логика взаимодействия для WindowTransact.xaml
    /// </summary>
    public partial class WindowTransact : Window
    {
        Bank<NaturalClient, LegalEnity, VipClient> Bank;
        private object item; //КТО ПЕРЕВОДИТ
        private object item2; //КОМУ ПЕРЕВОДЯТ
        public WindowTransact(Bank<NaturalClient, LegalEnity, VipClient> Bank, object item)
        {
            this.item = item;
            this.Bank = Bank;
            InitializeComponent();
        }

        /// <summary>
        /// При изменении textbox будет искаться клиент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxAccountNumber_TextChanged(object sender, TextChangedEventArgs e) => Find();

        /// <summary>
        /// Функция поиска
        /// </summary>
        private void Find()
        {
            try
            {
                if (ComboDepartment.Text == "Физический")
                {
                    var a = Bank.Find<SimpleNaturalClient>(int.Parse(BoxAccountNumber.Text), ComboDepartment.Text);
                    if (a as SimpleNaturalClient != null)
                    {
                        LabelName.Content = $"{(a as SimpleNaturalClient).FirstName} {(a as SimpleNaturalClient).LastName}";
                        item2 = a;
                    }
                    else LabelName.Content = a;
                }
                else if (ComboDepartment.Text == "Юридический")
                {
                    var a = Bank.Find<SimpleLegalEnity>(int.Parse(BoxAccountNumber.Text), ComboDepartment.Text);
                    if (a as SimpleLegalEnity != null)
                    {
                        LabelName.Content = $"{(a as SimpleLegalEnity).Name}";
                        item2 = a;
                    }
                    else LabelName.Content = a;
                }
                else if (ComboDepartment.Text == "VIP")
                {
                    var a = Bank.Find<VipClient>(int.Parse(BoxAccountNumber.Text), ComboDepartment.Text);
                    if (a as VipClient != null)
                    {
                        if (a as VipNaturalClient != null)
                        {
                            LabelName.Content = $"{(a as VipNaturalClient).FirstName} {(a as VipNaturalClient).LastName}";
                            item2 = a;
                        }
                        else if (a as VipLegalEnity != null)
                        {
                            LabelName.Content = $"{(a as VipLegalEnity).Name}";
                            item2 = a;
                        }
                        else LabelName.Content = a;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Обработчкик события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToLogMessage(object sender, AccountEventArgs e)
        {
            string path = $"logs/{DateTime.Now.ToShortDateString()}_log_transacts.txt";
            DirectoryInfo directoryInfo = new DirectoryInfo("logs");
            if (directoryInfo.Exists == false)
                Directory.CreateDirectory("logs");
            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine(e.Message);
                streamWriter.WriteLine($"Сумма транзакции: {e.Sum} рублей");
            }
            FileInfo fileInfo = new FileInfo(path);
            MessageBox.Show($"Транзакция была записана в log расположенный по пути: \n {fileInfo.FullName}",
                "Complete!", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        /// <summary>
        /// Кнопка - транзацкция
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButTransact_Click(object sender, RoutedEventArgs e)
        {
            if (item2 != null)
            {
                var Client1 = item as IAccount;
                var Client2 = item2 as IAccount;
                if (item is SimpleNaturalClient)
                {
                    if ((item as SimpleNaturalClient).AmountOfMoney > decimal.Parse(BoxSum.Text))
                    {
                        if (!item.Equals(item2))
                        {
                            Bank.Transact(Client1, Client2, decimal.Parse(BoxSum.Text));
                            SQLupdate(item);
                            SQLupdate(item2);
                            Close();
                        }
                        else MessageBox.Show("Нельзя перевести самому себе", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else MessageBox.Show("Недостаточно денег для перевода", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (item is SimpleLegalEnity)
                {
                    if ((item as SimpleLegalEnity).AmountOfMoney > decimal.Parse(BoxSum.Text))
                    {
                        if (!item.Equals(item2))
                        {
                            Bank.Transact(Client1, Client2, decimal.Parse(BoxSum.Text));
                            SQLupdate(item);
                            SQLupdate(item2);
                            Close();
                        }
                        else MessageBox.Show("Нельзя перевести самому себе", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else if (item is VipClient)
                {
                    if ((item as VipClient).AmountOfMoney > decimal.Parse(BoxSum.Text))
                    {
                        if (!item.Equals(item2))
                        {
                            Bank.Transact(Client1, Client2, decimal.Parse(BoxSum.Text));
                            SQLupdate(item);
                            SQLupdate(item2);
                            Close();
                        }
                        else MessageBox.Show("Нельзя перевести самому себе", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else MessageBox.Show("Нельзя перевести в пустоту", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Кнопка - поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButSearch_Click(object sender, RoutedEventArgs e) => Find();

        /// <summary>
        /// Ограничение на ввод только цифр в аккаунте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxAccountNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Кнопка - отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButCancel_Click(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Ограничение на ввод только цифр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Выборка в ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoxAccountNumber != null)
            {
                BoxAccountNumber.Clear();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bank.Notify += SaveToLogMessage;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Bank.Notify -= SaveToLogMessage;
        }

        private void SQLupdate<T>(T client)
        {
            SQLDataBase dataBase = new SQLDataBase();
            string sql = string.Empty;
            if (client is SimpleNaturalClient)
            {
                var temp = client as SimpleNaturalClient;
                sql = $@"
UPDATE AllNaturalClients SET AmountOfMoney = {temp.AmountOfMoney} WHERE Id = {temp.ID}
";
            }
            else if (client is SimpleLegalEnity)
            {
                var temp = client as SimpleLegalEnity;
                sql = $@"
UPDATE AllLegalClients SET AmountOfMoney = {temp.AmountOfMoney} WHERE Id = {temp.ID}
";
            }
            else if (client is VipNaturalClient)
            {
                var temp = client as VipNaturalClient;
                sql = $@"
UPDATE AllVipNaturalClients SET AmountOfMoney = {temp.AmountOfMoney} WHERE Id = {temp.ID}
";
            }
            else if (client is VipLegalEnity)
            {
                var temp = client as VipLegalEnity;
                sql = $@"
UPDATE AllVipLegalClients SET AmountOfMoney = {temp.AmountOfMoney} WHERE Id = {temp.ID}
";
            }
            dataBase.sql_upgade(sql);
        }
    }
}
