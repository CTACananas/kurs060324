using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs060324
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string Login = textBox1.Text;
            string Password = textBox2.Text;
            string Passwordproverka = textBox3.Text;

            if (Password == "" || Passwordproverka == "" || Login == "")
            {
                label5.Text = "Пожалуйста, введите логин/пароль";
                return;
            }
            
            else if (Password != Passwordproverka)
            {
                label5.Text = "Пожалуйста, подтвердите пароль";
                return;

            }
            DB db = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"select login from Registration where login = '{Login}'";
            SqlCommand command = new SqlCommand(query, db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            db.openConnection();
            if (table.Rows.Count == 0)
            {
                // Если пользователя нет, выполняем запрос на добавление нового пользователя в базу данных.
                SqlCommand insertCommand = new SqlCommand($"insert into Registration (login, password) values ('{Login}',  '{Password}')", db.getConnection());
                // Если запрос выполнен успешно, выводим сообщение об успешной регистрации и открываем форму входа.
                if (insertCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Регистрация прошла успешно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Avtorization frmlog = new Avtorization();
                    this.Close();
                    frmlog.Show();

                }

            }
            else
            {
                label5.Text = "Аккаунт не создан";
            }
            db.closeConnection();

        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
