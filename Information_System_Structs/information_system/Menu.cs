using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace information_system
{
    struct Menu
    {
        #region Поля
        List<Worker> workers;
        List<Departametns> departametns;

        int index;
        private string path;
        #endregion

        #region Конструктор
        public Menu(string Path)
        {
            this.workers = new List<Worker>(1);
            this.departametns = new List<Departametns>(1);
            this.index = 0;
            this.path = Path;

            Automatic_Massive();
        }
        #endregion

        #region Методы

        #region МЕНЮ
        /// <summary>
        /// Основное меню
        /// </summary>
        public void MyMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1 - Работа с сотрудниками 2 - Работа с отделами \n" +
                                  "3 - Экспорт 4 - Импорт 0 - Выход\n");

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1: All_Workers(); break;
                    case 2: All_Departaments(); break;
                    case 3:
                        {
                            Console.WriteLine("В какокй формат хотете экспортировать?");
                            Console.WriteLine("1 - XML, 2 - JSON");
                            switch (Convert.ToInt32(Console.ReadLine()))
                            {
                                case 1:
                                    {
                                        SerializationXML(workers, "workers.xml");
                                        SerializationXML(workers, "departamens.xml"); 
                                        break;
                                    }
                                case 2:
                                    {
                                        SerializationJson(workers, "workers.json");
                                        SerializationJson(departametns, "departamens.json");
                                        break;
                                    }
                                default:
                                    break;
                            }

                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Из какого формата импорт?");
                            Console.WriteLine("1 - XML, 2 - JSON");
                            switch (Convert.ToInt32(Console.ReadLine()))
                            {
                                case 1:
                                    {
                                        workers = DeserializationXML("workers.xml");
                                        Console.WriteLine("Загруженные сотрудники: ");

                                        departametns = Departaments_DeserializationXML("departamens.xml");
                                        Console.WriteLine("Загруженные отделы: ");
                                        Print_of_Departaments();
                                        break;
                                    }
                                case 2:
                                    {
                                        workers = WorkerDeserializationJson("workers.json");
                                        Console.WriteLine("Загруженные сотрудники: ");
                                        Print_of_Massive();

                                        departametns = DepartametnsDeserializationJson("departamens.json");
                                        Console.WriteLine("Загруженные отделы: ");
                                        Print_of_Departaments();
                                        break;
                                    }
                                default:
                                    break;
                            }

                            break;

                        }
                    case 0: Exit(); break;
                    default:
                        break;
                }
            }
        }

        #region Пункты меню

        /// <summary>
        /// Пункт меню - Работа с сотрудниками
        /// </summary>
        private void All_Workers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1 - Добавить сотрудника  2 - Удалить сотрудника \n" +
                                  "3 - Редактировать сотрудинка 4 - Показать всех сотрудников \n" +
                                  "5 - Сортировка 0 - Возврат к предыдущему меню");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1: Add_Worker(); break;
                    case 2:
                        {
                            Print_of_Massive();
                            Console.WriteLine("Введите номер записи для удаления");
                            Delete_Worker(Convert.ToInt32(Console.ReadLine()));
                            break;
                        }
                    case 3:
                        {
                            Print_of_Massive();
                            Console.WriteLine("Введите номер записи для редактирования");
                            Edit_Worker(Convert.ToInt32(Console.ReadLine()));
                            break;
                        }
                    case 4: Print_of_Massive(); break;
                    case 5:
                        {
                            Sort_Age();
                            Sort_Age_And_Salary();
                            Sort_Departament_Age_Salary();
                            break;
                        }
                    case 0: goto BACK;
                    default:
                        break;
                }
            }
        BACK:;
        }

        /// <summary>
        /// Пункт меню - Работа с отделами
        /// </summary>
        private void All_Departaments()
        {
            while (true)
            {

                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1 - Добавить отдел  2 - Удалить отдел \n" +
                                  "3 - Редактировать отдел 4 - Показать все отделы \n" +
                                  "0 - Возврат к предыдущему меню");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1: Add_Departament(); break;
                    case 2:
                        {
                            Print_of_Departaments();
                            Console.WriteLine("Введите номер записи для удаления");
                            Delete_Departament(Convert.ToInt32(Console.ReadLine()));
                            break;
                        }
                    case 3:
                        {
                            Print_of_Departaments();
                            Console.WriteLine("Введите номер записи для редактирования");
                            Edit_Departament(Convert.ToInt32(Console.ReadLine()));
                            break;
                        }
                    case 4: Print_of_Departaments(); break;
                    case 0: goto BACK;
                    default:
                        break;
                }
            }
        BACK:;
        }

        #endregion

        #endregion

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        private void Add_Worker()
        {

            Console.WriteLine("Введите имя сотрудника");
            string firstname = Console.ReadLine();

            Console.WriteLine("Введите фамилию сотрудника");
            string lastname = Console.ReadLine();

            Console.WriteLine("Введите возраст сотрудника");
            uint age = Convert.ToUInt32(Console.ReadLine());

            Console.WriteLine("Выберите отдел: ");

            for (int i = 0; i < departametns.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {departametns[i].Name}");
            }

            int ind = int.Parse(Console.ReadLine()) - 1;//индекс отдела

            Console.WriteLine("Введите зарплату сотрудника");
            int salary = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите количество проектов сотрудника");
            int numberOfProject = Convert.ToInt32(Console.ReadLine());

            workers.Add(new Worker(workers.Count + 1, $"{firstname,17}", $"{lastname,17}", age, $"{departametns[ind].Name}", salary, numberOfProject));

            departametns[ind] = new Departametns(ind + 1,
                                                 departametns[ind].Name,
                                                 departametns[ind].Date_Of_Create,
                                                 Count_Workers(departametns[ind].Name)); //пересчет количества сотрудников в отделе 

            Console.WriteLine($"Запись успешно добавлена!");
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="index"> Номер сотрудника</param>
        private void Delete_Worker(int index)
        {
            int number = index - 1;

            workers.RemoveAt(number);


            for (int i = index; i <= workers.Count; i++)
            {
                workers[i - 1] = new Worker(i,
                                             workers[i - 1].FirstName,
                                             workers[i - 1].LastName,
                                             workers[i - 1].Age,
                                             workers[i - 1].Department,
                                             workers[i - 1].Salary,
                                             workers[i - 1].NumberOfProject);
            }

            //пересчет количества сотрудников в отделе 
            for (int i = 0; i < departametns.Count; i++)
            {
                departametns[i] = new Departametns(i + 1, departametns[i].Name, departametns[i].Date_Of_Create, Count_Workers(departametns[i].Name));
            }

            Console.WriteLine($"Запись успешно удалена!");
            Console.ReadKey();
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="index">Номер сотрудника</param>
        private void Edit_Worker(int index)
        {
            int num = index - 1;
            Console.WriteLine("Что вы хотите изменить?");
            Console.WriteLine("1 - имя | 2 - фамилия | 3 - Возраст | 4 - Отдел | 5 - Зарплата | 6 - Количество проектов | 7 - ВСЁ");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.WriteLine("Введите имя");
                        workers[num] = new Worker(index,
                                                    $"{Console.ReadLine(),17}",
                                                    $"{workers[num].LastName,17}",
                                                    workers[num].Age,
                                                    $"{workers[num].Department}",
                                                    workers[num].Salary,
                                                    workers[num].NumberOfProject);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Введите фамилию");
                        workers[num] = new Worker(index,
                                                    $"{workers[num].FirstName,17}",
                                                    $"{Console.ReadLine(),17}",
                                                    workers[num].Age,
                                                    $"{workers[num].Department}",
                                                    workers[num].Salary,
                                                    workers[num].NumberOfProject);
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Введите возраст");
                        workers[num] = new Worker(index,
                                                    $"{workers[num].FirstName,17}",
                                                    $"{workers[num].LastName,17}",
                                                    Convert.ToUInt32(Console.ReadLine()),
                                                    $"{workers[num].Department}",
                                                    workers[num].Salary,
                                                    workers[num].NumberOfProject);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Выберите департамент");

                        for (int i = 0; i < departametns.Count; i++)
                        {
                            Console.WriteLine($"{i + 1} - {departametns[i].Name}");
                        }

                        int ind = int.Parse(Console.ReadLine()) - 1;//индекс отдела

                        workers[num] = new Worker(index,
                                                    $"{workers[num].FirstName,17}",
                                                    $"{workers[num].LastName,17}",
                                                    workers[num].Age,
                                                    $"{departametns[ind].Name}",
                                                    workers[num].Salary,
                                                    workers[num].NumberOfProject);
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Введите зарплату сотрудника");
                        workers[num] = new Worker(index,
                                                    $"{workers[num].FirstName,17}",
                                                    $"{workers[num].LastName,17}",
                                                    workers[num].Age,
                                                    $"{workers[num].Department}",
                                                    Convert.ToInt32(Console.ReadLine()),
                                                    workers[num].NumberOfProject);
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Введите количество проектов сотрудника");
                        workers[num] = new Worker(index,
                                                    $"{workers[num].FirstName,17}",
                                                    $"{workers[num].LastName,17}",
                                                    workers[num].Age,
                                                    $"{workers[num].Department}",
                                                    workers[num].Salary,
                                                    Convert.ToInt32(Console.ReadLine()));
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("Введите имя сотрудника");
                        string firstname = Console.ReadLine();

                        Console.WriteLine("Введите фамилию сотрудника");
                        string lastname = Console.ReadLine();

                        Console.WriteLine("Введите возраст сотрудника");
                        uint age = Convert.ToUInt32(Console.ReadLine());

                        Console.WriteLine("Выберите отдел: ");

                        for (int i = 0; i < departametns.Count; i++)
                        {
                            Console.WriteLine($"{i + 1} - {departametns[i].Name}");
                        }

                        int ind = int.Parse(Console.ReadLine()) - 1;//индекс отдела

                        Console.WriteLine("Введите зарплату сотрудника");
                        int salary = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите количество проектов сотрудника");
                        int numberOfProject = Convert.ToInt32(Console.ReadLine());

                        workers[num] = new Worker(index, $"{firstname,17}", $"{lastname,17}", age, $"{departametns[ind].Name}", salary, numberOfProject);
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Добавление отдела
        /// </summary>
        private void Add_Departament()
        {
            Console.WriteLine("Введите название отдела");
            string name = Console.ReadLine();

            Console.WriteLine("Введите дату образования отдела");
            DateTime dateOfCreate = Convert.ToDateTime(Console.ReadLine());

            departametns.Add(new Departametns(departametns.Count + 1, name, dateOfCreate, Count_Workers(name)));
            Console.WriteLine("Отдел успешно добавлен!");
        }

        /// <summary>
        /// Удаление отдела
        /// </summary>
        /// <param name="index"></param>
        private void Delete_Departament(int index)
        {
            int number = index - 1;

            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].Department == departametns[number].Name)
                    workers[i] = new Worker(i + 1,
                                             workers[i].FirstName,
                                             workers[i].LastName,
                                             workers[i].Age,
                                             "ОТДЕЛ УДАЛЕН!",
                                             workers[i].Salary,
                                             workers[i].NumberOfProject);
            }

            departametns.RemoveAt(number); //удаление отдела

            for (int i = index; i <= departametns.Count; i++)
            {
                departametns[i - 1] = new Departametns(i,
                             departametns[i - 1].Name,
                             departametns[i - 1].Date_Of_Create,
                             departametns[i - 1].Number_Of_Workers);
            }
            Console.WriteLine($"Запись успешно удалена!");
            Console.ReadKey();
        }

        private void Edit_Departament(int index)
        {
            int num = index - 1;

            Console.WriteLine("Что вы хотите изменить?");
            Console.WriteLine("1 - Название | 2 - Дата создания | 3 - ВСЁ");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.WriteLine("Введите название");
                        string Name_Of_Departaments = Console.ReadLine();


                        for (int i = 0; i < workers.Count; i++)
                        {
                            if (workers[i].Department == departametns[num].Name)
                                workers[i] = new Worker(i + 1,
                                                         workers[i].FirstName,
                                                         workers[i].LastName,
                                                         workers[i].Age,
                                                         Name_Of_Departaments,
                                                         workers[i].Salary,
                                                         workers[i].NumberOfProject);
                        }

                        departametns[num] = new Departametns(index, Name_Of_Departaments, departametns[num].Date_Of_Create, departametns[num].Number_Of_Workers);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Введите дату создания");
                        departametns[num] = new Departametns(index, departametns[num].Name, Convert.ToDateTime(Console.ReadLine()), departametns[num].Number_Of_Workers);
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Введите название");
                        string Name_Of_Departaments = Console.ReadLine();

                        for (int i = 0; i < workers.Count; i++)
                        {
                            if (workers[i].Department == departametns[num].Name)
                                workers[i] = new Worker(i + 1,
                                                         workers[i].FirstName,
                                                         workers[i].LastName,
                                                         workers[i].Age,
                                                         Name_Of_Departaments,
                                                         workers[i].Salary,
                                                         workers[i].NumberOfProject);
                        }

                        Console.WriteLine("Введите дату создания");
                        DateTime dateOfCreate = Convert.ToDateTime(Console.ReadLine());

                        departametns[num] = new Departametns(index, Name_Of_Departaments, dateOfCreate, departametns[num].Number_Of_Workers);
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Автоматическое заполнение
        /// </summary>
        private void Automatic_Massive()
        {
            workers = new List<Worker>();
            departametns = new List<Departametns>();
            DateTime dateTime = new DateTime(2000, 01, 01);
            Random r = new Random();

            #region Заполнение массива сотрудников

            for (index = 0; index < 10; index++)
            {
                int ind = index + 1;

                workers.Add(new Worker(ind,
                                            $"{"Имя_",15}{ind,2}",
                                            $"{"Фамилия_",15}{ind,2}",
                                            Convert.ToUInt32(r.Next(20, 65)),
                                            $"Отдел_{ind / 3 + 1}",
                                            r.Next(10000, 100000),
                                            r.Next(10)));
            }
            Print_of_Massive();

            #endregion

            #region Заполнение массива с отделами

            int check = 1; //сохранение позиции во вложенном цикле
            string name_dep = workers[0].Department; //первое название отдела

            for (int i = 1; i <= 4; i++)
            {
                departametns.Add(new Departametns(i, name_dep, dateTime.AddDays(r.Next(5000)), Count_Workers(name_dep)));
                for (int j = check; j < workers.Count; j++)
                {
                    if (name_dep != workers[j].Department)
                    {
                        check = j;
                        name_dep = workers[j].Department;
                        break;
                    }
                }
            }

            Print_of_Departaments();

            #endregion
        }

        /// <summary>
        /// Печать на экран массива сотрудников
        /// </summary>
        private void Print_of_Massive()
        {
            Console.WriteLine($"{"Номер",7} {"Имя",15} {"Фамилия ",19} {"Возраст",5} {"Отдел",10} {"Зарплата",10} {"Количество проектов",3}");
            foreach (var e in workers)
            {
                Console.WriteLine(e.Print());
            }
            Console.WriteLine("Нажмите на любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Печать на экран массива отделов
        /// </summary>
        private void Print_of_Departaments()
        {
            Console.WriteLine($"{"Номер",7} {"Имя",15} {"Дата создания",15} {" кол-во сотрудников",1}");
            foreach (var e in departametns)
            {
                Console.WriteLine(e.Print());
            }
            Console.WriteLine("Нажмите на любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Подсчет количества сотрудников
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private int Count_Workers(string Name)
        {
            int count = 0;
            for (int i = 0; i < workers.Count; i++)
            {
                if (Name == workers[i].Department) count++;
            }
            return count;
        }

        #region XML

        /// <summary>
        /// Сериализация XML
        /// </summary>
        /// <param name="workers"></param>
        /// <param name="Path"></param>
        private void SerializationXML(List<Worker> this_workers, string Path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Worker>));

            Stream stream = new FileStream(Path, FileMode.Create, FileAccess.Write);

            xmlSerializer.Serialize(stream, this_workers);

            stream.Close();

            Console.WriteLine($"Файл {Path} успешно создан!");
            Console.WriteLine("Нажмите на любую кнопку для продолжения!");
            Console.ReadKey();
        }

        /// <summary>
        /// Перегрузка сериалзации XML
        /// </summary>
        /// <param name="departametns"></param>
        /// <param name="Path"></param>
        private void SerializationXML(List<Departametns> departametns, string Path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Departametns>));

            Stream stream = new FileStream(Path, FileMode.Create, FileAccess.Write);

            xmlSerializer.Serialize(stream, departametns);

            stream.Close();

            Console.WriteLine($"Файл {Path} успешно создан!");
            Console.WriteLine("Нажмите на любую кнопку для продолжения!");
            Console.ReadKey();
        }

        /// <summary>
        /// Десериализация XML 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private List<Worker> DeserializationXML(string Path)
        {
            List<Worker> temp_workers = new List<Worker>();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Worker>));

            Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read);

            temp_workers = xmlSerializer.Deserialize(stream) as List<Worker>;

            stream.Close();

            return temp_workers;
        }

        /// <summary>
        /// Десериализация XML с департаментами
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private List<Departametns> Departaments_DeserializationXML(string Path)
        {
            List<Departametns> temp_departaments = new List<Departametns>();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Departametns>));

            Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read);

            temp_departaments = xmlSerializer.Deserialize(stream) as List<Departametns>;

            stream.Close();
            Console.WriteLine("Загруженные файлы");
            Print_of_Massive();
            return temp_departaments;
        }

        #endregion

        #region JSON

        /// <summary>
        /// Сериализация JSON Сотрудников
        /// </summary>
        /// <param name="this_workers"></param>
        /// <param name="Path"></param>
        private void SerializationJson(List<Worker> this_workers, string Path)
        {
            string json = JsonConvert.SerializeObject(this_workers);
            File.WriteAllText(Path, json);

            Console.WriteLine($"Файл {Path} успешно создан!");
            Console.WriteLine("Нажмите на любую кнопку для продолжения!");
            Console.ReadKey();
        }

        /// <summary>
        /// Сериализация JSON Отделов
        /// </summary>
        /// <param name="departametns"></param>
        /// <param name="Path"></param>
        private void SerializationJson(List<Departametns> departametns, string Path)
        {
            string json = JsonConvert.SerializeObject(departametns);
            File.WriteAllText(Path, json);

            Console.WriteLine($"Файл {Path} успешно создан!");
            Console.WriteLine("Нажмите на любую кнопку для продолжения!");
            Console.ReadKey();
        }

        /// <summary>
        /// Десериализация JSON Сотрудников
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private List<Worker> WorkerDeserializationJson(string Path)
        {
            string json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<List<Worker>>(json);
        }

        /// <summary>
        /// Десериализация JSON Отделов
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private List<Departametns> DepartametnsDeserializationJson(string Path)
        {
            string json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<List<Departametns>>(json);
        }


        #endregion

        private void Sort_Age()
        {
            Console.WriteLine("Сортировка по возрасту: ");

            var rezult = from worker in workers
                         orderby worker.Age
                         select worker;
            foreach (Worker worker in rezult)
                Console.WriteLine(worker.Print());

            Console.WriteLine("Нажмите любую кнопку чтобы продолжить");
            Console.ReadKey();
        }

        private void Sort_Age_And_Salary()
        {
            Console.WriteLine("Сортировка по возрасту и зарплате: ");

            var rezult = from worker in workers
                         orderby worker.Age, worker.Salary
                         select worker;
            foreach (Worker worker in rezult)
                Console.WriteLine(worker.Print());

            Console.WriteLine("Нажмите любую кнопку чтобы продолжить");
            Console.ReadKey();
        }

        private void Sort_Departament_Age_Salary()
        {
            Console.WriteLine("Сортировка по полям возраст и оплате труда в рамках одного департамента: ");

            var rezult = workers.OrderBy(x => x.Department).ThenBy(x => x.Age).ThenBy(x => x.Salary);
            foreach (Worker worker in rezult)
                Console.WriteLine(worker.Print());

            Console.WriteLine("Нажмите любую кнопку чтобы продолжить");
            Console.ReadKey();
        }

        /// <summary>
        /// Выход
        /// </summary>
        private void Exit()
        {
            Environment.Exit(0);
        }

        #endregion
    }
}
