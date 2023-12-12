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
    
    public partial class namayandegi : Form
    {
       
        public namayandegi()
        {
            InitializeComponent();        
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                // تنظیم رنگ پس‌زمینه عنوان ستون
                e.Graphics.FillRectangle(Brushes.Red, e.CellBounds);

                // تنظیم متن عنوان ستون به رنگ سفید و وسط چین
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(dataGridView1.Columns[e.ColumnIndex].HeaderText, new Font("b titr", 11, FontStyle.Bold), Brushes.White, e.CellBounds, sf);
                }
                e.Handled = true;
            }
        }
        private void ChangeHeaderColor()
        {
            // حذف پیشخوان CellPainting
            dataGridView1.CellPainting -= dataGridView1_CellPainting;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.DarkRed; // رنگ پس‌زمینه عنوان ستون
                dataGridView1.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                // رنگ متن عنوان ستون
            }
        }
        private void namayandegi_Load(object sender, EventArgs e)
        {
            ChangeHeaderColor();

            // مجدداً متصل کردن پیشخوان CellPainting به رویه سفارشی
            dataGridView1.CellPainting += dataGridView1_CellPainting;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            sabt_namayandegi form2 = new sabt_namayandegi();

            // نمایش فرم دوم به صورت مدال
            form2.ShowDialog();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
