using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notebook
{
    struct Menu
    {
        Repository rep;
        
        /// <summary>
        /// Добавление
        /// </summary>
        public void Add ()
        {
            Console.WriteLine("1 - ввод вручную | 2 - добавление из файла ");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.WriteLine("Введите дату");

                        DateTime date = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Введите описание");
                        string specification = Console.ReadLine();

                        Console.WriteLine("Введите место");
                        string place = Console.ReadLine();

                        Console.WriteLine("Введите примечание");

                        string remark = Console.ReadLine();
                        rep.Add(new Note(rep.IndexOfNumber(), date, specification, place, remark));

                        Console.WriteLine("Нажмите любую кнопку");
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        string Path_add = @"notebook_add.csv";
                        rep.Load(Path_add);

                        Console.WriteLine("Нажмите любую кнопку");
                        Console.ReadKey();
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        public  void Delete ()
        {

            Console.WriteLine("Выберите нужную строку: ");
            rep.Delete(Convert.ToInt32(Console.ReadLine()));

            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        public void Editing ()
        {

            Console.WriteLine("Выберите нужную строку: ");
            rep.Editing(Convert.ToInt32(Console.ReadLine()));

            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Сортировка
        /// </summary>
        public void Sort ()
        {


            Console.WriteLine("Вариант сортировки: ");
            Console.WriteLine("1 - по убыванию номеров записей | 2 - по возрастанию дат ");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        rep.Sort();
                        break;
                    }
                case 2:
                    {
                        rep.Sort_dates();
                        break;
                    }
                default:
                    break;
            }
            //  Console.Clear();

            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Импорт записей в диапазоне
        /// </summary>
        public void Import_dates ()
        {
            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            string path2 = @"import_date.csv";

            while (true)
            {
                Console.WriteLine("Введите от какой даты хотите импортировать данные: ");
                date1 = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Введите до какой даты хотите импортировать данные: ");
                date2 = Convert.ToDateTime(Console.ReadLine());
                if (date1 > date2)
                {
                    Console.WriteLine("Вы ввели неправильный диапазон!");
                }
                else break;
            }

            rep.Save(path2, date1, date2);
            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Печать на экран
        /// </summary>
        public void Print ()
        {
            string path = @"notebook.csv";
            rep = new Repository(path);

            Console.Clear();
            rep.PrintNoteBookToConsole();
            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();
        }


        /// <summary>
        /// Сохранение в файл
        /// </summary>
        public void Save ()
        {
            string path = @"notebook.csv";
            rep.Save(path);
            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Выход
        /// </summary>
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
