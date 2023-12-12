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
    public partial class sabt_Kala_Modir : Form
    {
        private SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
        private Point offset;

        Panel_Modiriyat_kala y;
        public sabt_Kala_Modir(Panel_Modiriyat_kala z)
        {
            InitializeComponent();
            this.y = z;
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)y.dataGridView1.DataSource;

            DataRow newRow = dataTable.NewRow();
            newRow[1] = textBox1.Text; // نام ستون اول
            newRow[2] = comboBox1.Text; // نام ستون دوم
            newRow[3] = textBox10.Text; // نام ستون سوم
            newRow[4] = textBox3.Text;
            newRow[5] = textBox2.Text;
            newRow[6] = textBox8.Text;
            newRow[7] = textBox7.Text;
            newRow[8] = textBox6.Text;
            newRow[9] = textBox4.Text;
         
            newRow[10] = dateTimePicker1.Text;// و غیره برای ستون‌های بعدی
            dataTable.Rows.Add(newRow);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "یدکی" || comboBox1.Text == "جانبی")
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox7.Enabled = false;
                textBox2.BackColor = Color.FromArgb(206, 206, 206); // تغییر رنگ پس زمینه به خاکستری
                textBox3.BackColor = Color.FromArgb(206, 206, 206); // تغییر رنگ پس زمینه به خاکستری
                textBox4.BackColor = Color.FromArgb(206, 206, 206); // تغییر رنگ پس زمینه به خاکستری
                textBox7.BackColor = Color.FromArgb(206, 206, 206); // تغییر رنگ پس زمینه به خاکستری
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox7.Enabled = true;
                textBox1.BackColor = Color.White; // تغییر رنگ پس زمینه به خاکستری
                textBox2.BackColor = Color.White; // تغییر رنگ پس زمینه به خاکستری
                textBox3.BackColor = Color.White; // تغییر رنگ پس زمینه به خاکستری
                textBox4.BackColor = Color.White; // تغییر رنگ پس زمینه به خاکستری
                textBox7.BackColor = Color.White; // تغییر رنگ پس زمینه به خاکستری
            }
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
