using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace information_system
{
    public struct Departametns
    {
        #region Конструктор

        /// <summary>
        /// Создание отдела
        /// </summary>
        /// <param name="Name">Название отдела</param>
        /// <param name="Date_Of_Create">Дата создания</param>
        /// <param name="Number_Of_Workers">Количество сотрудников</param>
        public Departametns(int Number, string Name, DateTime Date_Of_Create, int Number_Of_Workers)
        {
            this.number = Number;
            this.name = Name;
            this.date_of_create = Date_Of_Create;
            this.number_of_workers = Number_Of_Workers;
        }
        #endregion

        #region Свойства

        /// <summary>
        /// Номер отдела
        /// </summary>
        public int Number { get { return this.number; } set { this.number = value; } }

        /// <summary>
        /// Создание названия
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// Создание даты образования
        /// </summary>
        public DateTime Date_Of_Create { get { return this.date_of_create; } set { this.date_of_create = value; } }

        /// <summary>
        /// Количество работников
        /// </summary>
        public int Number_Of_Workers { get { return this.number_of_workers; } set { this.number_of_workers = value; } }

        #endregion

        #region Методы 

        public string Print()
        {
            return $"{this.number,7} {this.name,15} {this.date_of_create.ToShortDateString(),12} {this.number_of_workers,5}";
        }

        #endregion

        #region Поля
        private int number;

        /// <summary>
        /// Название
        /// </summary>
        private string name;

        /// <summary>
        /// Дата создания
        /// </summary>
        private DateTime date_of_create;

        /// <summary>
        /// Количество сотрудников
        /// </summary>
        private int number_of_workers;

        #endregion
    }
}
