using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace UIbot
{
    /// <summary>
    /// Логика взаимодействия для StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window
    {
        public StartScreen()
        {
            InitializeComponent();
            if (File.Exists("token.txt"))
            {
                using (StreamReader streamReader = new StreamReader("token.txt"))
                {
                    tokenBot.Text = streamReader.ReadToEnd();
                }
            }
        }

        private void startBot_Click(object sender, RoutedEventArgs e)
        {
            if (tokenBot.Text == String.Empty)
                MessageBox.Show("ВВЕДИТЕ ТОКЕН!","ПРЕДУПРЕЖДЕНИЕ",MessageBoxButton.OK,MessageBoxImage.Warning);

            else if (!tokenBot.Text.Contains(":"))
                MessageBox.Show("Неправильный формат токена!", "ПРЕДУПРЕЖДЕНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);

            else
            {
                using (StreamWriter streamWriter = new StreamWriter("token.txt"))
                {
                    streamWriter.Write(tokenBot.Text);
                }
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();
                mainWindow.Activate();
                this.Close();
            }
                
        }

        private void findFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Текстовый файл (*.txt)|*.txt";
            string file = String.Empty;

            if (openFile.ShowDialog() == true)
            {
                file = openFile.FileName;
            }
            using (StreamReader streamReader = new StreamReader(file))
            {
                tokenBot.Text = streamReader.ReadToEnd();
            }
        }
    }
}
