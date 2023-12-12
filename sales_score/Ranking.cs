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
    public partial class Ranking : Form
    {
        SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
        public Ranking()
        {
            InitializeComponent();
        }
        private void Ranking_Load(object sender, EventArgs e)

        { 
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(50, 56, 65);
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Red;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(192, 0, 0);
            //کد زیر برای تغییر رنگ سرستون هاست
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //تغییر رنگ ستون کناری که قابلیت انتخاب سطر دارد
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            //وسط چین کردن محتویات دیتاگرید
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //وسط چین کردن سرتیرها
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns.Add("Count", "تعداد");
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["Count"].Value = e.RowIndex + 1;
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
            DataObject copydata = dataGridView1.GetClipboardContent();
            if (copydata != null)
            {
                Clipboard.SetDataObject(copydata);
                Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
                xlapp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook xlwbook;
                Microsoft.Office.Interop.Excel.Worksheet xlsheet;
                object missdata = System.Reflection.Missing.Value;
                xlwbook = xlapp.Workbooks.Add(missdata);
                xlsheet = xlwbook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
                Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
                xlr.Select();
                xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            }
        }
        private void button9_Click_3(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select PersonId,[ مشتری],[کد ملی/شماره اقتصادی],[نوع شخص/شرکت],[نحوه پرداخت],[خالص فاکتور],[شماره موبایل مشتری( جهت ارسال SMS)] " +
             "from [All_Customers] inner join [All_sales_1402] on [All_sales_1402].[PersonID_F_ key)]=[All_Customers].PersonId where"
                + " [خالص فاکتور] between " +
                "(SELECT TOP 1 max_buy_gold FROM levels_gold ORDER BY number DESC) and (SELECT TOP 1 min_buy_gold FROM levels_gold ORDER BY number DESC)", con);
            cmd.CommandTimeout = 250;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("SELECT TOP 1 Discount_gold FROM levels_gold ORDER BY number DESC", con);
            int discount = 0;
            discount = Convert.ToInt32(cmd2.ExecuteScalar());
            DataColumn newCol = new DataColumn("DiscountedValue", typeof(decimal)); // ساخت یک ستون جدید با نام DiscountedValue و نوع داده decimal
            newCol.Expression = "[خالص فاکتور] * " + discount + " / 100"; // تعریف فرمول برای محاسبه مقدار ستون جدید
            dt.Columns.Add(newCol); // اضافه کردن ستون جدید به DataTable
            DataColumn newCol2 = new DataColumn("Difference", typeof(decimal)); // ساخت یک ستون جدید با نام Difference و نوع داده decimal
            newCol2.Expression = "[خالص فاکتور] - [DiscountedValue]"; // تعریف فرمول برای محاسبه مقدار ستون جدید
            dt.Columns.Add(newCol2); // اضافه کردن ستون جدید به DataTable
            dataGridView1.DataSource = null; // حذف منبع داده قبلی DataGridView
            dataGridView1.DataSource = dt; // تعیین منبع داده جدید DataGridView
            dataGridView1.Columns["DiscountedValue"].HeaderText = "مقدار تخفیف"; // تغییر عنوان ستون جدید
            dataGridView1.Columns["DiscountedValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // تغییر هم ترازی اعداد به راست
            dataGridView1.Columns["Difference"].HeaderText = "قیمت با تخفیف"; // تغییر عنوان ستون جدید                                                            // dataGridView1.Columns["Difference"].DefaultCellStyle.Format = "N2"; // تغییر فرمت نمایش اعداد به دو رقم اعشار
            dataGridView1.Columns["Difference"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // تغییر هم ترازی اعداد به راست
            double sum = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum += Convert.ToDouble(row.Cells["خالص فاکتور"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label6.Text = sum.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب

            decimal splitnumber;
            splitnumber = long.Parse(label6.Text, System.Globalization.NumberStyles.Currency);
            label6.Text = splitnumber.ToString("#,#");
            //////////////////////////////////////////////////////////////////////////////
            double sum2 = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum2 += Convert.ToDouble(row.Cells["DiscountedValue"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label7.Text = sum2.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب
            decimal splitnumber2;
            splitnumber2 = decimal.Parse(label7.Text, System.Globalization.NumberStyles.Currency);
            label7.Text = splitnumber2.ToString("#,#");
            /////////////////////////////////////////////////////////////////////////////

            double sum3 = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum3 += Convert.ToDouble(row.Cells["Difference"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label10.Text = sum3.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب
            decimal splitnumber3;
            splitnumber3 = decimal.Parse(label10.Text, System.Globalization.NumberStyles.Currency);
            label10.Text = splitnumber3.ToString("#,#");
            con.Close();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select PersonId,[ مشتری],[کد ملی/شماره اقتصادی],[نوع شخص/شرکت],[نحوه پرداخت],[خالص فاکتور],[شماره موبایل مشتری( جهت ارسال SMS)] " +
             "from [All_Customers] inner join [All_sales_1402] on [All_sales_1402].[PersonID_F_ key)]=[All_Customers].PersonId where"
                + " [خالص فاکتور] between " +
                "(SELECT TOP 1 max_buy_silver FROM levels_silver ORDER BY number DESC) and (SELECT TOP 1 min_buy_silver FROM levels_silver ORDER BY number DESC)", con);

            cmd.CommandTimeout = 250;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("SELECT TOP 1 Discount_silver FROM levels_silver ORDER BY number DESC", con);
            int discount = 0;
            discount = Convert.ToInt32(cmd2.ExecuteScalar());
            DataColumn newCol = new DataColumn("DiscountedValue", typeof(decimal)); // ساخت یک ستون جدید با نام DiscountedValue و نوع داده decimal
            newCol.Expression = "[خالص فاکتور] * " + discount + " / 100"; // تعریف فرمول برای محاسبه مقدار ستون جدید
            dt.Columns.Add(newCol); // اضافه کردن ستون جدید به DataTable
            DataColumn newCol2 = new DataColumn("Difference", typeof(decimal)); // ساخت یک ستون جدید با نام Difference و نوع داده decimal
            newCol2.Expression = "[خالص فاکتور] - [DiscountedValue]"; // تعریف فرمول برای محاسبه مقدار ستون جدید
            dt.Columns.Add(newCol2); // اضافه کردن ستون جدید به DataTable
            dataGridView1.DataSource = null; // حذف منبع داده قبلی DataGridView
            dataGridView1.DataSource = dt; // تعیین منبع داده جدید DataGridView
            dataGridView1.Columns["DiscountedValue"].HeaderText = "مقدار تخفیف"; // تغییر عنوان ستون جدید
            dataGridView1.Columns["DiscountedValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // تغییر هم ترازی اعداد به راست
            dataGridView1.Columns["Difference"].HeaderText = "قیمت با تخفیف"; // تغییر عنوان ستون جدید                                                            // dataGridView1.Columns["Difference"].DefaultCellStyle.Format = "N2"; // تغییر فرمت نمایش اعداد به دو رقم اعشار
            dataGridView1.Columns["Difference"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // تغییر هم ترازی اعداد به راست
            double sum = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum += Convert.ToDouble(row.Cells["خالص فاکتور"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label6.Text = sum.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب
            decimal splitnumber;
            splitnumber = long.Parse(label6.Text, System.Globalization.NumberStyles.Currency);
            label6.Text = splitnumber.ToString("#,#");
            //////////////////////////////////////////////////////////////////////////////
            double sum2 = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum2 += Convert.ToDouble(row.Cells["DiscountedValue"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label7.Text = sum2.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب
            decimal splitnumber2;
            splitnumber2 = decimal.Parse(label7.Text, System.Globalization.NumberStyles.Currency);
            label7.Text = splitnumber2.ToString("#,#");
            /////////////////////////////////////////////////////////////////////////////

            double sum3 = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum3 += Convert.ToDouble(row.Cells["Difference"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label10.Text = sum3.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب
            decimal splitnumber3;
            splitnumber3 = decimal.Parse(label10.Text, System.Globalization.NumberStyles.Currency);
            label10.Text = splitnumber3.ToString("#,#");
            con.Close();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select PersonId,[ مشتری],[کد ملی/شماره اقتصادی],[نوع شخص/شرکت],[نحوه پرداخت],[خالص فاکتور],[شماره موبایل مشتری( جهت ارسال SMS)] " +
             "from [All_Customers] inner join [All_sales_1402] on [All_sales_1402].[PersonID_F_ key)]=[All_Customers].PersonId where"
                + " [خالص فاکتور] between " +
                "(SELECT TOP 1 max_buy_bronze FROM levels_bronze ORDER BY number DESC) and (SELECT TOP 1 min_buy_bronze FROM levels_bronze ORDER BY number DESC)", con);

            cmd.CommandTimeout = 250;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("SELECT TOP 1 Discount_bronze FROM levels_bronze ORDER BY number DESC", con);
            int discount = 0;
            discount = Convert.ToInt32(cmd2.ExecuteScalar());
            DataColumn newCol = new DataColumn("DiscountedValue", typeof(decimal)); // ساخت یک ستون جدید با نام DiscountedValue و نوع داده decimal
            newCol.Expression = "[خالص فاکتور] * " + discount + " / 100"; // تعریف فرمول برای محاسبه مقدار ستون جدید
            dt.Columns.Add(newCol); // اضافه کردن ستون جدید به DataTable
            DataColumn newCol2 = new DataColumn("Difference", typeof(decimal)); // ساخت یک ستون جدید با نام Difference و نوع داده decimal
            newCol2.Expression = "[خالص فاکتور] - [DiscountedValue]"; // تعریف فرمول برای محاسبه مقدار ستون جدید
            dt.Columns.Add(newCol2); // اضافه کردن ستون جدید به DataTable
            dataGridView1.DataSource = null; // حذف منبع داده قبلی DataGridView
            dataGridView1.DataSource = dt; // تعیین منبع داده جدید DataGridView
            dataGridView1.Columns["DiscountedValue"].HeaderText = "مقدار تخفیف"; // تغییر عنوان ستون جدید
            dataGridView1.Columns["DiscountedValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // تغییر هم ترازی اعداد به راست
            dataGridView1.Columns["Difference"].HeaderText = "قیمت با تخفیف"; // تغییر عنوان ستون جدید                                                            // dataGridView1.Columns["Difference"].DefaultCellStyle.Format = "N2"; // تغییر فرمت نمایش اعداد به دو رقم اعشار
            dataGridView1.Columns["Difference"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // تغییر هم ترازی اعداد به راست

            double sum = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum += Convert.ToDouble(row.Cells["خالص فاکتور"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label6.Text = sum.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب

            decimal splitnumber;
            splitnumber = long.Parse(label6.Text, System.Globalization.NumberStyles.Currency);
            label6.Text = splitnumber.ToString("#,#");
            //////////////////////////////////////////////////////////////////////////////
            double sum2 = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum2 += Convert.ToDouble(row.Cells["DiscountedValue"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label7.Text = sum2.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب
            decimal splitnumber2;
            splitnumber2 = decimal.Parse(label7.Text, System.Globalization.NumberStyles.Currency);
            label7.Text = splitnumber2.ToString("#,#");
            ////////////////////////////////////////////////////////////////////////////
            ///
            double sum3 = 0; // تعریف یک متغیر برای ذخیره جمع
            foreach (DataGridViewRow row in dataGridView1.Rows) // حلقه روی سطرها
            {
                sum3 += Convert.ToDouble(row.Cells["Difference"].Value); // اضافه کردن مقدار سلول به جمع
            }
            label10.Text = sum3.ToString(); // اختصاص دادن جمع به خاصیت Text برچسب
            decimal splitnumber3;
            splitnumber3 = decimal.Parse(label10.Text, System.Globalization.NumberStyles.Currency);
            label10.Text = splitnumber3.ToString("#,#");
            con.Close();
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            button9.BackColor = Color.Yellow;
            button9.ForeColor = Color.Black;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.BackColor = Color.Black;
            button9.ForeColor = Color.White;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
            DataObject copydata = dataGridView1.GetClipboardContent();
            if (copydata != null)
            {
                Clipboard.SetDataObject(copydata);
                Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
                xlapp.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook xlwbook;
                Microsoft.Office.Interop.Excel.Worksheet xlsheet;
                object missdata = System.Reflection.Missing.Value;
                xlwbook = xlapp.Workbooks.Add(missdata);
                xlsheet = xlwbook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
                Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
                xlr.Select();
                xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            }
        }
    } 
}
