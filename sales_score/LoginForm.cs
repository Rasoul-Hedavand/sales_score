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

namespace sales_score
{
    public partial class LoginForm : Form
    {
        private SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");

        public LoginForm()
        {
            InitializeComponent();
            InitializeTextBoxes();

        }
        private void InitializeTextBoxes()
        {
            // تنظیمات اولیه یا ایجاد TextBoxes
            SetPlaceholderText(textBoxUsername, "Username");
            SetPlaceholderText(textBoxPassword, "Password");

            // اتصال رویدادها
            textBoxUsername.Enter += TextBox_Enter;
            textBoxUsername.Leave += TextBox_Leave;

            textBoxPassword.Enter += TextBox_Enter;
            textBoxPassword.Leave += TextBox_Leave;
        }
        private void TextBox_Enter(object sender, EventArgs e)
        {
            // وقتی TextBox در فوکوس قرار می‌گیرد، اگر متن placeholder باشد، حذف شود
            TextBox textBox = (TextBox)sender;
            if (textBox.ForeColor == Color.Gray && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText; // یا هر رنگ دلخواه دیگر
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            // وقتی کاربر از TextBox خارج می‌شود و متن خالی است، متن placeholder اضافه شود
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetPlaceholderText(textBox, textBox.Tag.ToString());
            }
        }

        private void SetPlaceholderText(TextBox textBox, string placeholder)
        {
            // تنظیم متن placeholder و رنگ آن
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.Tag = placeholder; // متن placeholder را به عنوان Tag ذخیره می‌کنیم
        }
        

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //    label1.BackColor = Color.FromArgb(255, 38, 37, 43);
         
           
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        public static string code_namayandegi;
        private void rjButton1_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (CheckCredentials(username, password))
            {
                code_namayandegi = GetCodeNamayandegi(username, password);
                
                 main_form mainForm = new main_form();
                 mainForm.Show();
                   this.Hide();
            }
            else
            {
                MessageBox.Show("نام کاربری یا رمز عبور اشتباه است.");
            }
        }

        private bool CheckCredentials(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM All_namayandegi WHERE نام_کاربری = @Username AND رمز_عبور = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        if (reader.Read())
                        {
                        
                            return String.Equals(reader["نام_کاربری"].ToString(), username, StringComparison.Ordinal)
                                && String.Equals(reader["رمز_عبور"].ToString(), password, StringComparison.Ordinal);
                        }
                        else
                        {
                            return false; // No data found
                        }
                    }
                }
            }

        }
        private string GetCodeNamayandegi(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                string query = "SELECT کد_نمایندگی FROM All_namayandegi WHERE نام_کاربری = @Username AND رمز_عبور = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                   
                    return command.ExecuteScalar()?.ToString();
                }
            }
        }
    }
}