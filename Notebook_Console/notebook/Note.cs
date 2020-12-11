using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace notebook
{
    struct Note
    {
        #region конструктор

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="Number">Номер записи</param>
        /// <param name="date">Дата</param>
        /// <param name="specification">Описание</param>
        /// <param name="place">Место</param>
        /// <param name="remark">Примечание</param>
        public Note(int number, DateTime date, string specification, string place, string remark)
        {
            this.number = number;
            this.date = date;
            this.specification = specification;
            this.place = place;
            this.remark = remark;
        }

        public Note( DateTime date, string specification, string place, string remark) :
            this(0,date,specification,place,remark)
        {
        }

        #endregion

        #region Методы

        /// <summary>
        /// печать
        /// </summary>
        /// <returns></returns>
        public string Print ()
        {
            return $"{this.number,5} |  " +
                   $"{this.date.ToShortDateString(),20} | " +
                   $"{this.specification,20} | " +
                   $"{this.place,20} | {this.remark,20}";
        }
        #endregion

        #region Свойства

        /// <summary>
        /// получение номера записи
        /// </summary>
        public int Number { get { return this.number; } set { this.number = value; } }

        /// <summary>
        /// получение даты
        /// </summary>
        public DateTime Date { get { return this.date; } set { this.date = value;  } }

        /// <summary>
        /// получение описание
        /// </summary>
        public string Specification { get { return this.specification; } set { this.specification = value; } }

        /// <summary>
        /// получение места
        /// </summary>
        public string Place { get { return this.place; } set { this.place = value; } }

        /// <summary>
        /// получение примечания
        /// </summary>
        public string Remark { get { return this.remark; } set { this.remark = value; } }
        #endregion

        #region Поля

        /// <summary>
        /// Номер записи
        /// </summary>
        private int number;

        /// <summary>
        /// Дата
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Описание
        /// </summary>
        private string specification;

        /// <summary>
        /// Место
        /// </summary>
        private string place;

        /// <summary>
        /// Примечание
        /// </summary>
        private string remark;

        #endregion
    }
}
