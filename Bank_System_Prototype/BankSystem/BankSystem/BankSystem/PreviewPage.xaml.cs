using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Clients;
using Clients.VIP;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для PreviewPage.xaml
    /// </summary>
    public partial class PreviewPage : Window
    {
        private RoutedEventHandler routedClient;
        private RoutedEventHandler routedWorker;

        private RoutedEventHandler routedGoClient;
        private RoutedEventHandler routedGoWorker;
        Bank<NaturalClient, LegalEnity, VipClient> Bank = new Bank<NaturalClient, LegalEnity, VipClient>();
        public PreviewPage()
        {
            InitializeComponent();
            cancelBtn.Click += delegate { Close(); };
        }

        /// <summary>
        /// Выбран рабочий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectWorker(object sender, RoutedEventArgs e)
        {
            comboBox.IsEnabled = false;
            BoxLogin.IsEnabled = false;
            okBtn.Click -= routedGoClient;
            okBtn.Click += routedGoWorker;
        }

        /// <summary>
        /// Выбран клиент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectClient(object sender, RoutedEventArgs e)
        {
            comboBox.IsEnabled = true;
            BoxLogin.IsEnabled = true;
            okBtn.Click -= routedGoWorker;
            okBtn.Click += routedGoClient;

        }

        /// <summary>
        /// Авторизация за рабочего
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoWorker(object sender, RoutedEventArgs e)
        {

            StartPage startPage = new StartPage();
            startPage.Show();
            this.Close();
        }

        /// <summary>
        /// Авторизация за клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoClient(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();
            string dep = string.Empty;

            string sql = string.Empty;
            if (comboBox.Text == "Физ отдел")
                dep = "AllNaturalClients";
            else if (comboBox.Text == "Юр отдел")
                dep = "AllLegalClients";
            else if (comboBox.Text == "VIP отдел")
                dep = "AllVipNaturalClients";
            sql = $@"
SELECT * FROM {dep} WHERE Id = {BoxLogin.Text}
";

            try
            {
                using (var com = new SqlCommand(sql, dataBase.connection))
                {
                    dataBase.connection.Open();
                    SqlDataReader r = com.ExecuteReader();
                    if (!r.HasRows)
                    {
                        dep = "AllVipLegalClients";
                        sql = $@"
SELECT * FROM {dep} WHERE Id = {BoxLogin.Text}
";
                    }
                    dataBase.connection.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                using (var com = new SqlCommand(sql, dataBase.connection))
                {
                    dataBase.connection.Open();
                    SqlDataReader r = com.ExecuteReader();

                    while (r.Read())
                    {
                        if (comboBox.Text == "Физ отдел")
                        {
                            var temp = new SimpleNaturalClient(r.GetInt32(0), r["FirstName"].ToString(),
                                r["LastName"].ToString(), r["Department"].ToString(), r.GetDateTime(3),
                                r.GetDecimal(7));
                            temp.CheckContribution = r.GetDecimal(8).ToString();
                            temp.CheckDebt = r.GetDecimal(9).ToString();
                            ClientWindow clientWindow = new ClientWindow(Bank,temp);
                            clientWindow.Activate();
                            clientWindow.Show();
                        }
                        else if (comboBox.Text == "Юр отдел")
                        {
                            var temp = new SimpleLegalEnity(r.GetInt32(0),
                                r["Name"].ToString(), r["Department"].ToString(), r.GetDateTime(2),
                                r.GetDecimal(6));
                            temp.CheckContribution = r.GetDecimal(7).ToString();
                            temp.CheckDebt = r.GetDecimal(8).ToString();
                            ClientWindow clientWindow = new ClientWindow(Bank, temp);
                            clientWindow.Activate();
                            clientWindow.Show();
                        }
                        else if (comboBox.Text == "VIP отдел")
                        {
                            try
                            {
                                var temp = new VipNaturalClient(r.GetInt32(0), r["FirstName"].ToString(),
                                    r["LastName"].ToString(), r["Department"].ToString(), r.GetDateTime(3),
                                    r.GetDecimal(6));
                                temp.CheckContribution = r.GetDecimal(7).ToString();
                                temp.CheckDebt = r.GetDecimal(8).ToString();
                                ClientWindow clientWindow = new ClientWindow(Bank, temp);
                                clientWindow.Activate();
                                clientWindow.Show();
                            }
                            catch (Exception)
                            {
                                var temp = new VipLegalEnity(r.GetInt32(0),
                                    r["Name"].ToString(), r["Department"].ToString(), r.GetDateTime(2),
                                    r.GetDecimal(5));
                                temp.CheckContribution = r.GetDecimal(6).ToString();
                                temp.CheckDebt = r.GetDecimal(7).ToString();
                                ClientWindow clientWindow = new ClientWindow(Bank, temp);
                                clientWindow.Activate();
                                clientWindow.Show();
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Close();

        }

        /// <summary>
        /// Выгрузка окна и отписка от событий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            okBtn.Click -= routedGoClient;
            okBtn.Click -= routedGoWorker;
            radioWorker.Checked -= routedWorker;
            radioClient.Checked -= routedClient;
        }

        /// <summary>
        /// Загрузка окна и подпись на события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            routedClient = SelectClient;
            routedWorker = SelectWorker;
            routedGoClient = GoClient;
            routedGoWorker = GoWorker;
            butInfo.Click += NewWindow;

            radioWorker.Checked += routedWorker;
            radioClient.Checked += routedClient;
        }


        private void NewWindow(object sender, RoutedEventArgs e)
        {
            WinInfo info = new WinInfo();
            info.Owner = this;
            this.IsEnabled = false;
            if (info.ShowDialog() == true)
            {
                info.Show();
                info.Activate();
            }
            this.IsEnabled = true;
        }
    }
}
