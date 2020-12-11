using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using Clients;
using Clients.VIP;
using System.Xaml;
using System.Xml;
namespace BankSystem
{
    public class SQLDataBase
    {
        private SqlConnectionStringBuilder strCon;
        public SqlConnection connection;
        SqlDataAdapter da;
        DataTable dt;
        DataRowView row;

        public SQLDataBase()
        {
            string[] db = JSON();
            strCon = new SqlConnectionStringBuilder()
            {
                DataSource = $@"{db[0].ToString()}", //@"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = $@"{db[1].ToString()}",//@"ClientsDB",
                IntegratedSecurity = bool.Parse(db[2].ToString()),
                Pooling = bool.Parse(db[3].ToString())
            };
            connection = new SqlConnection(strCon.ConnectionString);
        }
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClientsDB;Integrated Security=True;Pooling=False
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClientsDB;Integrated Security=True;Pooling=False
        //Data Source=(localDB)\MSSQLLocalDB;Initial Catalog=ClientsDB;Integrated Security=True;Pooling=False
        /// <summary>
        /// Соединение с бд
        /// </summary>
        private void ServerConnect()
        {
            connection.Open();
        }

        /// <summary>
        /// Разрыв соединения с бд
        /// </summary>
        public void ServerDisConnect()
        {
            connection.Close();
        }

        /// <summary>
        /// Запрос на добавление данных
        /// </summary>
        /// <param name="sql_insert"></param>
        public void sql_insert(string[] sql_insert)
        {
            ServerConnect();
            foreach (var item in sql_insert)
            {
                SqlCommand command = new SqlCommand(item, connection);
                command.ExecuteNonQuery();
            }
            ServerDisConnect();
        }

        /// <summary>
        /// Запрос на Апгрейд данных
        /// </summary>
        /// <param name="sql_upgrade"></param>
        public void sql_upgade(string[] sql_upgrade)
        {
            ServerConnect();
            foreach (var item in sql_upgrade)
            {
                SqlCommand command = new SqlCommand(item, connection);
                command.ExecuteNonQuery();
            }
            ServerDisConnect();
        }
        public void sql_upgade(string sql_upgrade)
        {
            try
            {
                ServerConnect();
                SqlCommand command = new SqlCommand(sql_upgrade, connection);
                command.ExecuteNonQuery();
                ServerDisConnect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Запрос удаления
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void sql_delete<T>(T item)
        {
            try
            {

                ServerConnect();
                string sql = string.Empty;
                if (item is SimpleNaturalClient)
                {
                    var temp = item as SimpleNaturalClient;
                    sql = $@"
DELETE FROM AllNaturalClients WHERE Id = {temp.ID}
";
                }
                else if (item is SimpleLegalEnity)
                {
                    var temp = item as SimpleLegalEnity;
                    sql = $@"
DELETE FROM AllLegalClients WHERE Id = {temp.ID}
";
                }
                else if (item is VipNaturalClient)
                {
                    var temp = item as VipNaturalClient;
                    sql = $@"
DELETE FROM AllVipNaturalClients WHERE Id = {temp.ID}
";
                }
                else if (item is VipLegalEnity)
                {
                    var temp = item as VipLegalEnity;
                    sql = $@"
DELETE FROM AllVipLegalClients WHERE Id = {temp.ID}
";
                }
                ServerDisConnect();
                MessageBox.Show("Удаление завершено!", "ГОТОВО", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Запрос на выборку
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public SqlDataReader sql_select(string department)
        {
            string sql = string.Empty;
            ServerConnect();
            if (department == "Физический")
            {
                sql = @"
SELECT * FROM AllNaturalClients
";
            }
            else if (department == "Юридический")
            {
                sql = @"
SELECT * FROM AllLegalClients
";
            }
            else if (department == "VIP_физ")
            {
                sql = @"
SELECT * FROM AllVipNaturalClients
";
            }
            else if (department == "VIP_юр")
            {
                sql = @"
SELECT * FROM AllVipLegalClients
";
            }
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader r = command.ExecuteReader();
            return r;
        }

        /// <summary>
        /// Создание SQL запроса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="temp">Клиент</param>
        /// <returns></returns>
        public string CreateSQL<T>(T temp)
        {
            string sql = string.Empty;
            string DataBase = string.Empty;
            string Name = string.Empty;
            string reputation = "";
            if (temp is SimpleNaturalClient || temp is VipNaturalClient)
            {
                Name = "[FirstName], [LastName]";
                if (temp is SimpleNaturalClient)
                {
                    reputation = "[reputation],";
                    DataBase = "AllNaturalClients";
                }
                else
                    DataBase = "AllVipNaturalClients";
            }
            else if (temp is SimpleLegalEnity || temp is VipLegalEnity)
            {
                Name = "[Name]";
                if (temp is SimpleLegalEnity)
                {
                    reputation = "[reputation],";
                    DataBase = "AllLegalClients";
                }
                else
                    DataBase = "AllVipLegalClients";
            }

            sql = $@"
INSERT INTO {DataBase} ([Id], {Name}, [DateOfBirth], 
{reputation} [Department], 
[AccountNumber], [AmountOfMoney],
[CheckContribution], [CheckDebt]) VALUES ";
            return sql;
        }

        /// <summary>
        /// Отправка запроса в базу данных
        /// </summary>
        /// <param name="sql"></param>
        public void GoToDataBase(string sql)
        {
            try
            {
                SQLDataBase dataBase = new SQLDataBase();
                dataBase.sql_upgade(sql);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string[] JSON()
        {
            string [] temp_string = new  string[4];
            string path = "../../../Connect/connect.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Name == "DataSource")
                {
                    temp_string[0] = $@"{xnode.InnerText}";
                }
                else if (xnode.Name == "InitialCatalog")
                    temp_string[1] = xnode.InnerText;
                else if (xnode.Name == "IntegratedSecurity")
                    temp_string[2] = xnode.InnerText;
                else if (xnode.Name == "Pooling")
                    temp_string[3] = xnode.InnerText;
            }

            return temp_string;
        }
    }
}
