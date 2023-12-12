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

namespace sales_score
{
    public partial class Panel_foroush : Form
    {
        SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
        public object DataTable { get; internal set; }
        public Panel_foroush()
        {
            InitializeComponent();
        }
        private void rjButton7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void rjButton9_Click(object sender, EventArgs e)
        {
            Sabt_Foroush sabt_Foroush = new Sabt_Foroush(this);
            sabt_Foroush.ShowDialog();

        }

        private void Panel_foroush_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 5; // مقدار مورد نظر شما


            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            UpdateDataGridView();
            if (dataGridView1.Columns.Count >= 12)
            {
                // تغییر ترتیب ستون‌ها برای نمایش دو ستون جدید به اندیس‌های 12 و 13
                dataGridView1.Columns[1].DisplayIndex = 12;
                dataGridView1.Columns[0].DisplayIndex = 11;
            }
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        internal void UpdateDataGridView()
        {
            using (SqlCommand cmd1 = new SqlCommand("SELECT TOP 0 * FROM All_Sales", con))
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

        private void rjButton8_Click(object sender, EventArgs e)
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

        private void rjButton10_Click(object sender, EventArgs e)
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
                    OleDbCommand cmd = new OleDbCommand("Select [کد_محصول],[نام_محصول],[نوع_کالا],[تعداد],[پلاک],[سریال_پلاک],[رنگ],[شماره_شاسی],[قیمت],[شماره_تنه_موتور],[تاریخ_ثبت] from [Sheet1$]", excelConnection);
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
    }
}
