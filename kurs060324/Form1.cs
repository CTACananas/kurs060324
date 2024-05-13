using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kurs060324
{
    public partial class Avtorization : Form
    {
        private string responseMessage;

        public Avtorization()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            string login = txtLoginName.Text;
            string password = txtPassword.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string Customer = $"select login, password from [Registration] where login = '{login}' and  password = '{password}'";
            SqlCommand command = new SqlCommand(Customer, db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                Form3 an = new Form3(login);
                this.Hide();
                an.Show();

               

            }

            else
            {
                label4.Text = "Неверный логин или павроль";
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration reg = new Registration();
            this.Hide();
            reg.Show();
        }

        private void Avtorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
    

