using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using InterfasesLib;

namespace Clients
{
    public abstract class NaturalClient : IAccount
    {
        #region StaticID&StaticAccountNumber

        /// <summary>
        /// статический ID
        /// </summary>
        private static int staticID;

        /// <summary>
        /// Статический номер счета
        /// </summary>
        private static int staticAccountNumber;

        /// <summary>
        /// Подсчет нового номера счета
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        static int NextAccountNumber(int ID)
        {
            staticAccountNumber = ID + 1000;
            return staticAccountNumber;
        }

        /// <summary>
        /// Добавление к новому ID
        /// </summary>
        /// <returns></returns>
        static int NextID()
        {
            staticID++;
            return staticID;
        }

        /// <summary>
        /// Статический конструктор
        /// </summary>
        static NaturalClient()
        {
            staticAccountNumber = 0;
            staticID = 0;
        }

        #endregion

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отдел
        /// </summary>
        public string Department { get; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Номер счета
        /// </summary>
        public int AccountNumber { get; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public string DateofBirth { get; }
        /// <summary>
        /// Сумма денег
        /// </summary>
        public decimal AmountOfMoney { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="department">Отдел</param>
        /// <param name="Birthday">День рождения</param>
        protected NaturalClient(string firstName, string lastName, string department, DateTime Birthday, decimal amountOfMoney)
        {
            this.ID = NextID();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Department = department;
            this.Age = GetAge(Birthday);
            this.AccountNumber = NextAccountNumber(this.ID);
            this.AmountOfMoney = amountOfMoney;
            this.DateofBirth = Birthday.ToShortDateString();
        }

        protected NaturalClient(int ID, string firstName, string lastName, string department, DateTime Birthday,
            decimal amountOfMoney)
        {
            this.ID = ID;;
            staticID++;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Department = department;
            this.Age = GetAge(Birthday);
            this.AccountNumber = NextAccountNumber(this.ID);
            this.AmountOfMoney = amountOfMoney;
            this.DateofBirth = Birthday.ToShortDateString();
        }
        /// <summary>
        /// Получение возраста
        /// </summary>
        /// <param name="DateOfBirth">Дата рождения</param>
        /// <returns></returns>
        private int GetAge(DateTime DateOfBirth)
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year - DateOfBirth.Year;
            if (dateTime.Month < DateOfBirth.Month || (dateTime.Month == DateOfBirth.Month
                                                       && dateTime.Day < DateOfBirth.Day))
                year--;
            return year;
        }
    }
}