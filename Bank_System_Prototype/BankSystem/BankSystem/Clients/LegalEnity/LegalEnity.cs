using System;
using  InterfasesLib;
namespace Clients
{
    public abstract class LegalEnity : IAccount
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
        static LegalEnity()
        {
            staticAccountNumber = 0;
            staticID = 0;
        }

        #endregion

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отдел
        /// </summary>
        public string Department { get; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public string DateofBirth { get; }
        /// <summary>
        /// Номер счета
        /// </summary>
        public int AccountNumber { get; }

        /// <summary>
        /// Сумма денег
        /// </summary>
        public decimal AmountOfMoney { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название</param>
        /// <param name="department">Отдел</param>
        /// <param name="Birthday">Дата создания отдела</param>
        protected LegalEnity(string name, string department, DateTime DateOfCreate, decimal AmountOfMoney)
        {
            this.ID = NextID();
            this.Name = name;
            this.Department = department;
            this.Age = GetAge(DateOfCreate);
            this.AccountNumber = NextAccountNumber(this.ID);
            this.AmountOfMoney = AmountOfMoney;
            this.DateofBirth = DateOfCreate.ToShortDateString();
        }
        protected LegalEnity(int ID, string name, string department, DateTime DateOfCreate, decimal AmountOfMoney)
        {
            this.ID = ID;
            this.Name = name;
            this.Department = department;
            this.Age = GetAge(DateOfCreate);
            this.AccountNumber = NextAccountNumber(this.ID);
            this.AmountOfMoney = AmountOfMoney;
            this.DateofBirth = DateOfCreate.ToShortDateString();
            staticID++;
        }

        /// <summary>
        /// Получение возраста отдела
        /// </summary>
        /// <param name="DateOfBirth">Создания</param>
        /// <returns></returns>
        private int GetAge(DateTime DateOfCreate)
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year - DateOfCreate.Year;
            if (dateTime.Month < DateOfCreate.Month || (dateTime.Month == DateOfCreate.Month
                                                        && dateTime.Day < DateOfCreate.Day))
                year--;
            return year;
        }

    }
}