using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Clients;
using Clients.Delegats;
using Clients.Interfaces;
using Clients.VIP;
using InterfasesLib;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {
        Bank<NaturalClient, LegalEnity, VipClient> Bank = new Bank<NaturalClient, LegalEnity, VipClient>();
        public StartPage()
        {
            InitializeComponent();
            button_Click();
        }

        delegate void Temptemp(int value);

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <param name="mainWindow">Главное окно</param>
        private void Deserializ(MainWindow mainWindow)
        {
            //Физические лица
            Temptemp temptemp = ProgressiveValue;

            try
            {

                SQLDataBase dataBase = new SQLDataBase();
                SqlDataReader reader = dataBase.sql_select("Физический");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int itemID = reader.GetInt32(0); 
                        string itemFirstName = reader["FirstName"].ToString(); 
                        string itemLastName = reader["LastName"].ToString(); 
                        DateTime itemDateOfCreate = reader.GetDateTime(3); 
                        string itemRep = reader["reputation"].ToString(); 
                        string itemDepartment = reader["Department"].ToString(); 
                        decimal itemAmountOfMoney = reader.GetDecimal(7); 

                        var new_client = new SimpleNaturalClient(itemID, itemFirstName, itemLastName, itemDepartment,
                            itemDateOfCreate, itemAmountOfMoney);

                        new_client.CheckContribution =
                            reader.GetDecimal(8).ToString();
                        new_client.CheckDebt = reader.GetDecimal(9).ToString(); 
                        Bank.naturals_clients.Add(new_client);
                        var progress = new Progress<int>(value =>
                            Dispatcher.Invoke(temptemp, DispatcherPriority.Normal, value));
                        Task task1 = new Task(() =>
                        {
                            Dispatcher.Invoke(ProgressiveZero);
                            for (int i = 0; i < 100; i++)
                            {
                                Dispatcher.Invoke(delegate { label.Content = itemFirstName + itemDepartment; });
                                ((IProgress<int>)progress).Report(i);
                             Thread.Sleep(1);
                        }
                        });
                        task1.Start();
                        Task.WaitAll(task1);
                        task1.Dispose();
                    }
                }
                Dispatcher.Invoke(() => dataBase.ServerDisConnect());
                Task task2 = new Task(() =>
                {
                    if (Bank.naturals_clients.Count>0)
                    {
                        foreach (var VARIABLE in Bank.naturals_clients)
                        {
                            Dispatcher.Invoke(() => mainWindow.list_natural_clients.Items.Add(VARIABLE),
                                DispatcherPriority.Normal);
                        }
                    }
                });
                

                //Юр лица
                reader = dataBase.sql_select("Юридический");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int itemID = reader.GetInt32(0); 
                        string itemName = reader["Name"].ToString(); 
                        DateTime itemDateOfCreate = reader.GetDateTime(2); 
                        string itemRep = reader["reputation"].ToString(); 
                        string itemDepartment = reader["Department"].ToString(); 
                        decimal itemAmountOfMoney = reader.GetDecimal(6); 

                        var new_client = new SimpleLegalEnity(itemID, itemName, itemDepartment,
                            itemDateOfCreate, itemAmountOfMoney);

                        new_client.CheckContribution =
                            reader.GetDecimal(7).ToString();
                        new_client.CheckDebt = reader.GetDecimal(8).ToString(); 

                        Bank.legals_clients.Add(new_client);
                        var progress = new Progress<int>(value =>
                            Dispatcher.Invoke(temptemp, DispatcherPriority.Normal, value));
                        Task task1 = new Task(() =>
                        {
                            Dispatcher.Invoke(ProgressiveZero);
                            for (int i = 0; i < 100; i++)
                            {
                                Dispatcher.Invoke(delegate { label.Content = itemName + itemDepartment; });
                                ((IProgress<int>)progress).Report(i);
                                 Thread.Sleep(1);
                            }
                        });
                        task1.Start();
                        Task.WaitAll(task1);
                        task1.Dispose();
                    }
                }
                Dispatcher.Invoke(() => dataBase.ServerDisConnect());
                Task task3 = new Task(() =>
                {
                    if (Bank.legals_clients.Count > 0)
                    {
                        foreach (var VARIABLE in Bank.legals_clients)
                        {
                            Dispatcher.Invoke(() => mainWindow.list_legal_clients.Items.Add(VARIABLE),
                                DispatcherPriority.Normal);
                        }
                    }
                });
                

                //VIP физ
                reader = dataBase.sql_select("VIP_физ");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int itemID = reader.GetInt32(0); 
                        string itemFirstName = reader["FirstName"].ToString(); 
                        string itemLastName = reader["LastName"].ToString(); 
                        DateTime itemDateOfCreate = reader.GetDateTime(3); 
                        string itemDepartment = reader["Department"].ToString(); 
                        decimal itemAmountOfMoney = reader.GetDecimal(6); 

                        var new_client = new VipNaturalClient(itemID, itemFirstName, itemLastName, itemDepartment,
                            itemDateOfCreate, itemAmountOfMoney);

                        new_client.CheckContribution =
                            reader.GetDecimal(7).ToString(); 
                        new_client.CheckDebt = reader.GetDecimal(8).ToString(); 

                        Bank.vip_clients.Add(new_client);
                        var progress = new Progress<int>(value =>
                            Dispatcher.Invoke(temptemp, DispatcherPriority.Normal, value));
                        Task task1 = new Task(() =>
                        {
                            Dispatcher.Invoke(ProgressiveZero);
                            for (int i = 0; i < 100; i++)
                            {
                                Dispatcher.Invoke(delegate { label.Content = itemFirstName + itemDepartment; });
                                ((IProgress<int>)progress).Report(i);
                                 Thread.Sleep(1);
                            }
                        });
                        task1.Start();
                        Task.WaitAll(task1);
                        task1.Dispose();
                    }
                }
                Dispatcher.Invoke(() => dataBase.ServerDisConnect());
                Task task4 = new Task(() =>
                {
                    if (Bank.vip_clients.Count > 0)
                    {
                        foreach (var VARIABLE in Bank.vip_clients)
                        {
                            if (VARIABLE is VipNaturalClient)
                            {
                                Dispatcher.Invoke(() => mainWindow.list_vip_Natural_clients.Items.Add(VARIABLE),
                                    DispatcherPriority.Normal);
                            }
                        }
                    }
                });
                
                //VIP Юр
                reader = dataBase.sql_select("VIP_юр");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int itemID = reader.GetInt32(0); 
                        string itemName = reader["Name"].ToString(); 
                        DateTime itemDateOfCreate = reader.GetDateTime(2); 
                        string itemDepartment = reader["Department"].ToString(); 
                        decimal itemAmountOfMoney = reader.GetDecimal(5); 

                        var new_client = new VipLegalEnity(itemID, itemName, itemDepartment,
                            itemDateOfCreate, itemAmountOfMoney);

                        new_client.CheckContribution =
                            reader.GetDecimal(6).ToString(); 
                        new_client.CheckDebt = reader.GetDecimal(7).ToString(); 

                        Bank.vip_clients.Add(new_client);
                        var progress = new Progress<int>(value =>
                            Dispatcher.Invoke(temptemp, DispatcherPriority.Normal, value));
                        Task task1 = new Task(() =>
                        {
                            Dispatcher.Invoke(ProgressiveZero);
                            for (int i = 0; i < 100; i++)
                            {
                                Dispatcher.Invoke(delegate { label.Content = itemName + itemDepartment; });
                                ((IProgress<int>)progress).Report(i);
                                 Thread.Sleep(1);
                            }
                        });
                        task1.Start();
                        Task.WaitAll(task1);
                        task1.Dispose();
                    }
                }
                Dispatcher.Invoke(() => dataBase.ServerDisConnect());
                Task task5 = new Task(() =>
                {
                    if (Bank.vip_clients.Count > 0)
                    {
                        foreach (var VARIABLE in Bank.vip_clients)
                        {
                            if (VARIABLE is VipLegalEnity)
                            {

                                Dispatcher.Invoke(() => mainWindow.list_vip_legal_clients.Items.Add(VARIABLE),
                                    DispatcherPriority.Normal);
                            }

                        }
                    }
                });
                task2.Start();
                task3.Start();
                task4.Start();
                task5.Start();
                Dispatcher.Invoke(() => mainWindow.Show());
                Dispatcher.Invoke(() => mainWindow.Activate());
                Task.WaitAll(task2,task3,task4,task5);
                task2.Dispose(); task3.Dispose(); task4.Dispose(); task5.Dispose();
                Dispatcher.Invoke(Close);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Dispatcher.Invoke(() => mainWindow.Show());

                Dispatcher.Invoke(Close);
            }
        }

        /// <summary>
        /// Не знаю зачем сделал, скидывает на 0 прогрессив бар
        /// </summary>
        private void ProgressiveZero()
        {
            ProgressBar.Value = 0;
        }

        /// <summary>
        /// Не знаю зачем сделал, получение значения для прогрессив бара
        /// </summary>
        private void ProgressiveValue(int value)
        {
            ProgressBar.Value = value;
        }


        private void button_Click()
        {
            MainWindow mainWindow = new MainWindow(Bank);

            var progress = new Progress<int>(value => ProgressBar.Value = value);
            Task task1 = new Task(() =>
            {
                Dispatcher.Invoke(ProgressiveZero);
                for (int i = 0; i < 100; i++)
                {
                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(100);
                }
            });
            task1.Start();
            Task.Run(() => Deserializ(mainWindow));
            // Deserializ(mainWindow);
        }



    }
}
