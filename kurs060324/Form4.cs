using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace kurs060324
{
    public partial class Form4 : Form
    { 
        DB db = new DB();
        private SqlDataAdapter adapter;
        private DataTable table;
        public Form4(string login)
        {
            InitializeComponent();
            label3.Text = login;
        }
       

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void f()
        {
            table = new DataTable();
            DB db = new DB();
            adapter = new SqlDataAdapter();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            table.Clear();
            string zadan = $"select k.id as [Номер], t.Name as [Название товара], p.Organizacia as [Организация],k.kolichestvo as [Количество], k.price as [Цена] from Korzina k,Tovar t, Postavshiki p where t.id = k.id_tovara and p.id = k.id_postavshika and polz = '{label3.Text}'";
            SqlCommand command = new SqlCommand(zadan, db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            // Рассчитать сумму значений в столбце "price"
            int sum = 0;
            foreach (DataRow row in table.Rows)
            {
                sum += int.Parse(row["Цена"].ToString());
            }

            // Вывести сумму в Label
            label2.Text = sum.ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            f();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(label3.Text);
            Random rand = new Random();
            form5.Info($"* Название магазина: Шишкина радость (ШР)\r\n* Адрес магазина: Покровского 21\r\n* Дата и время покупки: {DateTime.Now}\r\n* Номер чека: {rand.Next(10000,100000)}\r\n", "| Наименование товара | Организация | Количество | Итоговая цена |\r\n|---|---|---|---|", $"Общая цена: {label2.Text}");
            form5.ShowDialog();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string deleteQuery = $"DELETE FROM Korzina where polz = '{label3.Text}'";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, db.getConnection());
            db.openConnection();
            deleteCommand.ExecuteNonQuery();
            db.closeConnection();
            f();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Получить индексы ячейки
            int rowIndex = dataGridView1.CurrentCell.RowIndex;

            // Получить значение ячейки
            int id = (int)dataGridView1[0, rowIndex].Value;

            // Удалить запись из базы данных
            string deleteQuery = $"DELETE FROM Korzina WHERE id = {id} and polz = '{label3.Text}'";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, db.getConnection());
            db.openConnection();
            deleteCommand.ExecuteNonQuery();
            db.closeConnection();
            f();
        }
    }
}
