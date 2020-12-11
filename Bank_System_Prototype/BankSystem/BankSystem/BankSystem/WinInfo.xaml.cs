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
using  System.IO;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для WinInfo.xaml
    /// </summary>
    public partial class WinInfo : Window
    {
        public WinInfo()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            using (StreamReader reader = new StreamReader("../../../Text/readme.txt"))
            {
                textBlock.Text = reader.ReadToEnd();
            }
        }
    }
}
