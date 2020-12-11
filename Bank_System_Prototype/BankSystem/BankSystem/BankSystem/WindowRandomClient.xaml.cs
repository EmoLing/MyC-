using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WindowRandomClient.xaml
    /// </summary>
    public partial class WindowRandomClient : Window
    {
        private Bank<NaturalClient, LegalEnity, VipClient> Bank;
        public WindowRandomClient(Bank<NaturalClient, LegalEnity, VipClient> Bank)
        {
            this.Bank = Bank;
            InitializeComponent();
        }

        /// <summary>
        /// Запуск процесса генерации сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButGoToMass_Click(object sender, RoutedEventArgs e)
        {
            List<string> Client_FirstNames = new List<string>();
            Client_FirstNames = LoadNames("../../../resources/Names.txt");
            List<string> Client_LastNames = new List<string>();
            Client_LastNames = LoadNames("../../../resources/LastNames.txt");
            int count_workers = 0;

            try
            {
                count_workers = int.Parse(RandomBox.Text); //количество рабочих
            }
            catch (FormatException)
            {
                count_workers = default(int);
                MessageBox.Show("Неверный формат, можно вводить только цифры!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Random r = new Random();
            Random random_spec = new Random(); //специальность
            Random random_name = new Random();
            Random random_dep = new Random();
            SQLDataBase dataBase = new SQLDataBase();
            while (count_workers > 0)
            {
                int count = Bank.Count;
                switch (random_spec.Next(1, 5))
                {
                    case 1:
                        {
                            var temp = new SimpleNaturalClient($"{Client_FirstNames[random_name.Next(Client_FirstNames.Count)]}",
                                                         $"{Client_LastNames[random_name.Next(Client_LastNames.Count)]}",
                                                         "Физический",
                                                         DateTime.Now.AddYears(-r.Next(16, 80)), (decimal)r.Next(20000, 2000000));
                            Bank.Add(temp);
                            dataBase.GoToDataBase(dataBase.CreateSQL(temp) + $@"
(
{temp.ID}, N'{temp.FirstName}', N'{temp.LastName}', '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}', N'{temp.reputation}',
N'{temp.Department}', {temp.AccountNumber},
{temp.AmountOfMoney}, {temp.CheckContribution}, {temp.CheckDebt}
)
");
                            break;
                        }
                    case 2:
                        {
                            var temp = new SimpleLegalEnity($"{Client_FirstNames[random_name.Next(Client_FirstNames.Count)]}",
                                "Юридический",
                                DateTime.Now.AddYears(-r.Next(18, 80)), (decimal)r.Next(20000, 2000000));
                            Bank.Add(temp);
                            dataBase.GoToDataBase(dataBase.CreateSQL(temp) + $@"
(
{temp.ID}, N'{temp.Name}', '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}', N'{temp.reputation}',N'{temp.Department}', {temp.AccountNumber},
{temp.AmountOfMoney}, {temp.CheckContribution}, {temp.CheckDebt}
)
");
                            break;
                        }
                    case 3:
                        {
                            var temp =  new VipNaturalClient($"{Client_FirstNames[random_name.Next(Client_FirstNames.Count)]}",
                                $"{Client_LastNames[random_name.Next(Client_LastNames.Count)]}",
                                "VIP",
                                DateTime.Now.AddYears(-r.Next(18, 80)), (decimal)r.Next(20000, 2000000));

                            Bank.Add(temp);
                            dataBase.GoToDataBase(dataBase.CreateSQL(temp) + $@"
(
{temp.ID}, N'{temp.FirstName}', N'{temp.LastName}', '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}',
N'{temp.Department}', {temp.AccountNumber},
{temp.AmountOfMoney}, {temp.CheckContribution}, {temp.CheckDebt}
)
");
                            break;
                        }
                    case 4:
                        {
                            var temp = new VipLegalEnity($"{Client_FirstNames[random_name.Next(Client_FirstNames.Count)]}",
                                "VIP",
                                DateTime.Now.AddYears(-r.Next(16, 80)), (decimal)r.Next(20000, 2000000));

                            Bank.Add(temp);
                            dataBase.GoToDataBase(dataBase.CreateSQL(temp) + $@"
(
{temp.ID}, N'{temp.Name}', '{Convert.ToDateTime(temp.DateofBirth).ToString("yyyy-MM-dd")}', N'{temp.Department}', {temp.AccountNumber},
{temp.AmountOfMoney}, {temp.CheckContribution}, {temp.CheckDebt}
)
");
                            break;
                        }
                    default:
                        break;
                }
                count_workers--;
            }
            MessageBox.Show("Готово", "Complite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Close();
        }
        

        /// <summary>
        /// Дессериализация дефолтных имен/фамилий
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<string> LoadNames(string path)
        {
            List<string> Workers_Names = new List<string>();
            string name = string.Empty;
            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    name = streamReader.ReadToEnd();
                }
                var names = name.Split(',', ' ');
                for (int i = 0; i < names.Length; i++)
                {
                    Workers_Names.Add(names[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return Workers_Names;

        }
    }
}
