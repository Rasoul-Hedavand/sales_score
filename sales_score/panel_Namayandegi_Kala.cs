using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sales_score
{
    public partial class panel_Namayandegi_Kala : Form
    {
       
        SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
        public object DataTable { get; internal set; }
      
        public panel_Namayandegi_Kala()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void rjButton1_Click(object sender, EventArgs e)
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
        private void rjButton2_Click(object sender, EventArgs e)
        {

        }
        private void rjButton3_Click(object sender, EventArgs e)
        {
            Sabt_Kala_Namayandegi form2 = new Sabt_Kala_Namayandegi(this);
            form2.ShowDialog();
        }
        private void ChangeHeaderColor()
        {

        }
     private void panel_Act_kala_Load(object sender, EventArgs e)
        { 
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
            using (SqlCommand cmd1 = new SqlCommand("SELECT TOP 0 * FROM Anbar_Kala_Input", con))
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
        internal void UpdateDataGridView1()
        {
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {

                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                string productName = selectedRow.Cells["نام_محصول"].Value.ToString();
                string productType = selectedRow.Cells["نوع_کالا"].Value.ToString();
                string productPelak = selectedRow.Cells["پلاک"].Value.ToString();
                string productNumber = selectedRow.Cells["تعداد"].Value.ToString();
                string productPrice = selectedRow.Cells["قیمت"].Value.ToString();
                string productPelak_Seri = selectedRow.Cells["سریال_پلاک"].Value.ToString();
                string productColor = selectedRow.Cells["رنگ"].Value.ToString();
                string productSSH = selectedRow.Cells["شماره_شاسی"].Value.ToString();
                string productSTM = selectedRow.Cells["شماره_تنه_موتور"].Value.ToString();
                
                DateTime date = DateTime.Parse(selectedRow.Cells["تاریخ_ثبت"].Value.ToString());
                Edite_kala_Namayandegi editeKalaForm = new Edite_kala_Namayandegi(productName, productType, productPelak,
                    productPrice, productPelak_Seri, productColor, productSSH,
                    productSTM, productNumber, date, dataGridView1, e.RowIndex);
                editeKalaForm.Show();

            }

            else if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                // بررسی خالی نبودن مقدار سلول متناظر با کد_محصول

                if (string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString()))
                {
                    DialogResult result = MessageBox.Show("سطر مربوطه هنوز در انبار محصولات ذخیره نشده است آیا مطمئن هستید که می‌خواهید آن را حذف کنید؟", "تایید حذف", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    else
                    {

                    }
                }
                else
                {
                    int rowIndex = e.RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                    DialogResult result = MessageBox.Show("آیا مطمئن هستید که می‌خواهید این مورد را انبار محصولات حذف کنید؟", "تایید حذف", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        string productID = selectedRow.Cells["نام_محصول"].Value.ToString();
                        con.Open();
                        string deleteQuery = "DELETE FROM Anbar_Kala_Input WHERE نام_محصول = @کد_محصول";
                        using (SqlCommand command = new SqlCommand(deleteQuery, con))
                        {
                            command.Parameters.AddWithValue("@کد_محصول", productID);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("مورد با موفقیت حذف شد.");
                                UpdateDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("خطا در حذف مورد.");
                            }
                        }
                        con.Close();

                    }
                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red; // تنظیم رنگ پس‌زمینه هدر به قرمز
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                // اینجا yourImageColumnIndex، شماره ستون تصویر در دیتاگریدویو می‌باشد
                Image img = (Image)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (img != null)
                {
                    // تغییر سایز تصویر به عنوان مثال به 100x100 پیکسل
                    int newSize = 21;
                    Bitmap newImage = new Bitmap(img, new Size(newSize, newSize));
                    e.Value = newImage;
                }
            }
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                // اینجا yourImageColumnIndex، شماره ستون تصویر در دیتاگریدویو می‌باشد
                Image img = (Image)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                if (img != null)
                {
                    // تغییر سایز تصویر به عنوان مثال به 100x100 پیکسل
                    int newSize = 21;
                    Bitmap newImage = new Bitmap(img, new Size(newSize, newSize));
                    e.Value = newImage;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.CellBounds);
                e.Handled = true;
                Font font = new Font("b nazanin", 10, FontStyle.Bold);
                e.Graphics.DrawString(dataGridView1.Columns[e.ColumnIndex].HeaderText, font, Brushes.White, e.CellBounds, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
        }

        private void rjButton1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void rjButton1_MouseEnter(object sender, EventArgs e)
        {


        }

        private void rjButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton4_Click(object sender, EventArgs e)
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

           private void rjButton5_Click(object sender, EventArgs e)
           {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                FunctionB(r, con);
            }
            MessageBox.Show("اطلاعات با موفقیت ثبت شد!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // دریافت اندیس سطر انتخاب شده
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }
        private void FunctionB(DataGridViewRow r, SqlConnection con)
        {
            try
            {
                con.Open();
                string insertCommand = "INSERT INTO Anbar_Kala_Input (نام_محصول, نوع_کالا, تعداد, پلاک, سریال_پلاک, رنگ, شماره_شاسی, قیمت, شماره_تنه_موتور, تاریخ_ثبت) VALUES ";
                List<SqlParameter> parameters = new List<SqlParameter>();
                // اضافه کردن یک عبارت به دستور INSERT با استفاده از پارامترهای مربوط به سطر
                insertCommand += $"(@name, @type, @quantity, @plate, @serial, @color, @chassis, @price, @engine, @date) ";
                parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50) { Value = r.Cells[3].Value });
                parameters.Add(new SqlParameter("@type", SqlDbType.NVarChar, 50) { Value = r.Cells[4].Value });
                parameters.Add(new SqlParameter("@quantity", SqlDbType.Int) { Value = r.Cells[5].Value });
                parameters.Add(new SqlParameter("@plate", SqlDbType.NVarChar, 50) { Value = r.Cells[6].Value });
                parameters.Add(new SqlParameter("@serial", SqlDbType.NVarChar, 50) { Value = r.Cells[7].Value });
                parameters.Add(new SqlParameter("@color", SqlDbType.NVarChar, 50) { Value = r.Cells[8].Value });
                parameters.Add(new SqlParameter("@chassis", SqlDbType.NVarChar, 50) { Value = r.Cells[9].Value });
                parameters.Add(new SqlParameter("@price", SqlDbType.BigInt) { Value = r.Cells[10].Value });
                parameters.Add(new SqlParameter("@engine", SqlDbType.NVarChar, 50) { Value = r.Cells[11].Value });
              
                parameters.Add(new SqlParameter("@date", SqlDbType.Date) { Value = r.Cells[12].Value });

                // حذف ویرگول اضافی از انتهای دستور INSERT
                insertCommand = insertCommand.TrimEnd(',', ' ');
                // ایجاد یک شیء SqlCommand با استفاده از دستور INSERT و اتصال
                using (SqlCommand insert = new SqlCommand(insertCommand, con))
                {
                    // اضافه کردن پارامترها به شیء SqlCommand
                    insert.Parameters.AddRange(parameters.ToArray());
                    insert.ExecuteNonQuery();
                 //   MessageBox.Show("اطلاعات با موفقیت وارد جدول Anbar_Kala_Input شدند.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // مدیریت خطا به دلخواه شما
                MessageBox.Show($"خطا در ثبت اطلاعات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // بستن اتصال در هر حالت
            }
        }
    }
}
