using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bank_System
{
    /// <summary>
    /// Логика взаимодействия для WindowCalendar.xaml
    /// </summary>
    public partial class WindowCalendar : Window
    {
        List<DateTime> TempDateTime;
        public WindowCalendar(List<DateTime> date_time_of_create)
        {
            TempDateTime = date_time_of_create;
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TempDateTime.Add((DateTime)NameCalendar.SelectedDate);
            this.Close();
        }

        private void NameCalendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NameCalendar.SelectedDate != null)
            {
                TempDateTime.Add((DateTime)NameCalendar.SelectedDate);
                this.Close();
            }
        }
    }
}
