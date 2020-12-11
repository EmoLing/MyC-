using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WindowsNameWay.xaml
    /// </summary>
    public partial class WindowsNameWay : Window
    {
        public string Name = string.Empty;
        public WindowsNameWay()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BoxName.Text != string.Empty)
                {
                    DateTime dateTime = DateTime.Parse(BoxName.Text);
                    Name = dateTime.ToString("dd'/'MM'/'yy");
                    Close();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
