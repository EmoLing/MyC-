using System;

namespace Waybil_work
{
    public class Waybil_Class
    {
        /// <summary>
        /// Статическое поле
        /// </summary>
        private static int staticID;

        /// <summary>
        /// статический конструктор
        /// </summary>
        static Waybil_Class()
        {
            staticID = 0;
        }

        /// <summary>
        /// Статический метод
        /// </summary>
        /// <returns></returns>
        private static int NextID()
        {
            staticID++;
            return staticID;
        }

        /// <summary>
        /// ID, не будет напечатано
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Дата 
        /// </summary>
        public string dateTime { get; set; }

        /// <summary>
        /// Место прибытия
        /// </summary>
        public string Mesto_Pribitiya { get; set; }

        /// <summary>
        /// Время прибытия
        /// </summary>
        public  string Otmetka_O_Pribitii { get; set; }

        /// <summary>
        /// Время отбытия
        /// </summary>
        public string Otmetka_Ob_Ubutii { get; set; }

        /// <summary>
        /// Подтверждающий документ
        /// </summary>
        public string Podtvergdaushiy_Doc { get; set; }

        /// <summary>
        /// Затраты
        /// </summary>
        public string Zatrati { get; set; }

        public double Lost_Sum { get; set; }
        public Waybil_Class(DateTime dateTime, string Mesto_Pribitiya, string Otmetka_O_Pribitii, string Otmetka_Ob_Ubutii, string Podtvergdaushiy_Doc, int Kilometri)
        {
            this.ID = NextID();
            this.dateTime = dateTime.ToString("dd'/'MM'/'yy");
            this.Mesto_Pribitiya = Mesto_Pribitiya;
            this.Otmetka_O_Pribitii = Otmetka_O_Pribitii;
            this.Otmetka_Ob_Ubutii = Otmetka_Ob_Ubutii;
            this.Podtvergdaushiy_Doc = Podtvergdaushiy_Doc;
            this.Zatrati = Zatrati_string(Kilometri);
            this.Lost_Sum = Kilometri * 0.093 * 51;
        }
    
        private string Zatrati_string(int Kilometrs)
        {
            double rashod = 0.093; //na 1 km
            int oil = 51; //стоимость бензина
            int itog = Convert.ToInt32(rashod * oil * Kilometrs);
            return $"{Kilometrs}км ({itog} руб.)";
        }
    }
}