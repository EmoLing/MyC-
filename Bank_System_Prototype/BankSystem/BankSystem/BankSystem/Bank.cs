using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using Clients;
using Clients.Delegats;
using Clients.Interfaces;
using Clients.VIP;
using InterfasesLib;

namespace BankSystem
{
    public class Bank<T1, T2, T3> : IAdd, ITransact, IRemove, IFind, IEdit<T1, T2, T3>
        where T1 : NaturalClient
        where T2 : LegalEnity
        where T3 : VipClient
    {
        public event AccountHandler Notify;

        public List<T1> naturals_clients; //Коллекция физических лиц
        public List<T2> legals_clients; // Коллекция юридических лиц
        public List<T3> vip_clients; //Коллекция VIP лиц

        private int count = 0;

        public int Count
        {
            get { return count; }
        }
        public Bank()
        {
            naturals_clients = new List<T1>();
            legals_clients = new List<T2>();
            vip_clients = new List<T3>();
            count = naturals_clients.Count + legals_clients.Count + vip_clients.Count;
        }

        /// <summary>
        /// Добавление в клиента
        /// </summary>
        /// <typeparam name="T">Natural, Vip, Legal</typeparam>
        /// <param name="item"></param>
        public void Add<T>(T item)
        {
            if (item is T1)
            {
                var temp_item = item as T1;
                temp_item.AddToList(naturals_clients);
              //  naturals_clients.Add(temp_item);
            }
            else if (item is T2)
            {
                var temp_item = item as T2;
                temp_item.AddToList(legals_clients);
               // legals_clients.Add(temp_item);
            }
            else if (item is T3)
            {
                var temp_item = item as T3;
                temp_item.AddToList(vip_clients);
               // vip_clients.Add(temp_item);
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Remove<T>(T item)
        {
            if (item is T1)
                (item as T1).RemoveFromList(naturals_clients);
                //naturals_clients.Remove(item as T1);
            else if (item is T2)
                (item as T2).RemoveFromList(legals_clients);
                //legals_clients.Remove(item as T2);
            else if (item is T3)
                (item as T3).RemoveFromList(vip_clients);
                //vip_clients.Remove(item as T3);
        }

        /// <summary>
        /// Транзакция
        /// </summary>
        /// <typeparam name="TClient1">Отправитель</typeparam>
        /// <typeparam name="TClient2">Получатель</typeparam>
        /// <param name="client1">Отправитель</param>
        /// <param name="client2">Получатель</param>
        /// <param name="sum">сумма</param>
        public void Transact<TClient1>(TClient1 client1, TClient1 client2, decimal sum)
            where TClient1 : IAccount

        {
            if (client1 == null || client2 == null)
                return;
            if (client1.Equals(client2))
            {
                return;
            }

            if (!LockTransact(client1, client2, sum)) return;
            GiveMoney(client1, client2, sum);
            GetMoney(client2, client1, sum);
        }

        /// <summary>
        /// Блокировка транзакции
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client1"></param>
        /// <param name="client2"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        private bool LockTransact<T>(T client1, T client2, decimal sum)
            where T : IAccount
        {

            if ((client1.Department != client2.Department) && (sum >= 50000))
            {
                Notify?.Invoke(this, new AccountEventArgs(
                    $"{DateTime.Now} Заблокированная транзакция Транзакция: Клиент с номером счета {client1.AccountNumber} из {client1.Department} отдела  перевел " +
                    $"Клиенту с номером счета {client2.AccountNumber} из {client2.Department} отдела", sum));
                MessageBox.Show("Перевод в другой отдел возможен только при суммах меньше 50000", "WARNING",
                    MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            else return true;
        }
        /// <summary>
        /// Отправка денег
        /// </summary>
        private void GiveMoney<T>(T client1, T client2, decimal sum)
            where T : IAccount
        {
            client1.AmountOfMoney = client1 - sum;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Транзакция: Клиент с номером счета {client1.AccountNumber} из {client1.Department} отдела перевел" +
                                                      $" клиенту с номером счета {client2.AccountNumber} из {client2.Department} отдела", sum));
        }

        /// <summary>
        /// Получение денег
        /// </summary>
        private void GetMoney<T>(T client2, T client1, decimal sum)
            where T : IAccount
        {
            client2.AmountOfMoney = client2 + sum;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Транзакция: Клиент с номером счета {client2.AccountNumber} из {client2.Department} отдела получил " +
                                                      $"от клиента с номером счета {client1.AccountNumber} из отдела {client2.Department}", sum));
        }

        /// <summary>
        /// Проверка на уже созданный вклад
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool CheckTransact<T>(T item)
        {
            if (item is T1)
            {
                if (double.Parse((item as SimpleNaturalClient).CheckContribution) != 0)
                    return true;
                else return false;
            }
            if (item is T2)
            {
                if (double.Parse((item as SimpleLegalEnity).CheckContribution) != 0)
                    return true;
                else return false;
            }
            if (item is T3)
            {
                if (double.Parse((item as VipClient).CheckContribution) != 0)
                    return true;
                else return false;
            }
            else return true;
        }

        /// <summary>
        /// Проверка на уже созданный вклад
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool CheckCredit<T>(T item)
        {
            if (item is T1)
            {
                if (double.Parse((item as SimpleNaturalClient).CheckDebt) != 0)
                    return true;
                else return false;
            }
            if (item is T2)
            {
                if (double.Parse((item as SimpleLegalEnity).CheckDebt) != 0)
                    return true;
                else return false;
            }
            if (item is T3)
            {
                if (double.Parse((item as VipClient).CheckDebt) != 0)
                    return true;
                else return false;
            }
            else return true;
        }

        /// <summary>
        /// Поиск по Имени и фамилии
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Department"></param>
        /// <returns></returns>
        public object Find<T>(string FirstName, string LastName, string Department)
        {
            if (Department == "Физический")
            {
                foreach (var item in naturals_clients)
                {
                    if (FirstName == item.FirstName && LastName == item.LastName)
                        return item;

                }
                return null;
            }
            else if (Department == "VIP")
            {
                foreach (var item in vip_clients)
                {
                    if (FirstName == (item as VipNaturalClient).FirstName && LastName == (item as VipNaturalClient).LastName)
                        return item;
                }
                return null;
            }
            else return null;
        }

        /// <summary>
        /// Поиск по Названию
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Name"></param>
        /// <param name="Department"></param>
        /// <returns></returns>
        public object Find<T>(string Name, string Department)
        {
            if (Department == "Юридический")
            {
                foreach (var item in legals_clients)
                {
                    if (Name == item.Name)
                        return item;

                }
                return null;
            }
            else if (Department == "VIP")
            {
                foreach (var item in vip_clients)
                {
                    if (Name == (item as VipLegalEnity).Name)
                        return item;
                }
                return null;
            }
            else return null;
        }

        /// <summary>
        /// Поиск по Номеру счета
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="AccountNumber"></param>
        /// <param name="Department"></param>
        /// <returns></returns>
        public object Find<T>(int AccountNumber, string Department)
        {
            SQLDataBase dataBase = new SQLDataBase();

            string dep = string.Empty;

            string sql = string.Empty;
            if (Department == "Физический")
                dep = "AllNaturalClients";
            else if (Department == "Юридический")
                dep = "AllLegalClients";
            else if (Department == "VIP")
                dep = "AllVipNaturalClients";
            sql = $@"
SELECT * FROM {dep} WHERE AccountNumber = {AccountNumber}
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
SELECT * FROM {dep} WHERE AccountNumber = {AccountNumber}
";
                    }
                    dataBase.connection.Close();
                }
            }
            catch (Exception)
            {
            }

            try
            {
                using (var com = new SqlCommand(sql, dataBase.connection))
                {
                    dataBase.connection.Open();
                    SqlDataReader r = com.ExecuteReader();

                    while (r.Read())
                    {
                        if (Department == "Физический")
                        {
                            var temp = new SimpleNaturalClient(r.GetInt32(0), r["FirstName"].ToString(),
                                r["LastName"].ToString(), r["Department"].ToString(), r.GetDateTime(3),
                                r.GetDecimal(7));
                            temp.CheckContribution = r.GetDecimal(8).ToString();
                            temp.CheckDebt = r.GetDecimal(9).ToString();
                            return temp;
                        }
                        else if (Department == "Юридический")
                        {
                            var temp = new SimpleLegalEnity(r.GetInt32(0),
                                r["Name"].ToString(), r["Department"].ToString(), r.GetDateTime(2),
                                r.GetDecimal(6));
                            temp.CheckContribution = r.GetDecimal(7).ToString();
                            temp.CheckDebt = r.GetDecimal(8).ToString();
                            return temp;
                        }
                        else if (Department == "VIP")
                        {
                            try
                            {
                                var temp = new VipNaturalClient(r.GetInt32(0), r["FirstName"].ToString(),
                                    r["LastName"].ToString(), r["Department"].ToString(), r.GetDateTime(3),
                                    r.GetDecimal(6));
                                temp.CheckContribution = r.GetDecimal(7).ToString();
                                temp.CheckDebt = r.GetDecimal(8).ToString();
                                return temp;
                            }
                            catch (Exception)
                            {
                                var temp = new VipLegalEnity(r.GetInt32(0),
                                    r["Name"].ToString(), r["Department"].ToString(), r.GetDateTime(2),
                                    r.GetDecimal(5));
                                temp.CheckContribution = r.GetDecimal(6).ToString();
                                temp.CheckDebt = r.GetDecimal(7).ToString();
                                return temp;
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return "NOT FOUND";
            }
            return "NOT FOUND";
        }

        /// <summary>
        /// Редактирование физического лица
        /// </summary>
        /// <param name="item">NaturalClient</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        public void Edit(T1 item, string FirstName, string LastName, int Age)
        {
            item.FirstName = FirstName;
            item.LastName = LastName;
            item.Age = Age;
        }

        /// <summary>
        /// Редактирование юридического лица
        /// </summary>
        /// <param name="item">LegalEnity</param>
        /// <param name="Name">Название</param>
        /// <param name="Age">Возраст</param>
        public void Edit(T2 item, string Name, int Age)
        {
            item.Name = Name;
            item.Age = Age;
        }

        /// <summary>
        /// Редактирование вип лица(физическое лицо)
        /// </summary>
        /// <param name="item">VipClient</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        public void Edit(T3 item, string FirstName, string LastName, int Age)
        {
            (item as VipNaturalClient).FirstName = FirstName;
            (item as VipNaturalClient).LastName = LastName;
            (item as VipNaturalClient).Age = Age;
        }

        /// <summary>
        /// Редактирование вип лица(юридическое лицо)
        /// </summary>
        /// <param name="item">VipClient</param>
        /// <param name="Name">Название</param>
        /// <param name="Age">Возраст</param>
        public void Edit(T3 item, string Name, int Age)
        {
            (item as VipLegalEnity).Name = Name;
            (item as VipLegalEnity).Age = Age;
        }

    }
}