using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Bank_System;
using Clients;
using Clients.VIP;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;

namespace BankSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bank<NaturalClient, LegalEnity, VipClient> Bank = new Bank<NaturalClient, LegalEnity, VipClient>();
        //  private DateTime currentDateTime = DateTime.Today;
        public MainWindow(Bank<NaturalClient, LegalEnity, VipClient> Bank)
        {
            this.Bank = Bank;
            InitializeComponent();
            Update();
        }

        /// <summary>
        /// Обновление listView
        /// </summary>
        private void Update()
        {
            list_natural_clients.Items.Clear();
            for (int i = 0; i < Bank.naturals_clients.Count; i++)
            {
                list_natural_clients.Items.Add(Bank.naturals_clients[i]);
            }

            list_legal_clients.Items.Clear();
            for (int i = 0; i < Bank.legals_clients.Count; i++)
            {
                list_legal_clients.Items.Add(Bank.legals_clients[i]);
            }

            list_vip_Natural_clients.Items.Clear();
            list_vip_legal_clients.Items.Clear();

            foreach (var item in Bank.vip_clients)
            {
                if (item is VipNaturalClient)
                    list_vip_Natural_clients.Items.Add(item);
                else
                    list_vip_legal_clients.Items.Add(item);
            }
        }

        /// <summary>
        /// Кнопка Добавить 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {

            if (ComboBoxDep.Text == "Физ. Лицо")
            {
                AddWindowNaturalClient addWindow = new AddWindowNaturalClient(Bank);
                addWindow.Owner = this;
                this.IsEnabled = false;
                if (addWindow.ShowDialog() == true)
                {
                    addWindow.Show();
                    addWindow.Activate();
                }
                this.IsEnabled = true;
            }
            else if (ComboBoxDep.Text == "Юрид. Лицо")
            {
                AddWindowLegalEnity addWindow = new AddWindowLegalEnity(Bank);
                addWindow.Owner = this;
                this.IsEnabled = false;
                if (addWindow.ShowDialog() == true)
                {
                    addWindow.Show();
                    addWindow.Activate();
                }
                this.IsEnabled = true;
            }
            else if (ComboBoxDep.Text == "VIP Лицо")
            {
                AddWindowVipClient addWindow = new AddWindowVipClient(Bank);
                addWindow.Owner = this;
                this.IsEnabled = false;
                if (addWindow.ShowDialog() == true)
                {
                    addWindow.Show();
                    addWindow.Activate();
                }
                this.IsEnabled = true;
            }
            Update();
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButDelete_Click(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();

            if (TabItemNatural.IsSelected)
            {
                if (list_natural_clients.SelectedItem != null)
                {
                    var temp = list_natural_clients.SelectedItem as SimpleNaturalClient;
                    Bank.Remove(list_natural_clients.SelectedItem);
                    dataBase.sql_delete(temp);
                }
            }
            else if (TabItemLegal.IsSelected)
            {
                if (list_legal_clients.SelectedItem != null)
                {
                    var temp = list_legal_clients.SelectedItem as SimpleLegalEnity;
                    Bank.Remove(list_legal_clients.SelectedItem);
                    dataBase.sql_delete(temp);
                }
            }
            else if (TabItemVip.IsSelected)
            {
                if (list_vip_legal_clients.SelectedItem != null)
                {
                    var temp = list_vip_legal_clients.SelectedItem as VipLegalEnity;
                    Bank.Remove(list_vip_legal_clients.SelectedItem);
                    dataBase.sql_delete(temp);
                }

                else if (list_vip_Natural_clients.SelectedItem != null)
                {
                    var temp = list_vip_Natural_clients.SelectedItem as VipNaturalClient;
                    Bank.Remove(list_vip_Natural_clients.SelectedItem);
                    dataBase.sql_delete(temp);
                }
            }
            Update();
        }

        #region Выбор в ComboBox

        /// <summary>
        /// Физическое лицо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemNatural_Selected(object sender, RoutedEventArgs e)
        {
            TabItemNatural.IsSelected = true;
        }

        /// <summary>
        /// Юридическое лицо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemLegal_Selected(object sender, RoutedEventArgs e)
        {
            TabItemLegal.IsSelected = true;
        }

        /// <summary>
        /// VIP клиенты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemVip_Selected(object sender, RoutedEventArgs e)
        {
            TabItemVip.IsSelected = true;
        }

        #endregion

        /// <summary>
        /// Кнопка Вклад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxContribution_Click(object sender, RoutedEventArgs e)
        {
            if (TabItemNatural.IsSelected)
            {
                if (list_natural_clients.SelectedItem != null)
                {
                    if (!Bank.CheckTransact(list_natural_clients.SelectedItem))
                        Contribution_TemplateWindow(list_natural_clients.SelectedItem);
                    else MessageBox.Show("Вклад уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (TabItemLegal.IsSelected)
            {
                if (list_legal_clients.SelectedItem != null)
                {
                    if (!Bank.CheckTransact(list_legal_clients.SelectedItem))
                        Contribution_TemplateWindow(list_legal_clients.SelectedItem);
                    else MessageBox.Show("Вклад уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (TabItemVip.IsSelected)
            {
                if (list_vip_legal_clients.SelectedItem != null)
                {
                    if (!Bank.CheckTransact(list_vip_legal_clients.SelectedItem))
                        Contribution_TemplateWindow(list_vip_legal_clients.SelectedItem);
                    else MessageBox.Show("Вклад уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                else if (list_vip_Natural_clients.SelectedItem != null)
                {
                    if (!Bank.CheckTransact(list_vip_Natural_clients.SelectedItem))
                        Contribution_TemplateWindow(list_vip_Natural_clients.SelectedItem);
                    else MessageBox.Show("Вклад уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            Update();
        }

        /// <summary>
        /// Кнопка закрытия клада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseContibution_Click(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();
            if (TabItemNatural.IsSelected)
            {
                if (list_natural_clients.SelectedItem != null)
                {
                    var temp = list_natural_clients.SelectedItem as SimpleNaturalClient;
                    (list_natural_clients.SelectedItem as SimpleNaturalClient).Notify += SaveToLogContrubutionMessage;
                    (list_natural_clients.SelectedItem as SimpleNaturalClient).CloseContribution();
                    (list_natural_clients.SelectedItem as SimpleNaturalClient).Notify -= SaveToLogContrubutionMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else if (TabItemLegal.IsSelected)
            {
                if (list_legal_clients.SelectedItem != null)
                {
                    var temp = list_legal_clients.SelectedItem as SimpleLegalEnity;
                    (list_legal_clients.SelectedItem as SimpleLegalEnity).Notify += SaveToLogContrubutionMessage;
                    (list_legal_clients.SelectedItem as SimpleLegalEnity).CloseContribution();
                    (list_legal_clients.SelectedItem as SimpleLegalEnity).Notify -= SaveToLogContrubutionMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else if (TabItemVip.IsSelected)
            {
                if (list_vip_legal_clients.SelectedItem != null)
                {
                    var temp = list_vip_legal_clients.SelectedItem as VipLegalEnity;
                    (list_vip_legal_clients.SelectedItem as VipLegalEnity).Notify += SaveToLogContrubutionMessage;
                    (list_vip_legal_clients.SelectedItem as VipLegalEnity).CloseContribution();
                    (list_vip_legal_clients.SelectedItem as VipLegalEnity).Notify -= SaveToLogContrubutionMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllVipLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }

                else if (list_vip_Natural_clients.SelectedItem != null)
                {
                    var temp = list_vip_Natural_clients.SelectedItem as VipNaturalClient;
                    (list_vip_Natural_clients.SelectedItem as VipNaturalClient).Notify += SaveToLogContrubutionMessage;
                    (list_vip_Natural_clients.SelectedItem as VipNaturalClient).CloseContribution();
                    (list_vip_Natural_clients.SelectedItem as VipNaturalClient).Notify -= SaveToLogContrubutionMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllVipNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckContribution] = @CheckContribution WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckContribution", SqlDbType.Decimal).Value = temp.CheckContribution;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            Update();
        }

        /// <summary>
        /// ШАБЛОН Создание экземпляра окна Вклада
        /// </summary>
        /// <typeparam name="T">Natural, Legal, VIP</typeparam>
        /// <param name="item"></param>
        private void Contribution_TemplateWindow<T>(T item)
        {
            WindownContribution windownContribution = new WindownContribution(item);

            windownContribution.Owner = this;
            this.IsEnabled = false;
            if (windownContribution.ShowDialog() == true)
            {
                windownContribution.Show();
                windownContribution.Activate();
            }
            this.IsEnabled = true;
        }

        /// <summary>
        /// Транзакция
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButTransact_Click(object sender, RoutedEventArgs e)
        {
            if (TabItemNatural.IsSelected)
            {
                if (list_natural_clients.SelectedItem != null)
                {
                    Transact_TemplateWindow(list_natural_clients.SelectedItem);
                }
                else MessageBox.Show("Выберите отправителя!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (TabItemLegal.IsSelected)
            {
                if (list_legal_clients.SelectedItem != null)
                {
                    Transact_TemplateWindow(list_legal_clients.SelectedItem);
                }
                else MessageBox.Show("Выберите отправителя!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (TabItemVip.IsSelected)
            {
                if (list_vip_legal_clients.SelectedItem != null)
                {
                    Transact_TemplateWindow(list_vip_legal_clients.SelectedItem);
                }

                else if (list_vip_Natural_clients.SelectedItem != null)
                {
                    Transact_TemplateWindow(list_vip_Natural_clients.SelectedItem);
                }
                else MessageBox.Show("Выберите отправителя!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Update();
        }

        /// <summary>
        /// ШАБЛОН Создание экземпляра окна Транхакция
        /// </summary>
        /// <typeparam name="T">Natural, Legal, VIP</typeparam>
        /// <param name="item"></param>
        private void Transact_TemplateWindow<T>(T item)
        {
            WindowTransact windowTransact = new WindowTransact(Bank, item);
            windowTransact.Owner = this;
            this.IsEnabled = false;
            if (windowTransact.ShowDialog() == true)
            {
                windowTransact.Show();
                windowTransact.Activate();
            }
            this.IsEnabled = true;
        }

        /// <summary>
        /// Редактировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TabItemNatural.IsSelected)
            {
                if (list_natural_clients.SelectedItem != null)
                {
                    EditWindow_Template(list_natural_clients.SelectedItem);
                }
                else MessageBox.Show("Выберите клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (TabItemLegal.IsSelected)
            {
                if (list_legal_clients.SelectedItem != null)
                {
                    EditWindow_Template(list_legal_clients.SelectedItem);
                }
                else MessageBox.Show("Выберите клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (TabItemVip.IsSelected)
            {
                if (list_vip_legal_clients.SelectedItem != null)
                {
                    EditWindow_Template(list_vip_legal_clients.SelectedItem);
                }

                else if (list_vip_Natural_clients.SelectedItem != null)
                {
                    EditWindow_Template(list_vip_Natural_clients.SelectedItem);
                }
                else MessageBox.Show("Выберите клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Update();
        }

        /// <summary>
        /// ШАБЛОН Создание экземпляра окна Транхакция
        /// </summary>
        /// <typeparam name="T">Natural, Legal, VIP</typeparam>
        /// <param name="item"></param>
        private void EditWindow_Template<T>(T item)
        {
            EditWindow editWindow = new EditWindow(Bank, item);
            editWindow.Owner = this;
            this.IsEnabled = false;
            if (editWindow.ShowDialog() == true)
            {
                editWindow.Show();
                editWindow.Activate();
            }
            this.IsEnabled = true;
        }

        /// <summary>
        /// Изменения Combobox, названий подменю при изменении TabControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuAddClient != null && e.AddedItems.Count != 0 && (e.AddedItems[0] as TabItem) != null)
            {
                if ((e.AddedItems[0] as TabItem).Header.ToString() == "Физические лица")
                {
                    MenuAddClient.Header = "Добавить физ. лицо";
                    MenuEditClient.Header = "Редактировать физ. лицо";
                    MenuDeleteClient.Header = "Удалить физ. лицо";
                    ComboBoxDep.SelectedIndex = 0;
                }
                else if ((e.AddedItems[0] as TabItem).Header.ToString() == "Юридические лица")
                {
                    MenuAddClient.Header = "Добавить юр. лицо";
                    MenuEditClient.Header = "Редактировать юр. лицо";
                    MenuDeleteClient.Header = "Удалить юр. лицо";
                    ComboBoxDep.SelectedIndex = 1;
                }
                else if ((e.AddedItems[0] as TabItem).Header.ToString() == "VIP клиенты")
                {
                    MenuAddClient.Header = "Добавить VIP";
                    MenuEditClient.Header = "Редактировать VIP";
                    MenuDeleteClient.Header = "Удалить VIP";
                    ComboBoxDep.SelectedIndex = 2;
                }
            }

        }

        /// <summary>
        /// Оформить кредит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButCredit_Click(object sender, RoutedEventArgs e)
        {
            if (TabItemNatural.IsSelected)
            {
                if (list_natural_clients.SelectedItem != null)
                {
                    if (!Bank.CheckCredit(list_natural_clients.SelectedItem))
                        Credit_TemplateWindow(list_natural_clients.SelectedItem);
                    else MessageBox.Show("Кредит уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else MessageBox.Show("Выберите клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (TabItemLegal.IsSelected)
            {
                if (list_legal_clients.SelectedItem != null)
                {
                    if (!Bank.CheckCredit(list_legal_clients.SelectedItem))
                        Credit_TemplateWindow(list_legal_clients.SelectedItem);
                    else MessageBox.Show("Кредит уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else MessageBox.Show("Выберите клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (TabItemVip.IsSelected)
            {
                if (list_vip_legal_clients.SelectedItem != null)
                {
                    if (!Bank.CheckCredit(list_vip_legal_clients.SelectedItem))
                        Credit_TemplateWindow(list_vip_legal_clients.SelectedItem);
                    else MessageBox.Show("Кредит уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                else if (list_vip_Natural_clients.SelectedItem != null)
                {
                    if (!Bank.CheckCredit(list_vip_Natural_clients.SelectedItem))
                        Credit_TemplateWindow(list_vip_Natural_clients.SelectedItem);
                    else MessageBox.Show("Кредит уже есть", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else MessageBox.Show("Выбере клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Update();
        }

        /// <summary>
        /// ШАБЛОН Создание экземпляра окна Вклада
        /// </summary>
        /// <typeparam name="T">Natural, Legal, VIP</typeparam>
        /// <param name="item"></param>
        private void Credit_TemplateWindow<T>(T item)
        {
            WindowCredit windowCredit = new WindowCredit(item);

            windowCredit.Owner = this;
            this.IsEnabled = false;
            if (windowCredit.ShowDialog() == true)
            {
                windowCredit.Show();
                windowCredit.Activate();
            }
            this.IsEnabled = true;
        }

        /// <summary>
        /// Закрыть кредит
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SQLDataBase dataBase = new SQLDataBase();
            if (TabItemNatural.IsSelected)
            {
                if (list_natural_clients.SelectedItem != null)
                {
                    var temp = list_natural_clients.SelectedItem as SimpleNaturalClient;
                    (list_natural_clients.SelectedItem as SimpleNaturalClient).Notify += SaveToLogCreditMessage;
                    (list_natural_clients.SelectedItem as SimpleNaturalClient).CloseCredit();
                    (list_natural_clients.SelectedItem as SimpleNaturalClient).Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}",dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }
                else MessageBox.Show("Выберите клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (TabItemLegal.IsSelected)
            {
                if (list_legal_clients.SelectedItem != null)
                {
                    var temp = list_legal_clients.SelectedItem as SimpleLegalEnity;
                    (list_legal_clients.SelectedItem as SimpleLegalEnity).Notify += SaveToLogCreditMessage;
                    (list_legal_clients.SelectedItem as SimpleLegalEnity).CloseCredit();
                    (list_legal_clients.SelectedItem as SimpleLegalEnity).Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }
                else MessageBox.Show("Выберите клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (TabItemVip.IsSelected)
            {
                if (list_vip_legal_clients.SelectedItem != null)
                {
                    var temp = list_vip_legal_clients.SelectedItem as VipLegalEnity;
                    (list_vip_legal_clients.SelectedItem as VipClient).Notify += SaveToLogCreditMessage;
                    (list_vip_legal_clients.SelectedItem as VipClient).CloseCredit();
                    (list_vip_legal_clients.SelectedItem as VipClient).Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllVipLegalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }

                else if (list_vip_Natural_clients.SelectedItem != null)
                {
                    var temp = list_vip_Natural_clients.SelectedItem as VipNaturalClient;
                    (list_vip_Natural_clients.SelectedItem as VipClient).Notify += SaveToLogCreditMessage;
                    (list_vip_Natural_clients.SelectedItem as VipClient).CloseCredit();
                    (list_vip_Natural_clients.SelectedItem as VipClient).Notify -= SaveToLogCreditMessage;
                    using (var cmd = new SqlCommand($@"UPDATE AllVipNaturalClients SET  [AmountOfMoney] = @AmountOfMoney, [CheckDebt] = @CheckDebt WHERE Id = {temp.ID}", dataBase.connection))
                    {
                        dataBase.connection.Open();
                        cmd.Parameters.Add("@AmountOfMoney", SqlDbType.Decimal).Value = temp.AmountOfMoney;
                        cmd.Parameters.Add("@CheckDebt", SqlDbType.Decimal).Value = temp.CheckDebt;
                        cmd.ExecuteNonQuery();
                    }
                }
                else MessageBox.Show("Выбере клиента!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
           // dataBase.sql_upgade(sql);
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

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButSerializ_Click(object sender, RoutedEventArgs e)
        {
            string[] sql = new string[Bank.naturals_clients.Count];
            int number = 0;
            foreach (var item in Bank.naturals_clients)
            {
                sql[number] = $@"
UPDATE AllNaturalClients SET [FirstName] = N'{item.FirstName}', [LastName] = N'{item.LastName}', 
[DateOfBirth] = '{Convert.ToDateTime(item.DateofBirth).ToString("yyyy-MM-dd")}', 
[reputation] = N'{(item as SimpleNaturalClient).reputation}',[Department] = N'{item.Department}', 
[AccountNumber] = {item.AccountNumber}, [AmountOfMoney] = {item.AmountOfMoney},
[CheckContribution] = {(item as SimpleNaturalClient).CheckContribution}, [CheckDebt] = {(item as SimpleNaturalClient).CheckDebt}
WHERE Id = {item.ID}
" + "\n";
                number++;
            }
            try
            {
                SQLDataBase dataBase = new SQLDataBase();
                dataBase.sql_upgade(sql);
                MessageBox.Show("Готово", "Complite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Юр лица
            sql = new string[Bank.legals_clients.Count];
            number = 0;
            foreach (var item in Bank.legals_clients)
            {
                sql[number] = $@"
UPDATE AllLegalClients SET [Name] = N'{item.Name}', 
[DateOfBirth] = '{Convert.ToDateTime(item.DateofBirth).ToString("yyyy-MM-dd")}', 
[reputation] = N'{(item as SimpleLegalEnity).reputation}',[Department] = N'{item.Department}', 
[AccountNumber] = {item.AccountNumber}, [AmountOfMoney] = {item.AmountOfMoney},
[CheckContribution] = {(item as SimpleLegalEnity).CheckContribution}, [CheckDebt] = {(item as SimpleLegalEnity).CheckDebt}
WHERE Id = {item.ID}
" + "\n";
                number++;
            }
            try
            {
                SQLDataBase dataBase = new SQLDataBase();
                dataBase.sql_upgade(sql);
                MessageBox.Show("Готово", "Complite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //VIP
            sql = new string[Bank.vip_clients.Count];
            number = 0;
            foreach (var item in Bank.vip_clients)
            {
                if (item is VipNaturalClient)
                {
                    sql[number] = $@"
UPDATE AllVipNaturalClients SET [FirstName] = N'{(item as VipNaturalClient).FirstName}', [LastName] = N'{(item as VipNaturalClient).LastName}', 
[DateOfBirth] = '{Convert.ToDateTime(item.DateofBirth).ToString("yyyy-MM-dd")}', 
[Department] = N'{item.Department}', 
[AccountNumber] = {item.AccountNumber}, [AmountOfMoney] = {item.AmountOfMoney},
[CheckContribution] = {item.CheckContribution}, [CheckDebt] = {item.CheckDebt}
WHERE Id = {item.ID}
" + "\n";
                    number++;
                }
                else
                {
                    sql[number] = $@"
UPDATE AllVipLegalClients SET [Name] = N'{(item as VipLegalEnity).Name}', 
[DateOfBirth] = '{Convert.ToDateTime(item.DateofBirth).ToString("yyyy-MM-dd")}', 
[Department] = N'{item.Department}', 
[AccountNumber] = {item.AccountNumber}, [AmountOfMoney] = {item.AmountOfMoney},
[CheckContribution] = {item.CheckContribution}, [CheckDebt] = {item.CheckDebt}
WHERE Id = {item.ID}
" + "\n";
                    number++;
                }

            }
            try
            {
                SQLDataBase dataBase = new SQLDataBase();
                dataBase.sql_upgade(sql);
                MessageBox.Show("Готово", "Complite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Сохранить как
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButSerializHOW_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "json files (*.json)|*.json";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                string json = JsonConvert.SerializeObject(Bank.naturals_clients);
                File.WriteAllText($"{saveFileDialog1.FileName}_Naturals", json);
                json = JsonConvert.SerializeObject(Bank.legals_clients);
                File.WriteAllText($"{saveFileDialog1.FileName}_Legal", json);
                json = JsonConvert.SerializeObject(Bank.vip_clients);
                File.WriteAllText($"{saveFileDialog1.FileName}_Vip", json);
                MessageBox.Show("Готово", "Complite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void Deserializ()
        {
            string s = File.ReadAllText("__repositury.json");
            var json = JObject.Parse(s)["naturals_clients"].ToArray();

            foreach (var item in json)
            {
                int itemID = int.Parse(item["ID"].ToString());
                string itemFirstName = item["FirstName"].ToString();
                string itemLastName = item["LastName"].ToString();
                DateTime itemDateOfCreate = DateTime.Parse(item["DateofBirth"].ToString());
                string itemRep = item["reputation"].ToString();
                string itemDepartment = item["Department"].ToString();
                decimal itemAmountOfMoney = decimal.Parse(item["AmountOfMoney"].ToString());
                Bank.naturals_clients.Add(new SimpleNaturalClient(itemFirstName, itemLastName, itemDepartment, itemDateOfCreate, itemAmountOfMoney));
            }

            json = JObject.Parse(s)["legal_clients"].ToArray();

            foreach (var item in json)
            {
                int itemID = int.Parse(item["ID"].ToString());
                string itemName = item["Name"].ToString();
                DateTime itemDateOfCreate = DateTime.Parse(item["DateofBirth"].ToString());
                string itemRep = item["reputation"].ToString();
                string itemDepartment = item["Department"].ToString();
                decimal itemAmountOfMoney = decimal.Parse(item["AmountOfMoney"].ToString());
                Bank.legals_clients.Add(new SimpleLegalEnity(itemName, itemDepartment, itemDateOfCreate, itemAmountOfMoney));
            }
        }
        /*
      "reputation": "Положительная",
      "CheckContribution": "0",
      "CheckDebt": "0",
      "ID": 1,
      "FirstName": "Artem",
      "LastName": "Selivanov",
      "Department": "Физический",
      "Age": 23,
      "AccountNumber": 1001,
      "DateofBirth": "31.03.1997",
      "AmountOfMoney": 10000.0
         */

        /// <summary>
        /// Генерация клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButRandomWorkers(object sender, RoutedEventArgs e)
        {
            WindowRandomClient windowRandom = new WindowRandomClient(Bank);

            int temp_count = Bank.Count;

            windowRandom.Owner = this;
            this.IsEnabled = false;
            if (windowRandom.ShowDialog() == true)
            {
                windowRandom.Show();
                windowRandom.Activate();
            }
            this.IsEnabled = true;
            Update();
        }

        /// <summary>
        /// Информационное окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButFAQ_Click(object sender, RoutedEventArgs e)
        {
            WinFAQ winFAQ = new WinFAQ();
            winFAQ.Show();
        }

    }
}
