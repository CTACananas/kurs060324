using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs060324
{
    public partial class Form5 : Form
    {
        private PrintDocument printDocument;
        DB db = new DB();
        public Form5(string login)
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            label1.Text = login;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 1160, 1620); // Размер страницы A10
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
                Hide();
            }
        }

        public void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Определение размера страницы A4
            float pageWidth = e.PageSettings.PrintableArea.Width;
            float pageHeight = e.PageSettings.PrintableArea.Height;

            // Установка шрифта для печати
            Font font = new Font("Microsoft Sans Serif", 14);

            // Печать всех Label и TextBox
            foreach (Control control in Controls)
            {
                // Проверка типа элемента управления
                if (control is Label || control is System.Windows.Forms.TextBox || control is ListBox)
                {
                    // Определение координат элемента на форме
                    float x = control.Left * pageWidth / Width;
                    float y = control.Top * pageHeight / Height;
                    string text = "";
                    // Определение текста для печати
                    if (control is Label)
                    {
                        text = ((Label)control).Text;
                    }
                    else if (control is System.Windows.Forms.TextBox)
                    {
                        text = ((System.Windows.Forms.TextBox)control).Text;
                    }
                    else if (control is ListBox)
                    {
                        ListBox listBox = (ListBox)control;

                        // Перебор элементов ListBox
                        foreach (object item in listBox.Items)
                        {
                            text += item.ToString() + "\n";
                        }
                    }


                    // Печать элемента управления
                    e.Graphics.DrawString(text, font, Brushes.Black, x, y);
                }
                else if (control is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)control;
                    Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                    pictureBox.DrawToBitmap(bmp, pictureBox.ClientRectangle);
                    float x = control.Left * pageWidth / Width;
                    float y = control.Top * pageHeight / Height;

                    e.Graphics.DrawImage(bmp, x, y);
                }
            }
        }

        public void Info(string info,string tov, string price)
        {
            textBox1.Text = info;
            textBox2.Text = tov;
            textBox3.Text = price;
        }

        public void LoadFullRowsFromDatabase(ListBox listBox)
        {
            // Создать подключение к базе данных
            // Открыть подключение
            db.openConnection();

            // Создать команду для выборки данных
            using (var command = db.getConnection().CreateCommand())
            {
                command.CommandText = $"select k.id as [Номер], t.Name as [Название товара], p.Organizacia as [Организация],k.kolichestvo as [Количество], k.price as [Цена] from Korzina k,Tovar t, Postavshiki p where t.id = k.id_tovara and p.id = k.id_postavshika and polz = '{label1.Text}'";

                // Выполнить команду и получить результат
                using (var reader = command.ExecuteReader())
                {
                    // Перебрать строки из результата
                    while (reader.Read())
                    {
                        // Создать строку для хранения значений столбцов
                        string row = "";

                        // Перебрать все столбцы в текущей строке
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            // Получить значение столбца
                            string value = reader[i].ToString();

                            // Добавить значение в строку
                            row += $"{value} | ";
                        }

                        // Добавить строку в ListBox
                        listBox.Items.Add(row);
                    }
                }
            }
            db.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadFullRowsFromDatabase(listBox1);
        }
    }
}
