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
using Bank_System;
using Clients;
using Clients.VIP;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private Bank<NaturalClient, LegalEnity, VipClient> Bank;
        private object client;
        private RoutedEventHandler Transact;
        private RoutedEventHandler Contribution;
        private RoutedEventHandler Credit;
        private RoutedEventHandler Close_Credit;
        private RoutedEventHandler Close_Contribution;
        public ClientWindow(Bank<NaturalClient, LegalEnity, VipClient> Bank, object client)
        {
            this.Bank = Bank;
            InitializeComponent();
            butExit.Click += delegate { Close(); };
            this.client = client;

            WhatIsClient(client);
        }

        /// <summary>
        /// Идентификация клиента
        /// </summary>
        /// <param name="client"></param>
        private void WhatIsClient(object client)
        {
            if (client is SimpleNaturalClient)
            {
                var temp_client = client as SimpleNaturalClient;
                textHELLO.Text = $"{HelloTime()}, {temp_client.FirstName}!";
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";
                //RecCred.ToolTip =
                //    "Недостаточно привилегий для использования этой функции, обратитесь в банк. Эта функция доступна для VIP клиентов";
                //RecContr.ToolTip =
                //    "Недостаточно привилегий для использования этой функции, обратитесь в банк. Эта функция доступна для VIP клиентов";
            }
            else if (client is SimpleLegalEnity)
            {
                var temp_client = client as SimpleLegalEnity;
                textHELLO.Text = $"{HelloTime()}, {temp_client.Name}!";
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";
                //RecCred.ToolTip =
                //    "Недостаточно привилегий для использования этой функции, обратитесь в банк. Эта функция доступна для VIP клиентов";
                //RecContr.ToolTip =
                //    "Недостаточно привилегий для использования этой функции, обратитесь в банк. Эта функция доступна для VIP клиентов";
            }
            else if (client is VipNaturalClient)
            {
                var temp_client = client as VipNaturalClient;
                textHELLO.Text = $"{HelloTime()}, {temp_client.FirstName}!";
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";
                butOpenContr.IsEnabled = true;
                butOpenCredir.IsEnabled = true;
            }
            else if (client is VipLegalEnity)
            {
                var temp_client = client as VipLegalEnity;
                textHELLO.Text = $"{HelloTime()}, {temp_client.Name}!";
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";
                butOpenContr.IsEnabled = true;
                butOpenCredir.IsEnabled = true;
            }
        }

        /// <summary>
        /// Приветствие по времени
        /// </summary>
        /// <returns></returns>
        private string HelloTime()
        {
            string hello = "Hi";
            if (DateTime.Now.Hour >= 4 && DateTime.Now.Hour < 12)
                hello = "Доброе утро";
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 17)
                hello = "Добрый день";
            else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 23)
                hello = "Добрый вечер";
            else if (DateTime.Now.Hour >= 23 && DateTime.Now.Hour < 4)
                hello = "Доброй ночи";
            return hello;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Transact = OpenTransact;
            Contribution = OpenContribution;
            Credit = OpenCredit;
            Close_Credit = CloseCredit;
            Close_Contribution = CloseContribution;


            butTransact.Click += Transact;
            butOpenContr.Click += Contribution;
            butOpenCredir.Click += Credit;
            butCloseContr.Click += Close_Contribution;
            butCloseCredit.Click += Close_Credit;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            butTransact.Click -= Transact;
            butOpenContr.Click -= Contribution;
            butOpenCredir.Click -= Credit;
            butCloseContr.Click -= Close_Contribution;
            butCloseCredit.Click -= Close_Credit;
        }

        /// <summary>
        /// Открытие окна транзакции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTransact(object sender, RoutedEventArgs e)
        {
            WindowTransact transact = new WindowTransact(Bank, client);
            transact.Owner = this;
            this.IsEnabled = false;
            if (transact.ShowDialog() == true)
            {
                transact.Show();
                transact.Activate();
            }
            this.IsEnabled = true;
            Update();
        }


        /// <summary>
        /// Открытие окна Вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenContribution(object sender, RoutedEventArgs e)
        {
            WindownContribution contribution = new WindownContribution(client);
            contribution.Owner = this;
            this.IsEnabled = false;
            if (contribution.ShowDialog() == true)
            {
                contribution.Show();
                contribution.Activate();
            }
            this.IsEnabled = true;
            Update();
        }

        /// <summary>
        /// Открытие окна кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCredit(object sender, RoutedEventArgs e)
        {
            WindowCredit credit = new WindowCredit(client);
            credit.Owner = this;
            this.IsEnabled = false;
            if (credit.ShowDialog() == true)
            {
                credit.Show();
                credit.Activate();
            }
            this.IsEnabled = true;
            Update();
        }

        /// <summary>
        /// Закрытие кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseCredit(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();

                if (client is SimpleNaturalClient)
                {
                    var temp = client as SimpleNaturalClient;
                    temp.Notify += SaveToLogCreditMessage;
                    temp.CloseCredit();
                    temp.Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }


                else if (client is SimpleLegalEnity)
                {
                    var temp = client as SimpleLegalEnity;
                    temp.Notify += SaveToLogCreditMessage;
                    temp.CloseCredit();
                    temp.Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }


                else if (client is VipLegalEnity)
                {
                    var temp = client as VipLegalEnity;
                    temp.Notify += SaveToLogCreditMessage;
                    temp.CloseCredit();
                    temp.Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllVipLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }

                else if (client is VipNaturalClient != null)
                {
                    var temp = client as VipNaturalClient;
                    temp.Notify += SaveToLogCreditMessage;
                    temp.CloseCredit();
                    temp.Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllVipNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }
                Update();
        }

        /// <summary>
        /// Закрытие вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseContribution(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();

            if (client is SimpleNaturalClient)
            {
                var temp = client as SimpleNaturalClient;
                temp.Notify += SaveToLogContrubutionMessage;
                temp.CloseContribution();
                temp.Notify -= SaveToLogContrubutionMessage;
                using (var cmd = new SqlCommand($@"UPDATE AllNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                {
                    dataBase.connection.Open();
                    cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                    cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                    cmd.ExecuteNonQuery();
                }
            }

            else if (client is SimpleLegalEnity)
            {
                var temp = client as SimpleLegalEnity;
                temp.Notify += SaveToLogContrubutionMessage;
                temp.CloseContribution();
                temp.Notify -= SaveToLogContrubutionMessage;
                using (var cmd =
                    new SqlCommand(
                        $@"UPDATE AllLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}",
                        dataBase.connection))
                {
                    dataBase.connection.Open();
                    cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                    cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                    cmd.ExecuteNonQuery();
                }
            }

            else if (client is VipLegalEnity)
            {
                var temp = client as VipLegalEnity;
                temp.Notify += SaveToLogContrubutionMessage;
                temp.CloseContribution();
                temp.Notify -= SaveToLogContrubutionMessage;
                using (var cmd =
                    new SqlCommand(
                        $@"UPDATE AllVipLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}",
                        dataBase.connection))
                {
                    dataBase.connection.Open();
                    cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                    cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                    cmd.ExecuteNonQuery();
                }
            }

            else if (client is VipNaturalClient)
            {
                var temp = client as VipNaturalClient;
                temp.Notify += SaveToLogContrubutionMessage;
                temp.CloseContribution();
                temp.Notify -= SaveToLogContrubutionMessage;
                using (var cmd =
                    new SqlCommand(
                        $@"UPDATE AllVipNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}",
                        dataBase.connection))
                {
                    dataBase.connection.Open();
                    cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                    cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                    cmd.ExecuteNonQuery();
                }
            }
            Update();
        }

        /// <summary>
        /// Сохранение операций по вкладу в ЛОГ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToLogContrubutionMessage(object sender, AccountEventArgs e)
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
            MessageBox.Show($"Запись была записана в log расположенный по пути: \n {fileInfo.FullName}",
                "Complete!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Сохранение операций по кредиту в ЛОГ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToLogCreditMessage(object sender, AccountEventArgs e)
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
            MessageBox.Show($"Запись была записана в log расположенный по пути: \n {fileInfo.FullName}",
                "Complete!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Update()
        {
            if (client is SimpleNaturalClient)
            {
                var temp_client = client as SimpleNaturalClient;
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";
            }
            else if (client is SimpleLegalEnity)
            {
                var temp_client = client as SimpleLegalEnity;
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";

            }
            else if (client is VipNaturalClient)
            {
                var temp_client = client as VipNaturalClient;
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";
            }
            else if (client is VipLegalEnity)
            {
                var temp_client = client as VipLegalEnity;
                textGold.Text = $"Ваше золото: {temp_client.AmountOfMoney}";
                textDolg.Text = $"Ваш долг: {temp_client.CheckDebt}";
                textContr.Text = $"Ваше накопление: {temp_client.CheckContribution}";
            }
        }
    }
}
