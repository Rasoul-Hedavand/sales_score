using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace sales_score
{
    public partial class panel_Act_moshtari : Form
    {
        // تعریف شیء SqlConnection به عنوان یک متغیر جهانی
        private SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
        public object DataTable { get; internal set; }
        public panel_Act_moshtari()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // استفاده از دستور using برای حذف شیء SqlCommand
            using (SqlCommand cmd = new SqlCommand("select * from All_Customers", con))
            {
                // استفاده از دستور using برای حذف شیء SqlDataAdapter
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    // استفاده از دستور using برای حذف شیء DataTable
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        // بازنویسی متد OnFormClosing برای حذف شیء SqlConnection
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            con.Dispose();
        }

        private void customers_Load(object sender, EventArgs e)
        {
            UpdateDataGridView();
            if (dataGridView1.Columns.Count >= 10)
            {
                // تغییر ترتیب ستون‌ها برای نمایش دو ستون جدید  
                dataGridView1.Columns[1].DisplayIndex = 10;
                dataGridView1.Columns[0].DisplayIndex = 9;
            }
          
        }
        internal void UpdateDataGridView()
        {
            using (SqlCommand cmd1 = new SqlCommand("SELECT TOP 0 * FROM All_Customers", con))
            {
                // استفاده از دستور using برای حذف شیء SqlDataAdapter
                using (SqlDataAdapter da = new SqlDataAdapter(cmd1))
                {
                    // استفاده از دستور using برای حذف شیء DataTable
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }

        }
  
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton11_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files|";
            ofd.InitialDirectory = "d:\\";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filePath = ofd.FileName;

                if (filePath.EndsWith(".xls") || filePath.EndsWith(".xlsx"))
                {
                    string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @filePath + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    OleDbCommand cmd = new OleDbCommand("Select [PersonId],[کد ملی/کد اقتصادی],[نام / نام شرکت],[نام خانوادگی],[نام پدر/شماره ثبت],[نوع شخص/شرکت],[تاریخ تولد],[محل تولد],[وضعیت] from [Sheet1$]", excelConnection);
                    OleDbDataReader dr1;
                    dr1 = cmd.ExecuteReader();
                    DataTable data = new DataTable();
                    data.Load(dr1);
                    dataGridView1.DataSource = data;
                    excelConnection.Close();
                }
                else
                {


                }
            }
        }

        private void rjButton10_Click(object sender, EventArgs e)
        {
            sabt_moshtari frm = new sabt_moshtari();
            frm.ShowDialog();
        }

        private void rjButton9_Click(object sender, EventArgs e)
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


        private void rjButton8_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                // برای هر ردیف در DataGridView، اطلاعات را وارد جدول All_Customers وارد می‌کنیم
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string insertQuery = "INSERT INTO All_Customers (PersonId, [کد ملی/کد اقتصادی], " +
                            "[نام / نام شرکت], [نام خانوادگی], [نام پدر/شماره ثبت], " +
                            "[نوع شخص/شرکت], [تاریخ تولد], [محل تولد], وضعیت) " +
                            "VALUES (@PersonId, @NationalCode, @FullName, @LastName, " +
                            "@FatherNameOrRegistrationNumber, @PersonTypeOrCompany, " +
                            "@BirthDate, @BirthPlace, @Status)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@PersonId", Convert.ToInt64(row.Cells["PersonId"].Value));
                            cmd.Parameters.AddWithValue("@NationalCode", row.Cells["کد ملی/کد اقتصادی"].Value.ToString());
                            cmd.Parameters.AddWithValue("@FullName", row.Cells["نام / نام شرکت"].Value.ToString());
                            cmd.Parameters.AddWithValue("@LastName", row.Cells["نام خانوادگی"].Value.ToString());
                            cmd.Parameters.AddWithValue("@FatherNameOrRegistrationNumber", row.Cells["نام پدر/شماره ثبت"].Value.ToString());
                            cmd.Parameters.AddWithValue("@PersonTypeOrCompany", row.Cells["نوع شخص/شرکت"].Value.ToString());
                            cmd.Parameters.AddWithValue("@BirthDate", row.Cells["تاریخ تولد"].Value.ToString());
                            cmd.Parameters.AddWithValue("@BirthPlace", row.Cells["محل تولد"].Value.ToString());
                            //cmd.Parameters.AddWithValue("@Status", Convert.ToBoolean(row.Cells["وضعیت"].Value));
                            cmd.Parameters.AddWithValue("@Status", (object)row.Cells["وضعیت"].Value ?? DBNull.Value);
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                MessageBox.Show("اطلاعات با موفقیت وارد شد.");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // شماره خطای تکراری بودن کلید اصلی
                {
                    MessageBox.Show("کلید(های) اصلی تکراری وارد کرده اید!\nلطفاً مقادیر دیگری انتخاب کنید!.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("خطا در وارد کردن اطلاعات: " + ex.Message);
                }
            }

            finally
            {
                con.Close();
            }
        }
    }
}