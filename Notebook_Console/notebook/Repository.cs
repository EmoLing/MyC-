using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace notebook
{
    struct Repository
    {
        #region Поля

        private Note[] notes; //Массив для хранения записей

        private string path; // путь к файлу с данными

        int index; //текущий элемент

       private string[] titles; //Массив для хранения заголовков

        #endregion

        #region конструктор

        public Repository(string path)
        {
            this.path = path; //сохранение пути к файлу
            this.index = 0; //текущая позиция для добавления записи
            this.titles = new string[0]; //инициализация массива заголовков 
            this.notes = new Note[1]; //инициализация массива записей

            this.Load();//загрузка из файла
        }

        #endregion

        #region методы

        /// <summary>
        /// увеличение места в массиве записей
        /// </summary>
        /// <param name="flag"></param>
        private void Resize(bool flag)
        {
            if (flag)
            {
                Array.Resize(ref this.notes, this.notes.Length * 2);
            }
        }

        /// <summary>
        /// добавление записи
        /// </summary>
        /// <param name="ThisNote"> Запись</param>
        public void Add (Note ThisNote)
        {
            this.Resize(index >= this.notes.Length);
            this.notes[index] = ThisNote;
            this.index++;

            PrintNoteBookToConsole();

            Console.WriteLine("Хотите сохранить изменения в файл? (y/n)");
            char k = Convert.ToChar(Console.ReadLine());
            if (k == 'y')
                Save(this.path);
        }

        /// <summary>
        /// добавление данных в массив, загруженных из файла 
        /// </summary>
        /// <param name="ThisNote"></param>
        public void Add_witout_save_in_file(Note ThisNote)
        {
            this.Resize(index >= this.notes.Length);
            this.notes[index] = ThisNote;
            this.index++;
        }

        /// <summary>
        /// удаление записи
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            int index_of_number = index - 1; 
            if (index_of_number < this.notes.Length)
            {
                for (int i = index_of_number; i < this.notes.Length; i++)
                {
                    if (i + 1 != this.notes.Length)
                    {
                        this.notes[i] = this.notes[i + 1];
                        this.notes[i].Number = i + 1;
                    }
                    else this.notes[i].Date = new DateTime (0);

                }
            }
            else if (index_of_number == this.notes.Length)
            {
                for (int i = index_of_number; i > 0; i--)
                {
                    if (i - 1 != 0)
                    {
                        this.notes[i] = this.notes[i - 1];
                        this.notes[i].Number = i - 1;
                    }
                    else this.notes[i].Date = new DateTime(0);

                }
            }
            this.index--;
            PrintNoteBookToConsole();

            Console.WriteLine("Хотите сохранить изменения в файл? (y/n)");

            char k = Convert.ToChar(Console.ReadLine());
            if (k == 'y')
                Save(this.path);
        }
        
        /// <summary>
        /// редактирование записи
        /// </summary>
        /// <param name="index"></param>
        public void Editing (int index)
        {
            Console.WriteLine("Что вы хотите отредактировать?");
            Console.WriteLine("1 - дата | 2 - описание | 3 - место | 4 - примечание | 5 - всё: ");
            int k = Convert.ToInt32(Console.ReadLine());

            switch (k)
            {
                case 1:
                    {
                        Console.WriteLine("Введите дату: ");
                        this.notes[index-1].Date = Convert.ToDateTime(Console.ReadLine());
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Введите описание: ");
                        this.notes[index-1].Specification = Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Введите место: ");
                        this.notes[index-1].Place = Console.ReadLine();
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Введите примечание: ");
                        this.notes[index-1].Remark = Console.ReadLine();
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("\nВведите дату: ");
                        this.notes[index-1].Date = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("\nВведите описание: ");
                        this.notes[index-1].Specification = Console.ReadLine();

                        Console.WriteLine("\nВведите место: ");
                        this.notes[index-1].Place = Console.ReadLine();

                        Console.WriteLine("\nВведите примечание: ");
                        this.notes[index-1].Remark = Console.ReadLine();
                        break;
                    }
                default:
                    break;
            }
            PrintNoteBookToConsole();

            Console.WriteLine("Хотите сохранить изменения в файл? (y/n)");
            char s = Convert.ToChar(Console.ReadLine());
            if (s == 'y')
                Save(this.path);
        }

        /// <summary>
        /// Заполнение массива заголовков
        /// </summary>
        /// <returns></returns>
        private string Titles ()
        { 
              return $"{"Номер",5} | {"Дата",21} | {"Описание",20} | {"Место",20} | {"Примечание",20} ";
        }

        /// <summary>
        /// печать в консоль
        /// </summary>
        public void PrintNoteBookToConsole ()
        {
            Console.WriteLine(Titles());
            
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.notes[i].Print());
            }
        }

        /// <summary>
        /// получение номера записи
        /// </summary>
        /// <returns></returns>
        public int IndexOfNumber ()
        {
            this.Resize(index >= this.notes.Length);
            this.notes[index].Number = index + 1;
            return this.notes[index].Number;
        }

        /// <summary>
        /// загрузка из файла
        /// </summary>
        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('\t');
                    Add_witout_save_in_file(new Note(IndexOfNumber(), Convert.ToDateTime(args[1]), 
                        args[2], args[3], args[4]));
                }
            }
            Console.WriteLine("Данные успешно загружены!");
            PrintNoteBookToConsole();
        }

        /// <summary>
        /// добавление записей в массив из другого файла
        /// </summary>
        /// <param name="Path"></param>
        public void Load(string Path)
        {
            using (StreamReader sr = new StreamReader(Path, Encoding.Unicode))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('\t');

                    Add_witout_save_in_file(new Note(IndexOfNumber(), Convert.ToDateTime(args[1]), args[2], args[3], args[4]));
                }
            }
            Console.WriteLine("Данные успешно загружены!");
            PrintNoteBookToConsole();
        }

        /// <summary>
        /// сохранение в файл
        /// </summary>
        /// <param name="Path"></param>
        public void Save (string Path)
        {
            using (StreamWriter sw = new StreamWriter(Path,false,Encoding.Unicode))
            {
                for (int i = 0; i < this.index; i++)
                {
                    string temp = String.Format("{0} \t {1} \t {2} \t {3} \t {4}",
                                            this.notes[i].Number,
                                            this.notes[i].Date.ToShortDateString(),
                                            this.notes[i].Specification,
                                            this.notes[i].Place,
                                            this.notes[i].Remark);
                    sw.Write(temp);
                    sw.WriteLine();
                }
            }
            Console.WriteLine("Сохранение прошло успешно!");
        }

        /// <summary>
        /// Импорт записей в диапозоне дат
        /// </summary>
        /// <param name="Path">Путь к файлу</param>
        /// <param name="date1">начальная дата</param>
        /// <param name="date2">конечная дата</param>
        public void Save (string Path,DateTime date1, DateTime date2)
        {
            using (StreamWriter sw = new StreamWriter(Path, false, Encoding.Unicode))
            {
                for (int i = 0; i < this.index; i++)
                {
                    if ( this.notes[i].Date >= date1 && this.notes[i].Date <= date2)
                    {
                        string temp = String.Format("{0} \t {1} \t {2} \t {3} \t {4}",
                                                this.notes[i].Number,
                                                this.notes[i].Date.ToShortDateString(),
                                                this.notes[i].Specification,
                                                this.notes[i].Place,
                                                this.notes[i].Remark);
                        sw.Write(temp);
                        sw.WriteLine();
                    }
                }
            }
            for (int i = 0; i < this.index; i++)
            {
                if (this.notes[i].Date >= date1 && this.notes[i].Date <= date2)
                {
                    Console.WriteLine(this.notes[i].Print());
                }
            }
            Console.WriteLine($"Данные записи были импортированы в файл {Path}");
        }

        /// <summary>
        /// Сортировка по дате
        /// </summary>
        public void Sort_dates ()
        {
            Note[] notes_sort = notes; //массив для хранения отсортированных данных
            Note note_sort = new Note();//хранение 1 строки
            DateTime date_min= notes_sort[0].Date;//хранение даты

            for (int i = 0; i < index; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    if (notes_sort[i].Date > notes_sort[j].Date)
                    {
                        date_min = notes_sort[i].Date;
                        note_sort = notes_sort[i];

                        notes_sort[i].Date = notes_sort[j].Date;
                        notes_sort[i] = notes_sort[j];

                        notes_sort[j].Date = date_min;
                        notes_sort[j] = note_sort;
                    }
                }
            }
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(notes_sort[i].Print());
            }
        }

        /// <summary>
        /// сортировка по убыванию номеров записей
        /// </summary>
        public void Sort()
        {
            Note[] notes_sort = notes; //массив для хранения отсортированных данных
            Note note_sort = new Note();//хранение 1 строки
            int int_min = int.MinValue;//хранение числа

            for (int i = 0; i < index; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    if (notes_sort[i].Number < notes_sort[j].Number)
                    {
                        int_min = notes_sort[i].Number;
                        note_sort = notes_sort[i];

                        notes_sort[i].Number = notes_sort[j].Number;
                        notes_sort[i] = notes_sort[j];

                        notes_sort[j].Number = int_min;
                        notes_sort[j] = note_sort;
                    }
                }
            }
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(notes_sort[i].Print());
            }
        }
        #endregion
    }
}
