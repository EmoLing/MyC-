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
using Clients.MyExceptions;
using Clients.VIP;


namespace Bank_System
{
    /// <summary>
    /// Логика взаимодействия для WindowCredit.xaml
    /// </summary>
    public partial class WindowCredit : Window
    {
        private object item;
        public WindowCredit(object item)
        {
            this.item = item;
            InitializeComponent();
        }

        /// <summary>
        /// Расчет кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButSeeTest_Click(object sender, RoutedEventArgs e)
        {
            if (BoxCountMonth != null && BoxSum != null && BoxTestTime.Text != string.Empty)
            {
                if (item is SimpleNaturalClient)
                {
                    try
                    {
                        string LostDebp = string.Empty;
                        string firstSum = string.Empty;
                        BoxEvermonth.Text = (item as SimpleNaturalClient).TestCredit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                            DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)), out LostDebp, out firstSum);
                        BoxDebt.Text = LostDebp;
                        BoxFirstSum.Text = firstSum;
                    }
                    catch (AgeExceptions exceptions)
                    {
                        MessageBox.Show(exceptions.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                if (item is SimpleLegalEnity)
                {
                    string LostDebp = string.Empty;
                    string firstSum = string.Empty;
                    BoxEvermonth.Text = (item as SimpleLegalEnity).TestCredit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                        DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)), out LostDebp, out firstSum);
                    BoxDebt.Text = LostDebp;
                    BoxFirstSum.Text = firstSum;
                }
                if (item is VipNaturalClient)
                {
                    string LostDebp = string.Empty;
                    string firstSum = string.Empty;
                    BoxEvermonth.Text = (item as VipNaturalClient).TestCredit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                        DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)), out LostDebp, out firstSum);
                    BoxDebt.Text = LostDebp;
                    BoxFirstSum.Text = firstSum;
                }
                if (item is VipLegalEnity)
                {
                    string LostDebp = string.Empty;
                    string firstSum = string.Empty;
                    BoxEvermonth.Text = (item as VipLegalEnity).TestCredit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                        DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)), out LostDebp, out firstSum);
                    BoxDebt.Text = LostDebp;
                    BoxFirstSum.Text = firstSum;
                }
            }
            else MessageBox.Show("НЕ ВСЕ ПОЛЯ ЗАПОЛНЕНЫ", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        #region Ограничения на ввод

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

        private void BoxTestTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }


        #endregion

        /// <summary>
        /// Оформление кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButCredit_Click(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();
            if (BoxCountMonth.Text != string.Empty && BoxSum.Text != string.Empty && BoxTestTime.Text != string.Empty)
            {
                try
                {
                    if (item is SimpleNaturalClient)
                    {
                        var temp = item as SimpleNaturalClient;
                        (item as SimpleNaturalClient).Credit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                            DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)));
                        using (var cmd = new SqlCommand($@"UPDATE AllNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                        {
                            dataBase.connection.Open();
                            cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                            cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (item is SimpleLegalEnity)
                    {
                        var temp = item as SimpleLegalEnity;
                        (item as SimpleLegalEnity).Credit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                            DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)));
                        using (var cmd = new SqlCommand($@"UPDATE AllLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                        {
                            dataBase.connection.Open();
                            cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                            cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (item is VipNaturalClient)
                    {
                        var temp = item as VipNaturalClient;
                        (item as VipNaturalClient).Credit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                            DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)));
                        using (var cmd = new SqlCommand($@"UPDATE AllVipNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                        {
                            dataBase.connection.Open();
                            cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                            cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (item is VipLegalEnity)
                    {
                        var temp = item as VipLegalEnity;
                        (item as VipLegalEnity).Credit(decimal.Parse(BoxSum.Text), int.Parse(BoxCountMonth.Text),
                            DateTime.Now, DateTime.Now.AddMonths(int.Parse(BoxTestTime.Text)));
                        using (var cmd = new SqlCommand($@"UPDATE AllVipLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                        {
                            dataBase.connection.Open();
                            cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                            cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (AgeExceptions exception)
                {
                    MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                finally
                {
                    Close();
                }
            }
            else MessageBox.Show("НЕ ВСЕ ПОЛЯ ЗАПОЛНЕНЫ", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void SaveToLogMessage(object sender, AccountEventArgs e)
        {
            string path = $"logs/{DateTime.Now.ToShortDateString()}_log_Credit.txt";
            DirectoryInfo directoryInfo = new DirectoryInfo("logs");
            if (directoryInfo.Exists == false)
                Directory.CreateDirectory("logs");
            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine(e.Message);
            }
            FileInfo fileInfo = new FileInfo(path);
            MessageBox.Show($"Кредит был записан в log расположенный по пути: \n {fileInfo.FullName}",
                "Complete!", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
