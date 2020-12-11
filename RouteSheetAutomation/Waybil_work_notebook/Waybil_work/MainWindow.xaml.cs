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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Win32;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Table = Xceed.Document.NET.Table;
using Word = Xceed.Words.NET;
namespace Waybil_work
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository repository = new Repository();
        double Sum_inCheck = 0;
        private string file_Name = string.Empty;
        string doc_number = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Sum_inCheck > 0)
            {
                AddWindow addWindow = new AddWindow(repository);

                int temp_count = repository.waybils.Count;

                addWindow.Owner = this;
                this.IsEnabled = false;
                if (addWindow.ShowDialog() == true)
                {
                    addWindow.Show();
                    addWindow.Activate();
                }
                this.IsEnabled = true;
                if (repository.waybils.Count > 0)
                    ButSaveWord.IsEnabled = true;
                if (repository.waybils.Count > temp_count)
                {
                    list_view_notes.Items.Add(newItem: repository.waybils[repository.waybils.Count - 1]);
                    list_view_notes.Items.Clear();

                    Sum_inCheck -= repository.waybils[temp_count].Lost_Sum;
                    LabelLostSum.Content = Sum_inCheck;

                    for (int i = 0; i < repository.waybils.Count; i++)
                    {
                        list_view_notes.Items.Add(repository.waybils[i]);

                    }

                    BlockAdd();
                }
            }
            else
                MessageBox.Show("Вначале введите сумму из чека!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ButSaveSum_Click(object sender, RoutedEventArgs e)
        {
            if (BoxSum.Text != string.Empty)
            {
                Sum_inCheck = int.Parse(BoxSum.Text);
                LabelLostSum.Content = Sum_inCheck;
                ButSaveSum.IsEnabled = false;
            }
            else MessageBox.Show("Введите сумму из чека", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButClear_Click(object sender, RoutedEventArgs e)
        {
            Sum_inCheck = 0;
            LabelLostSum.Content = Sum_inCheck;
            ButSaveSum.IsEnabled = true;
            ButAdd.IsEnabled = true;
        }

        /// <summary>
        /// Сохранение в Ворд
        /// </summary>
        private void SaveWord()
        {
            int N = repository.waybils.Count + 1;
            // путь к документу
            string pathDocument = "templates/template.docx";

            // создаём документ
            DocX document = DocX.Load(pathDocument);
            document.Bookmarks["Date"].SetText(doc_number);
            document.Bookmarks["Date"].Paragraph.Bold(true);
            document.Bookmarks["Date"].Paragraph.FontSize(20);
            // создаём таблицу с 1 строкой и 2 столбцами
            Table table = document.AddTable(N, 6);
            // располагаем таблицу по центру
            table.Alignment = Alignment.center;
            // меняем стандартный дизайн таблицы
            //table.Design = TableDesign.TableGrid;
            table.Rows[0].Height = 40;
            table.Rows[0].Cells[0].Width = 50;
            table.Rows[0].Cells[1].Width = 170;
            table.Rows[0].Cells[5].Width = 120;
            // заполнение ячейки текстом
            table.Rows[0].Cells[0].Paragraphs[0].Append("Дата поездки").FontSize(11);
            table.Rows[0].Cells[1].Paragraphs[0].Append("Место прибытия, адрес").FontSize(11);
            table.Rows[0].Cells[2].Paragraphs[0].Append("Отметка о прибытии").FontSize(11);
            table.Rows[0].Cells[3].Paragraphs[0].Append("Отметка об убытии").FontSize(11);
            table.Rows[0].Cells[4].Paragraphs[0].Append("Подтверждающий документ").FontSize(11);
            table.Rows[0].Cells[5].Paragraphs[0].Append("Затраты").FontSize(11);

            for (int i = 1; i < N; i++)
            {
                int j = i - 1;
                table.Rows[i].Cells[0].Paragraphs[0].Append(repository.waybils[j].dateTime).FontSize(11);
                table.Rows[i].Cells[1].Paragraphs[0].Append(repository.waybils[j].Mesto_Pribitiya).FontSize(11);
                table.Rows[i].Cells[2].Paragraphs[0].Append(repository.waybils[j].Otmetka_O_Pribitii).FontSize(11);
                table.Rows[i].Cells[3].Paragraphs[0].Append(repository.waybils[j].Otmetka_Ob_Ubutii).FontSize(11);
                table.Rows[i].Cells[4].Paragraphs[0].Append(repository.waybils[j].Podtvergdaushiy_Doc).FontSize(11);
                table.Rows[i].Cells[5].Paragraphs[0].Append(repository.waybils[j].Zatrati).FontSize(11);
            }

            document.Bookmarks["Tables"].Paragraph.InsertTableBeforeSelf(table);
            Save_Doc(document);
        }

        /// <summary>
        /// Сохранение через обозреватель
        /// </summary>
        /// <param name="document"></param>
        private void Save_Doc(DocX document)
        {
            Stream myStream;
            string Full_name;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "docx files (*.docx)|*.docx";

            doc_number = doc_number.Replace('/', '_');
            saveFileDialog.FileName = doc_number + ".docx";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    document.SaveAs(myStream);
                    myStream.Close();
                }
                Full_name = saveFileDialog.FileName;
                MessageBox.Show("Успешно", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show($"Файл был создан по пути:\n{Full_name}", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                file_Name = Full_name;
                MessageBoxResult result = MessageBox.Show("Хотите распечатать?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    Print_Doc(Full_name);//Печать на принтере
                MessageBoxResult result2 = MessageBox.Show("Хотите просмотреть Word файл?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result2 == MessageBoxResult.Yes)
                    Process.Start(Full_name);//Печать на принтере
            }
        }

        /// <summary>
        /// Печать на принтере
        /// </summary>
        /// <param name="Full_name">Путь к файлу</param>
        private void Print_Doc(string Full_name)
        {
            try
            {
                Spire.Doc.Document doc = new Spire.Doc.Document();
                doc.LoadFromFile(Full_name);
                var printDocument = doc.PrintDocument;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                    printDocument.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButSaveWord_Click(object sender, RoutedEventArgs e)
        {
            WindowsNameWay nameWay = new WindowsNameWay();
            nameWay.Owner = this;
            this.IsEnabled = false;
            if (nameWay.ShowDialog() == true)
            {
                nameWay.Show();
                nameWay.Activate();
            }
            doc_number = nameWay.Name;
            if (doc_number != string.Empty)
            {
                this.IsEnabled = true;
                SaveWord();
                ButPrint.IsEnabled = true;
            }
            else
            {
                this.IsEnabled = true;
                return;
            }
        }

        /// <summary>
        /// Печать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButPrint_Click(object sender, RoutedEventArgs e)
        {
            Print_Doc(file_Name);
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButDelete_Click(object sender, RoutedEventArgs e)
        {
            if (list_view_notes.SelectedItem != null)
            {
                Sum_inCheck += (list_view_notes.SelectedItem as Waybil_Class).Lost_Sum;
                LabelLostSum.Content = Sum_inCheck;
                for (int i = 0; i < repository.waybils.Count; i++)
                {
                    if ((list_view_notes.SelectedItem as Waybil_Class).ID == repository.waybils[i].ID)
                        repository.waybils.Remove(repository.waybils[i]);
                }
                list_view_notes.Items.Clear();
                foreach (var item in repository.waybils)
                {
                    list_view_notes.Items.Add(item);
                }
                BlockAdd();
            }
            else
                MessageBox.Show("Строка не выбрана", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButEdit_Click(object sender, RoutedEventArgs e)
        {
            if (list_view_notes.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                Sum_inCheck += (list_view_notes.SelectedItem as Waybil_Class).Lost_Sum;

                EditWindow editWindow = new EditWindow(repository, (list_view_notes.SelectedItem as Waybil_Class).ID);
                editWindow.Owner = this;
                this.IsEnabled = false;
                if (editWindow.ShowDialog() == true)
                {
                    editWindow.Show();
                    editWindow.Activate();
                }
                this.IsEnabled = true;

                Sum_inCheck -= (list_view_notes.SelectedItem as Waybil_Class).Lost_Sum;
                LabelLostSum.Content = Sum_inCheck;

                list_view_notes.Items.Clear();
                for (int i = 0; i < repository.waybils.Count; i++)
                {
                    list_view_notes.Items.Add(repository.waybils[i]);

                }

                BlockAdd();
            }
        }

        private void BlockAdd()
        {
            if (Sum_inCheck <= 0)
            {
                MessageBox.Show("Оставшаяся сумма из чека меньше 0, можно печатать!", "Complete",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                ButAdd.IsEnabled = false;
            }
            else
                ButAdd.IsEnabled = true;
        }

        private void ButTotalClear_Click(object sender, RoutedEventArgs e)
        {
            repository = new Repository();
            list_view_notes.Items.Clear();
            Sum_inCheck = 0;
            ButAdd.IsEnabled = true;
            LabelLostSum.Content = Sum_inCheck;
            ButPrint.IsEnabled = false;
        }
    }
}
