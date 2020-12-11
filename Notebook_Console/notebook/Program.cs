using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notebook
{
    class Program
    {
        static void Main(string[] args)
        {


            Menu menu = new Menu();

            while (true)
            {
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1 - добавить запись  2 - удалить запись  \n" +
                                  "3 - редактировать запись  4 - сортировка  \n" +
                                  "5 - импортирование по диапазону 6 - печать на экран \n" +
                                  "7 - сохранить изменения 0 - выход");
                
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        {
                            menu.Print();
                            menu.Add();
                        break;
                        }
                    case 2:
                        {
                            menu.Print();
                            menu.Delete();
                            break;
                        }
                    case 3:
                        {
                            menu.Print();
                            menu.Editing();
                            break;
                        }
                    case 4:
                        {
                            menu.Sort();
                            break;
                        }
                    case 5:
                        {
                            menu.Import_dates();
                            break;
                        }

                    case 6:
                        {
                            menu.Print();
                            break;
                        }
                    case 7:
                        {
                            menu.Save();
                            break;
                        }
                    case 0:
                        {
                            menu.Exit();
                            break;
                        }
                    default:
                        break;
                }
                Console.Clear();
            }
        }
    }
}
