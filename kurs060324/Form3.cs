using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace kurs060324
{
    public partial class Form3 : Form
    {
 
        public Form3(string login)
        {
            InitializeComponent();
            linkLabel1.Text = login;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            DB db = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            table.Clear();
            string zadan = $"select id as [Номер в списке], Organizacia as [Название организации] from [Postavshiki]";
            SqlCommand command = new SqlCommand(zadan, db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            // Проверяем, что индекс строки положителен
            if (e.RowIndex >= 0)
            {
                // Получаем выбранную строку
                DataGridViewRow row = dataGridView3.Rows[selectedRow];
                idtov = Convert.ToInt32(row.Cells[0].Value.ToString());
                price = Convert.ToInt32(row.Cells[2].Value.ToString());
            }
        }
        
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {     
                int selectedRow = e.RowIndex;
                // Проверяем, что индекс строки положителен
                if (e.RowIndex >= 0)
                {
                    // Получаем выбранную строку
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    idpost = Convert.ToInt32(row.Cells[0].Value.ToString());

                }
                DataTable table = new DataTable();
                DB db = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                table.Clear();
                string zadan = $"SELECT T.id as [Номер], T.Name as [Название товара], T.Price as [Цена], T.Srok_godnosti as [Срок годности] FROM Tovar T, Sviazka S, Postavshiki P where T.id = S.id_Tovar and S.id_Postavshiki = P.id AND P.id ={idpost} ";
                SqlCommand command = new SqlCommand(zadan, db.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                dataGridView3.DataSource = table;
        }

        

        private void Button1_Click(object sender, EventArgs e)
        {
            Form4 an = new Form4(linkLabel1.Text);
            an.ShowDialog();
        }

        int idtov = 0;
        int price = 0;
        int idpost = 0;
        private void Button2_Click(object sender, EventArgs e)
        {
            Tov();
        }

        public void Tov()
        {
            DB db = new DB();
            db.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"select * from Postavshiki where id =  {idpost} ";
            SqlCommand command = new SqlCommand(query, db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);


            SqlDataAdapter adapter1 = new SqlDataAdapter();
            DataTable table1 = new DataTable();
            string query1 = $"select * from Tovar where id =  {idtov}";
            SqlCommand command1 = new SqlCommand(query1, db.getConnection());
            adapter1.SelectCommand = command1;
            adapter1.Fill(table1);

            if (idtov == 0)
            {
                MessageBox.Show("Не добавлен!", "Не надо", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (table.Rows.Count != 0)
            {
                SqlCommand insertCommand = new SqlCommand($"insert into Korzina (polz,id_tovara, id_postavshika, kolichestvo, price) values ('{linkLabel1.Text}','{idtov}', '{idpost}', '{(double)numericUpDown1.Value}','{price * (int)numericUpDown1.Value}')", db.getConnection());
                insertCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Не добавлен!", "Не надо", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            db.closeConnection();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Avtorization f = new Avtorization();
            f.ShowDialog();
        }
    }
    
}
