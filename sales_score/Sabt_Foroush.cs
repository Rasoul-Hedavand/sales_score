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
    public partial class Sabt_Foroush : Form
    {


        private SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
        private Point offset;

        Panel_foroush y;
        public Sabt_Foroush(Panel_foroush z)
        {
            InitializeComponent();
            this.y = z;
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int mojudi = int.Parse(textBox9.Text) - int.Parse(textBox3.Text);

                using (SqlCommand command = new SqlCommand("UPDATE Anbar_Kala_Input SET تعداد = @Mojudi WHERE کد_محصول = @ProductCode", con))
                {
                    con.Open();

                    command.Parameters.AddWithValue("@Mojudi", mojudi);
                    command.Parameters.AddWithValue("@ProductCode", textBox1.Text);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("تعداد با موفقیت آپدیت شد.");
                    }
                    else
                    {
                        MessageBox.Show("آپدیت انجام نشد. محصول با کد مورد نظر یافت نشد یا خطایی رخ داد.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا: {ex.Message}");
            }
            finally
            {
                con.Close();
            }


            DataTable dataTable = (DataTable)y.dataGridView1.DataSource;
            
            DataRow newRow = dataTable.NewRow();
            newRow[1] = Convert.ToInt32(textBox1.Text);  // نام ستون اول
            newRow[2] = Convert.ToInt32(textBox5.Text); // نام ستون دوم
            newRow[3] = Convert.ToInt32(textBox10.Text); // نام ستون سوم
            newRow[4] = Convert.ToInt32(textBox3.Text);
            newRow[5] = Convert.ToDecimal(textBox2.Text);
            newRow[6] = Convert.ToDecimal(textBox8.Text);
            newRow[7] = textBox6.Text;
            newRow[8] = textBox7.Text;
            newRow[9] = dateTimePicker1.Value;
            newRow[10] = textBox4.Text;
            dataTable.Rows.Add(newRow);
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // محاسبه تغییر مختصات ماوس نسبت به موقعیت قبلی
                int deltaX = e.X - offset.X;
                int deltaY = e.Y - offset.Y;

                // تغییر موقعیت فرم با تغییر مختصات ماوس
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offset = e.Location; // ذخیره مختصات نسبی ماوس به کنترل panel1
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = (Convert.ToInt32(textBox2.Text) * Convert.ToInt32(textBox3.Text)).ToString();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // متوقف کردن پردازش کاراکتر
        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            textBox10.Text = LoginForm.code_namayandegi;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // اجرای مقایسه و دریافت مقدار از SQL Server
            string kod_mahsool = textBox1.Text;

            if (CheckIfProductExists(kod_mahsool))
            {
                int tedad = GetTedadFromDatabase(kod_mahsool);
                textBox9.Text = tedad.ToString();
            }
            else
            {
                textBox9.Text = ""; // مقدار خالی در صورت عدم وجود
            }
        }
        private bool CheckIfProductExists(string kod_mahsool)
        {
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Anbar_Kala_Input WHERE کد_محصول = @کد_محصول";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@کد_محصول", kod_mahsool);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        private int GetTedadFromDatabase(string kod_mahsool)
        {
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                string query = "SELECT تعداد FROM Anbar_Kala_Input WHERE کد_محصول = @کد_محصول";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@کد_محصول", kod_mahsool);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int tedad_input;
            int tedad_anbar;

            if (int.TryParse(textBox3.Text, out tedad_input) && int.TryParse(textBox9.Text, out tedad_anbar))
            {
                if (tedad_input > tedad_anbar)
                {
                    MessageBox.Show("تعداد نمیتواند بیشتر از موجودی انبار باشد!");
                    textBox3.Text = "";
                }
               
            }
           
        }
    }
}
