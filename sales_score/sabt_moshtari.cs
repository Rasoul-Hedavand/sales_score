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
    public partial class sabt_moshtari : Form
    {
        private SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
      
        public sabt_moshtari()
        {
            InitializeComponent();
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        }
        private bool changingState = false;
      
        private Point offset;

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!changingState) 
            {
                changingState = true;
                checkBox1.Checked = !checkBox2.Checked; 
                changingState = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!changingState)
            {
                changingState = true;
                checkBox2.Checked = !checkBox1.Checked; 
                changingState = false;
            }
        }


        private void sabt_moshtari_Load(object sender, EventArgs e)
        {
           
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {

          
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
                offset = new Point(e.X, e.Y); // تعریف `offset` به عنوان یک Point
            }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
