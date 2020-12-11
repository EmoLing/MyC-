using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Waybil_work
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Repository temp_rep;
        private int ID;
        public EditWindow(Repository repository,int ID)
        {
            temp_rep = repository;
            this.ID = ID;
            InitializeComponent();
        }

        private void ButEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DateTime dateTime;
                string Mesto_Pribitiya;
                string Otmetka_O_Pribitii;
                string Otmetka_Ob_Ubutii;
                string Podtvergdaushiy_Doc;
                int Kilometrs;

                if (CheckEditDate.IsChecked == true)
                {
                    dateTime = DateTime.Parse(BoxDate.Text);
                    temp_rep[ID].dateTime = dateTime.ToString("MM/dd/yy");
                }

                if(CheckEditPlace.IsChecked == true)
                {
                    if (CheckPlace.IsChecked == true)
                    {
                        switch (ComboPlace.Text)
                        {
                            case "Дом":
                                Mesto_Pribitiya = "г. Долгопрудный, ул. Парковая, д34";
                                break;
                            case "Работа":
                                Mesto_Pribitiya = "г. Долгопрудный, ул. Октябрьская, 20А";
                                break;
                            case "ИП Базанов":
                                Mesto_Pribitiya = "Апрелевка, ул. Жасминовая";
                                break;
                            case "Заправка":
                                Mesto_Pribitiya = "г. Долгопрудный, Лихачевский проезд, д8";
                                break;
                            default:
                                Mesto_Pribitiya = "г. Долгопрудный, ул. Парковая, д34";
                                break;
                        }
                    }
                    else
                        Mesto_Pribitiya = BoxPlace.Text;

                    temp_rep[ID].Mesto_Pribitiya = Mesto_Pribitiya;
                }

                if (CheckEditTimeArrived.IsChecked == true)
                {
                    if (CheckArrived.IsChecked == true)
                        Otmetka_O_Pribitii = DateTime.Parse(ListTimeArrived.Text).ToString("HH:mm");
                    else
                        Otmetka_O_Pribitii = DateTime.Parse(TimeArrived.Text).ToString("HH:mm");
                    if (!TextIsDate(Otmetka_O_Pribitii))
                    {
                        MessageBox.Show("Укажите время прибытия в формате HH:mm", "WARNING", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                    temp_rep[ID].Otmetka_O_Pribitii = Otmetka_O_Pribitii;
                }

                if (CheckEditTimeDep.IsChecked == true)
                {
                    if (CheckDepartment.IsChecked == true)
                        Otmetka_Ob_Ubutii = DateTime.Parse(ListTimeDepartment.Text).ToString("HH:mm");
                    else
                        Otmetka_Ob_Ubutii = DateTime.Parse(TimeDepartment.Text).ToString("HH:mm");
                    if (!TextIsDate(Otmetka_Ob_Ubutii))
                    {
                        MessageBox.Show("Укажите время убытия в формате HH:mm", "WARNING", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                    temp_rep[ID].Otmetka_Ob_Ubutii = Otmetka_Ob_Ubutii;
                }

                if(CheckEditKilometrs.IsChecked == true)
                {
                    if (CheckKilometrs.IsChecked == true)
                        Kilometrs = int.Parse(ListKilometrs.Text.ToString());
                    else
                        Kilometrs = int.Parse(BoxKillom.Text);

                    double rashod = 0.093; //na 1 km
                    int oil = 51; //стоимость бензина
                    int itog = Convert.ToInt32(rashod * oil * Kilometrs);
                    string temp_rashodi = $"{Kilometrs}км ({itog} руб.)";
                    temp_rep[ID].Lost_Sum = Convert.ToDouble(itog);
                    temp_rep[ID].Zatrati = temp_rashodi;
                }

                if(CheckEditDoc.IsChecked == true)
                {
                    if (checkDocument.IsChecked == true)
                        Podtvergdaushiy_Doc = ComboDoc.Text.ToString();
                    else
                        Podtvergdaushiy_Doc = BoxDocument.Text;
                    temp_rep[ID].Podtvergdaushiy_Doc = Podtvergdaushiy_Doc;
                }
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region CheckBox Checked

        private void CheckEditDate_Checked(object sender, RoutedEventArgs e)
        {
            BoxDate.IsEnabled = true;
            ButOpenCalen.IsEnabled = true;
        }

        private void CheckEditPlace_Checked(object sender, RoutedEventArgs e)
        {
            BoxPlace.IsEnabled = true;
            CheckPlace.IsEnabled = true;

        }

        private void CheckEditTimeArrived_Checked(object sender, RoutedEventArgs e)
        {
            TimeArrived.IsEnabled = true;
            CheckArrived.IsEnabled = true;
        }

        private void CheckEditTimeDep_Checked(object sender, RoutedEventArgs e)
        {
            TimeDepartment.IsEnabled = true;
            CheckDepartment.IsEnabled = true;
        }

        private void CheckEditKilometrs_Checked(object sender, RoutedEventArgs e)
        {
            BoxKillom.IsEnabled = true;
            CheckKilometrs.IsEnabled = true;
        }

        private void CheckEditDoc_Checked(object sender, RoutedEventArgs e)
        {
            BoxDocument.IsEnabled = true;
            checkDocument.IsEnabled = true;
        }

        #endregion

        #region CheckBox UnChecked

        private void CheckEditDate_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxDate.IsEnabled = false;
            ButOpenCalen.IsEnabled = false;
        }

        private void CheckEditPlace_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxPlace.IsEnabled = false;
            CheckPlace.IsEnabled = false;
        }

        private void CheckEditTimeArrived_Unchecked(object sender, RoutedEventArgs e)
        {
            TimeArrived.IsEnabled = false;
            CheckArrived.IsEnabled = false;
        }

        private void CheckEditTimeDep_Unchecked(object sender, RoutedEventArgs e)
        {
            TimeDepartment.IsEnabled = false;
            CheckDepartment.IsEnabled = false;
        }

        private void CheckEditKilometrs_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxKillom.IsEnabled = false;
            CheckKilometrs.IsEnabled = false;
        }

        private void CheckEditDoc_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxDocument.IsEnabled = false;
            checkDocument.IsEnabled = false;
        }
        #endregion

        /// <summary>
        /// Открытие календаря
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButOpenCalen_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> date_time_of_create = new List<DateTime>();
            WindowCalendar windowCalendar = new WindowCalendar(date_time_of_create);

            windowCalendar.Owner = this;
            this.IsEnabled = false;
            if (windowCalendar.ShowDialog() == true)
            {
                windowCalendar.Show();
                windowCalendar.Activate();
            }
            this.IsEnabled = true;
            if (date_time_of_create.Count > 0)
            {
                BoxDate.Text = date_time_of_create[0].Date.ToShortDateString();
            }
        }

        #region ЧекБокс Места прибылия
        private void CheckPlace_Checked(object sender, RoutedEventArgs e)
        {
            BoxPlace.IsEnabled = false;
            ComboPlace.IsEnabled = true;
        }

        private void CheckPlace_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxPlace.IsEnabled = true;
            ComboPlace.IsEnabled = false;
        }

        #endregion

        #region Чекбокс Времени прибытия
        private void CheckArrived_Checked(object sender, RoutedEventArgs e)
        {
            TimeArrived.IsEnabled = false;
            ListTimeArrived.IsEnabled = true;
        }

        private void CheckArrived_Unchecked(object sender, RoutedEventArgs e)
        {
            TimeArrived.IsEnabled = true;
            ListTimeArrived.IsEnabled = false;
        }

        #endregion

        #region Чекбокс Времени отправления
        private void CheckDepartment_Checked(object sender, RoutedEventArgs e)
        {
            TimeDepartment.IsEnabled = false;
            ListTimeDepartment.IsEnabled = true;
        }

        private void CheckDepartment_Unchecked(object sender, RoutedEventArgs e)
        {
            TimeDepartment.IsEnabled = true;
            ListTimeDepartment.IsEnabled = false;
        }

        #endregion

        #region Чекбокс километров
        private void CheckKilometrs_Checked(object sender, RoutedEventArgs e)
        {
            BoxKillom.IsEnabled = false;
            ListKilometrs.IsEnabled = true;
        }

        private void CheckKilometrs_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxKillom.IsEnabled = true;
            ListKilometrs.IsEnabled = false;
        }
        #endregion

        private void checkDocument_Checked(object sender, RoutedEventArgs e)
        {
            BoxDocument.IsEnabled = false;
            ComboDoc.IsEnabled = true;
        }

        private void checkDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            BoxDocument.IsEnabled = true;
            ComboDoc.IsEnabled = false;
        }


        private void BoxKillom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TimeArrived_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                (
                    textBox.Text.Where
                        (ch =>
                            ch == ':' || ch == '.'
                                      || (ch >= '0' && ch <= '9')).ToArray()
                );
            }
        }

        static bool TextIsDate(string text)
        {
            var dateFormat = "HH:mm";
            var dateFormat1 = "H:mm";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate) ||
                DateTime.TryParseExact(text, dateFormat1, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            BoxDate.Text = temp_rep[ID].dateTime;
            BoxPlace.Text = temp_rep[ID].Mesto_Pribitiya;
            TimeArrived.Text = temp_rep[ID].Otmetka_O_Pribitii;
            TimeDepartment.Text = temp_rep[ID].Otmetka_Ob_Ubutii;
            BoxKillom.Text = ((temp_rep[ID].Lost_Sum / 51) / 0.093).ToString();
            BoxDocument.Text = temp_rep[ID].Podtvergdaushiy_Doc;
        }
    }
}
