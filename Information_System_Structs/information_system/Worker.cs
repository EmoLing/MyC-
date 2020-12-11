using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace information_system
{
    public struct Worker
    {
        #region Конструктор

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="Number">Номер</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Department">Отдел</param>
        /// <param name="Salary">Оплата труда</param>
        /// <param name="NumberOfProject">Количество проектов</param>
        public Worker(int Number, string FirstName, string LastName, uint Age, string Department, int Salary, int NumberOfProject)
        {
            this.number = Number;
            this.firstName = FirstName;
            this.lastname = LastName;
            this.age = Age;
            this.departametns = Department;
            this.salary = Salary;
            this.numberOfProject = NumberOfProject;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Печать
        /// </summary>
        /// <returns></returns>
        public string Print ()
        {
            return $"{this.number,7} {this.firstName,15} {this.lastname,15} {this.age,3} {this.departametns,15} {this.salary,7} {this.numberOfProject,3}";
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Номер сотрудника
        /// </summary>
        public int Number { get { return this.number; } set { this.number = value; } }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get { return this.lastname; } set { this.lastname = value; } }

        /// <summary>
        /// Возраст
        /// </summary>
        public uint Age { get { return this.age; } set { this.age = value; } }

        /// <summary>
        /// Отдел
        /// </summary>
        public string Department { get { return this.departametns; } set { this.departametns = value; } }

        /// <summary>
        /// Оплата труда
        /// </summary>
        public int Salary { get { return this.salary; } set { this.salary = value; } }

        /// <summary>
        /// Количество проектов
        /// </summary>
        public int NumberOfProject { get { return this.numberOfProject; } set { this.numberOfProject = value; } }
        #endregion

        #region Поля

        /// <summary>
        /// Номер
        /// </summary>
        private int number;

        /// <summary>
        /// Имя
        /// </summary>
        private string firstName;

        /// <summary>
        /// Фамилия
        /// </summary>
        private string lastname;

        /// <summary>
        /// Возраст
        /// </summary>
        private  uint age;

        /// <summary>
        /// Отдел
        /// </summary>
        string departametns;
        /// <summary>
        /// Оплата труда
        /// </summary>
        private int salary;

        /// <summary>
        /// Количество проектов
        /// </summary>
        private int numberOfProject;

        #endregion
    }
}
