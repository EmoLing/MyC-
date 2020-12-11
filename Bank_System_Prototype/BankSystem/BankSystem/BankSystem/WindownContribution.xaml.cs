using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для WindownContribution.xaml
    /// </summary>
    public partial class WindownContribution : Window
    {
        private object item;
        private bool capit = true;
        public WindownContribution(object item)
        {
            this.item = item;
            InitializeComponent();
        }

        /// <summary>
        /// Кнопка Показать, предварительный просмотр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButSeeSum_Click(object sender, RoutedEventArgs e)
        {
            if (BoxCountMonth.Text == string.Empty || BoxSum.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime time = DateTime.Today;
            time = time.AddMonths(-int.Parse(BoxCountMonth.Text));

            if (item is SimpleNaturalClient)
            {
                if (((item as SimpleNaturalClient).AmountOfMoney - decimal.Parse(BoxSum.Text)) < 0)
                {
                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    LabelSum.Content = (item as SimpleNaturalClient).Test_Contribution(capit, DateTime.Now, time, decimal.Parse(BoxSum.Text));
            }
            else if (item is SimpleLegalEnity)
            {
                if (((item as SimpleLegalEnity).AmountOfMoney - decimal.Parse(BoxSum.Text)) < 0)
                {
                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    LabelSum.Content = (item as SimpleLegalEnity).Test_Contribution(capit, DateTime.Now, time, decimal.Parse(BoxSum.Text));
            }
            else if (item is VipNaturalClient)
            {
                if (((item as VipNaturalClient).AmountOfMoney - decimal.Parse(BoxSum.Text)) < 0)
                {
                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    LabelSum.Content = (item as VipNaturalClient).Test_Contribution(capit, DateTime.Now, time, decimal.Parse(BoxSum.Text));
            }
            else if (item is VipLegalEnity)
            {
                if (((item as VipLegalEnity).AmountOfMoney - decimal.Parse(BoxSum.Text)) < 0)
                {
                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    LabelSum.Content = (item as VipLegalEnity).Test_Contribution(capit, DateTime.Now, time, decimal.Parse(BoxSum.Text));
            }
            else
                LabelSum.Content = "Не корректный тип!";
        }

        /// <summary>
        /// Кнопка отмены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Вклад
        /// </summary>
        /// <typeparam name="T">Natural, Legal, VIP</typeparam>
        /// <param name="Capitalization">капитализация true/false</param>
        /// <param name="item">Клиент</param>
        /// <param name="CountMonth">Количество месяцев</param>
        /// <param name="sum">Сумма</param>
        private void Contribution<T>(bool Capitalization, T item, int CountMonth, decimal sum)
        {
            DateTime time = DateTime.Today;
            time = time.AddMonths(-CountMonth);
            if (item is SimpleNaturalClient)
            {
                (item as SimpleNaturalClient).Contribution(Capitalization, DateTime.Today, time, sum);
                LabelSum.Content = (item as SimpleNaturalClient).CheckContribution;
            }
            else if (item is SimpleLegalEnity)
            {
                (item as SimpleLegalEnity).Contribution(Capitalization, DateTime.Today, time, sum);
                LabelSum.Content = (item as SimpleLegalEnity).CheckContribution;
            }
            else if (item is VipNaturalClient)
            {
                (item as VipNaturalClient).Contribution(Capitalization, DateTime.Today, time, sum);
                LabelSum.Content = (item as VipNaturalClient).CheckContribution;
            }
            else if (item is VipLegalEnity)
            {
                (item as VipLegalEnity).Contribution(Capitalization, DateTime.Today, time, sum);
                LabelSum.Content = (item as VipLegalEnity).CheckContribution;
            }
        }

        /// <summary>
        /// RadioBut С капитализацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioWith_Checked(object sender, RoutedEventArgs e)
        {
            capit = true;
            try
            {
                if (BoxCountMonth.Text != string.Empty && BoxSum.Text != string.Empty)
                {
                    ButSeeSum_Click(sender, e);
                }
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// RadioBut Без капитализации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioWithout_Checked(object sender, RoutedEventArgs e)
        {
            capit = false;
            try
            {
                if (BoxCountMonth.Text != string.Empty && BoxSum.Text != string.Empty)
                {
                    ButSeeSum_Click(sender, e);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Открытие Вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButAddCapit_Click(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();
            if (BoxCountMonth.Text == string.Empty || BoxSum.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime time = DateTime.Today;
            time = time.AddMonths(-int.Parse(BoxCountMonth.Text));

            if (item is SimpleNaturalClient)
            {
                if(((item as SimpleNaturalClient).AmountOfMoney - decimal.Parse(BoxSum.Text)) < 0)
                {
                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var temp = item as SimpleNaturalClient;
                    Contribution(capit, item as SimpleNaturalClient, int.Parse(BoxCountMonth.Text),
                        decimal.Parse(BoxSum.Text));
                    using (var cmd = new SqlCommand($@"UPDATE AllNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else if (item is SimpleLegalEnity)
            {
                if ((item as SimpleLegalEnity).AmountOfMoney - decimal.Parse(BoxSum.Text) < 0)
                {
                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var temp = item as SimpleLegalEnity;
                    Contribution(capit, item as SimpleLegalEnity, int.Parse(BoxCountMonth.Text),
                        decimal.Parse(BoxSum.Text));
                    using (var cmd = new SqlCommand($@"UPDATE AllLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else if (item is VipNaturalClient)
            {
                if (((item as VipNaturalClient).AmountOfMoney - decimal.Parse(BoxSum.Text)) < 0)
                {

                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var temp = item as VipNaturalClient;
                    Contribution(capit, item as VipNaturalClient, int.Parse(BoxCountMonth.Text),
                        decimal.Parse(BoxSum.Text));
                    using (var cmd = new SqlCommand($@"UPDATE AllVipNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else if (item is VipLegalEnity)
            {
                if (((item as VipLegalEnity).AmountOfMoney - decimal.Parse(BoxSum.Text)) < 0)
                {

                    MessageBox.Show("НУЖНО БОЛЬШЕ ЗОЛОТА", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    var temp = item as VipLegalEnity;
                    Contribution(capit, item as VipLegalEnity, int.Parse(BoxCountMonth.Text),
                        decimal.Parse(BoxSum.Text));
                    using (var cmd = new SqlCommand($@"UPDATE AllVipLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            Close();
        }

        private void BoxSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BoxCountMonth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (item is SimpleNaturalClient)
                (item as SimpleNaturalClient).Notify += SaveToLogMessage;
            else if (item is SimpleLegalEnity)
                (item as SimpleLegalEnity).Notify += SaveToLogMessage;
            else if (item is VipNaturalClient)
                (item as VipNaturalClient).Notify += SaveToLogMessage;
            else if (item is VipLegalEnity)
                (item as VipLegalEnity).Notify += SaveToLogMessage;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (item is SimpleNaturalClient)
                (item as SimpleNaturalClient).Notify -= SaveToLogMessage;
            else if (item is SimpleLegalEnity)
                (item as SimpleLegalEnity).Notify -= SaveToLogMessage;
            else if (item is VipNaturalClient)
                (item as VipNaturalClient).Notify -= SaveToLogMessage;
            else if (item is VipLegalEnity)
                (item as VipLegalEnity).Notify -= SaveToLogMessage;
        }

        /// <summary>
        /// Обработчкик события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToLogMessage(object sender, AccountEventArgs e)
        {
            string path = $"logs/{DateTime.Now.ToShortDateString()}_log_Contribution.txt";
            DirectoryInfo directoryInfo = new DirectoryInfo("logs");
            if (directoryInfo.Exists == false)
                Directory.CreateDirectory("logs");
            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine(e.Message);
            }
            FileInfo fileInfo = new FileInfo(path);
            MessageBox.Show($"Вклад был записан в log расположенный по пути: \n {fileInfo.FullName}",
                "Complete!", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
