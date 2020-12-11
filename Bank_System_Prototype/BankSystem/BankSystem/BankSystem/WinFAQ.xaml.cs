using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для WinFAQ.xaml
    /// </summary>
    public partial class WinFAQ : Window
    {
        public WinFAQ()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            string path = "../../../resources/readme.txt";
            using (StreamReader streamReader = new StreamReader(path))
            {
                readbeBlock.Text = streamReader.ReadToEnd();
            }
        }
    }
}
