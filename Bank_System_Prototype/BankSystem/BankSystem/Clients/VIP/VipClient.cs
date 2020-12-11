using System;
using Clients.Delegats;
using Clients.MyExceptions;
using InterfasesLib;

namespace Clients.VIP
{
    public abstract class VipClient : IContribution, IAccount
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
        static VipClient()
        {
            staticAccountNumber = 0;
            staticID = 0;
        }

        #endregion


        public event AccountHandler Notify;

        /// <summary>
        /// Сумма долга 
        /// </summary>
        private decimal Check_Debt = 0;

        /// <summary>
        /// Количество денег на вкладе
        /// </summary>
        private decimal Check_Contribution = 0;

        /// <summary>
        /// Сумма долга, свойство
        /// </summary>
        public string CheckDebt
        {
            get { return $"{Check_Debt,0:0.##}"; }
            set => Check_Debt = decimal.Parse(value);
        }

        /// <summary>
        /// Количество денег на вкладе (свойство)
        /// </summary>
        public string CheckContribution
        {
            get
            {
                return $"{Check_Contribution,0:0.##}";
            }
            set => Check_Contribution = decimal.Parse(value);
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; }


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
        /// <param name="department">Отдел</param>
        /// <param name="DateOfCreate">Дата создания/рождения</param>
        /// <param name="amountOfMoney">Сумма на счету</param>
        protected VipClient(string department, DateTime DateOfCreate, decimal amountOfMoney)
        {
            this.ID = NextID();
            this.Department = department;
            this.Age = GetAge(DateOfCreate);
            this.AccountNumber = NextAccountNumber(ID);
            this.AmountOfMoney = amountOfMoney;
            this.DateofBirth = DateOfCreate.ToShortDateString();
        }

        protected VipClient(int ID,string department, DateTime DateOfCreate, decimal amountOfMoney)
        {
            this.ID = ID;
            this.Department = department;
            this.Age = GetAge(DateOfCreate);
            this.AccountNumber = NextAccountNumber(ID);
            this.AmountOfMoney = amountOfMoney;
            this.DateofBirth = DateOfCreate.ToShortDateString();
            staticID++;
        }

        /// <summary>
        /// Получить возраст
        /// </summary>
        /// <param name="DateOfCreate">Дата создания/рождения</param>
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

        /// <summary>
        /// Вклад
        /// </summary>
        /// <param name="Capitalization">С капитализацией или без</param>
        /// <param name="currentDateTime">Текущая дата</param>
        /// <param name="oldDateTime">Дата создания вклада</param>
        /// <param name="sum">Сумма</param>
        public void Contribution(bool Capitalization, DateTime currentDateTime, DateTime oldDateTime, decimal sum)
        {
            var a = currentDateTime.Subtract(oldDateTime).Days / (365.25 / 12);
            int month = Convert.ToInt32(a);
            AmountOfMoney -= sum;
            int stavka = 24;
            Check_Contribution = sum;

            if (AmountOfMoney < 0)
            {
                AmountOfMoney += sum;
                Check_Contribution = 0;
                return;
            }
            if (Capitalization)
            {
                decimal percent = stavka / 12;
                decimal percent_stavka = percent;
                for (int i = 0; i < month; i++)
                {
                    Check_Contribution += percent_stavka;
                    percent_stavka = (Check_Contribution * ((decimal)stavka / 100)) / 12;
                }
            }
            else
            {
                if (month == 12)
                    Check_Contribution += stavka;
            }
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Открытие вклада: Клиент с номером счета {this.AccountNumber} из отдела {this.Department} открыл вклад и положил на него сумму {sum} рублей"));
        }

        /// <summary>
        /// Вклад, котоырй будет показан, как предварительный
        /// </summary>
        /// <param name="Capitalization">С капитализацией или без</param>
        /// <param name="currentDateTime">Текущая дата</param>
        /// <param name="oldDateTime">Дата создания вклада</param>
        /// <param name="sum">Сумма</param>
        /// <returns></returns>
        public string Test_Contribution(bool Capitalization, DateTime currentDateTime, DateTime oldDateTime, decimal sum)
        {
            var a = currentDateTime.Subtract(oldDateTime).Days / (365.25 / 12);
            int month = Convert.ToInt32(a);

            int stavka = 24;
            decimal testCheck_Contribution = sum;
            if (Capitalization)
            {
                decimal percent = stavka / 12;
                decimal percent_stavka = percent;
                for (int i = 0; i < month; i++)
                {
                    testCheck_Contribution += percent_stavka;
                    percent_stavka = (testCheck_Contribution * ((decimal)stavka / 100)) / 12;
                }
            }
            else
            {
                if (month == 12)
                    testCheck_Contribution += stavka;
            }
            return $"{testCheck_Contribution,0:0.##}";
        }

