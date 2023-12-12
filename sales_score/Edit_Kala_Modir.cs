using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sales_score
{
    
    public partial class Edit_Kala_Modir : Form
    {
        //انتقال اطلاعات بین دو فرم 
        private DataGridView dataGridView1; // اضافه کردن یک متغیر DataGridView
        private int rowIndex; // اضافه کردن یک متغیر برای نگهداری شماره ردیف
        public Edit_Kala_Modir(string productName, string productCode, string productType, string productPelak, string productPrice, string productPelak_Seri,
         string productColor, string productSSH, string productSTM, string productNumber, string productsanad, DateTime date, DataGridView dataGridView1, int rowIndex) : base()
        {
            InitializeComponent();

            this.dataGridView1 = dataGridView1; // تنظیم مقدار dataGridView1
            this.rowIndex = rowIndex; // تنظیم مقدار rowIndex
            // نصب اطلاعات در کنترل‌ها
            textBox1.Text = productName;
            comboBox1.SelectedItem = productType;
            textBox3.Text = productPelak;
            textBox10.Text = productNumber;
            textBox6.Text = productPrice;
            textBox2.Text = productPelak_Seri;
            textBox8.Text = productColor;
            textBox7.Text = productSSH;
            textBox4.Text = productSTM;
            

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";

            // چک کردن صحت تاریخ از دیتاگریدویو
            DateTime dateFromGrid;
            string dateString = dataGridView1.Rows[rowIndex].Cells["تاریخ_پلاک"].Value?.ToString();

            if (DateTime.TryParse(dateString, out dateFromGrid))
            {
                dateTimePicker1.Value = dateFromGrid;
            }
            else
            {
                // در صورتی که تبدیل موفقیت‌آمیز نباشد، می‌توانید تاریخ پیشفرض یا مقدار دیگری را تنظیم کنید.
                dateTimePicker1.Value = DateTime.Now;
            }

            textBox9.Text = productsanad;
        }

        public Edit_Kala_Modir(string productCode, string productName, string productType, string productPelak, string productPrice,
            string productPelak_Seri, string productColor, string productSSH, string productSTM, string productNumber, DateTime date, DataGridView dataGridView1, int rowIndex)
        {
            this.productCode = productCode;
            this.productName = productName;
            this.productType = productType;
            this.productPelak = productPelak;
            this.productPrice = productPrice;
            this.productPelak_Seri = productPelak_Seri;
            this.productColor = productColor;
            this.productSSH = productSSH;
            this.productSTM = productSTM;
            this.productNumber = productNumber;
            this.date = date;
            this.dataGridView1 = dataGridView1;
            this.rowIndex = rowIndex;

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            // اعمال تغییرات در اطلاعات
            string updatedName = textBox1.Text;
            string updatedkalatype = comboBox1.Text;
            string updateTedad = textBox10.Text;
            string updatePelak = textBox3.Text;
            string Seriyal_pelak = textBox2.Text;
            string Color = textBox8.Text;
            string Price = textBox6.Text;
            string Shasi = textBox7.Text;
            string Taneh_motor = textBox4.Text;
            string Sand = textBox9.Text;
            string Tarikh = dateTimePicker1.Text;
            // آپدیت کردن سطر مربوطه در dataGridView
            DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
            selectedRow.Cells["نام_محصول"].Value = updatedName;
            selectedRow.Cells["نوع_کالا"].Value = updatedkalatype;
            selectedRow.Cells["تعداد"].Value = updateTedad;
            selectedRow.Cells["پلاک"].Value = updatePelak;
            selectedRow.Cells["سریال_پلاک"].Value = Seriyal_pelak;
            selectedRow.Cells["رنگ"].Value = Color;
            selectedRow.Cells["قیمت"].Value = Price;
            selectedRow.Cells["شماره_شاسی"].Value = Shasi;
            selectedRow.Cells["شماره_تنه_موتور"].Value = Taneh_motor;
            selectedRow.Cells["شماره_سند"].Value = Sand;
            selectedRow.Cells["تاریخ_پلاک"].Value = Tarikh;
            MessageBox.Show("اطلاعات ویرایش شد!");
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offset = e.Location;
            }
        }
        private Point offset;
       private string productCode;
        private string productName;
        private string productType;
        private string productPelak;
        private string productPrice;
        private string productPelak_Seri;
        private string productColor;
        private string productSSH;
        private string productSTM;
        private string productNumber;
        private DateTime date;

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
    }
}
