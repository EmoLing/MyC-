using System;
using Clients.Delegats;
using InterfasesLib;

namespace Clients
{
    public class SimpleLegalEnity : LegalEnity, IContribution, ICredit
    {
        public event AccountHandler Notify;

        /// <summary>
        /// Сумма долга 
        /// </summary>
        private decimal Check_Debt = 0;

        /// <summary>
        /// Количество денег на вкладе (поле)
        /// </summary>
        private decimal Check_Contribution = 0;

        /// <summary>
        /// Положительная или отрицательная кредитная история
        /// </summary>
        public string reputation { get; private set; }

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
            get { return $"{Check_Contribution,0:0.##}"; }
            set => Check_Contribution = decimal.Parse(value);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название</param>
        /// <param name="Department">Отдел</param>
        /// <param name="DateOfCreate">Дата создания</param>
        public SimpleLegalEnity(string Name, string Department, DateTime DateOfCreate, decimal AmountOfMoney)
            : base(Name, Department, DateOfCreate, AmountOfMoney)
        {
            this.reputation = GetReputation();
        }

        public SimpleLegalEnity(int ID,string Name, string Department, DateTime DateOfCreate, decimal AmountOfMoney)
            : base(ID,Name, Department, DateOfCreate, AmountOfMoney)
        {
            this.reputation = GetReputation();
        }

        /// <summary>
        /// Получение репутации
        /// </summary>
        /// <returns></returns>
        private string GetReputation()
        {
            Random r = new Random();
            if (r.Next(0, 101) < 25)
                return "Положительная";
            else
                return "Отрицательная";
        }

        /// <summary>
        /// Вклад
        /// </summary>
        /// <param name="Capitalization">С капитализацией или без</param>
        /// <param name="currentDateTime">Текущая дата</param>
        /// <param name="oldDateTime">Дата создания вклада</param>
        /// <param name="sum">Сумма</param>
        /// <returns></returns>
        public void Contribution(bool Capitalization, DateTime currentDateTime, DateTime oldDateTime, decimal sum)
        {
            var a = currentDateTime.Subtract(oldDateTime).Days / (365.25 / 12);
            int month = Convert.ToInt32(a);

            Check_Contribution = sum;
            AmountOfMoney -= sum;

            if (AmountOfMoney < 0)
            {
                AmountOfMoney += sum;
                Check_Contribution = 0;
                return;
            }

            int stavka = 10;
            stavka = (reputation == "Положительная") ? 20 : 10;

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

            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Открытие вклада: Клиент {this.Name} из отдела {this.Department} открыл вклад и положил на него сумму {sum} рублей"));
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

            int stavka = 12;
            stavka = (reputation == "Положительная") ? 15 : 12;

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
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Закрытие вклада: Клиент {this.Name} из отдела {this.Department} закрыл вклад"));
        }

        /// <summary>
        /// Кредит
        /// </summary>
        /// <param name="sum_credit"></param>
        /// <param name="count_month"></param>
        /// <param name="current_moth"></param>
        public void Credit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate)
        {

            var current_moth = currentDate.Subtract(oldDate).Days / (365.25 / 12);
            decimal credit_stavka = (reputation == "Положительная") ? (decimal)0.03 : (decimal)0.1;
            decimal first_sum = (reputation == "Положительная") ? (sum_credit / 100 * 5) : (sum_credit / 100 * 10); //Первоначальный взнос
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
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Получение кредита: Клиент {this.Name} из отдела {this.Department} взял кредит на сумму {sum_credit} рублей на {count_month} месяцев"));
        }

        public string TestCredit(decimal sum_credit, int count_month, DateTime oldDate, DateTime currentDate, out string OstatokPoCredit, out string firstSum)
        {
            var current_moth = currentDate.Subtract(oldDate).Days / (365.25 / 12);
            string NextSummPlatezha = string.Empty;

            decimal credit_stavka = (reputation == "Положительная") ? (decimal)0.03 : (decimal)0.08;
            decimal first_sum = (reputation == "Положительная") ? (sum_credit / 100 * 5) : (sum_credit / 100 * 10); //Первоначальный взнос
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
            Notify?.Invoke(this, new AccountEventArgs($"{DateTime.Now}  Закрытие кредита: Клиент {this.Name} из отдела {this.Department} закрыл кредит"));
        }
    }
}