        /// <summary>
        /// Закрытие вклада
        /// </summary>
        public void CloseContribution()
        {
            AmountOfMoney += Check_Contribution;
            Check_Contribution = 0;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Закрытие вклада: Клиент с номером счета {this.AccountNumber} из отдела {this.Department} закрыл вклад"));
        }

        /// <summary>
        /// Кредит
        /// </summary>
        /// <param name="sum_credit"></param>
        /// <param name="count_month"></param>
        /// <param name="current_moth"></param>
        public void Credit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate)
        {
            if (this.Age < 18)
                throw new AgeExceptions("Нельзя выдавать кредит несовершеннолетним");
            var current_moth = currentDate.Subtract(oldDate).Days / (365.25 / 12);
            decimal credit_stavka = (decimal)0.01;
            decimal first_sum = 0; //Первоначальный взнос
            sum_credit -= first_sum; //Сумма кредита
            AmountOfMoney += sum_credit;

            decimal every_month_debt = sum_credit / count_month; //ежемесячный долг

            decimal Nachis_Percents = sum_credit * credit_stavka / 12; //начисленные проценты

            decimal Ostatok_Po_Credit = sum_credit - every_month_debt; //Остаток по кредиту

            Check_Debt = Ostatok_Po_Credit;
            for (int i = 2; i <= current_moth; i++)
            {
                Nachis_Percents = Ostatok_Po_Credit * credit_stavka / 12; //на второй месяц

                decimal Sum_Platezha = every_month_debt + Nachis_Percents; //Сумма платежа

                AmountOfMoney -= Sum_Platezha;

                Ostatok_Po_Credit -= every_month_debt;

                Check_Debt = Ostatok_Po_Credit;
            }
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Получение кредита: Клиент с номером счет {this.AccountNumber} из отдела {this.Department} взял кредит на сумму {sum_credit} рублей на {count_month} месяцев"));
        }

        public string TestCredit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate, out string OstatokPoCredit, out string firstSum)
        {
            var current_moth = currentDate.Subtract(oldDate).Days / (365.25 / 12);
            string NextSummPlatezha = string.Empty;

            decimal credit_stavka = (decimal)0.01;
            decimal first_sum = 0; //Первоначальный взнос
            firstSum = $"{first_sum,0:0.##}";

            sum_credit -= first_sum; //Сумма кредита

            decimal sum_ostatkov = 0;

            decimal every_month_debt = sum_credit / count_month; //ежемесячный долг

            decimal Nachis_Percents = sum_credit * credit_stavka / 12; //начисленные проценты

            decimal Ostatok_Po_Credit = sum_credit - every_month_debt; //Остаток по кредиту

            sum_ostatkov = Ostatok_Po_Credit;

            OstatokPoCredit = $"{Ostatok_Po_Credit,0:0.##}";
            for (int i = 2; i <= current_moth; i++)
            {
                Nachis_Percents = Ostatok_Po_Credit * credit_stavka / 12; //на второй месяц

                decimal Sum_Platezha = every_month_debt + Nachis_Percents; //Сумма платежа
                NextSummPlatezha = $"{Sum_Platezha,0:0.##}";

                Ostatok_Po_Credit -= every_month_debt;

                sum_ostatkov += Ostatok_Po_Credit;
                OstatokPoCredit = $"{sum_ostatkov,0:0.##}";
            }
            return NextSummPlatezha;
        }

        public void CloseCredit()
        {
            AmountOfMoney -= Check_Debt;
            Check_Debt = 0;
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Закрытие кредита: Клиент с номером счет {this.AccountNumber} из отдела {this.Department} закрыл кредит"));
        }
    }
}