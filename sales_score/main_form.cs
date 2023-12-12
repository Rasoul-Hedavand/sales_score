using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace sales_score
{
    public partial class main_form : Form
    {
  

        public object ShadowDirection { get; private set; }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.Red; // یا هر رنگ دیگری که میخواهید
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = Color.Blue; // یا هر رنگ دیگری که میخواهید
        }


        public main_form()
        {
            InitializeComponent();
            customizeDesign();
        }
        private void customizeDesign()
        {
            panelMediaSubMenu.Visible = false;
            panelMediaSubMenu2.Visible = false;
            panelMediaSubMenu3.Visible = false;
        }
        private void hideSubMenu()
        {
          if(panelMediaSubMenu.Visible==true)
                panelMediaSubMenu.Visible=false;
            if (panelMediaSubMenu2.Visible == true)
                panelMediaSubMenu2.Visible = false;
            if (panelMediaSubMenu3.Visible == true)
                panelMediaSubMenu3.Visible = false;
        }
        private void showSubMenu(Panel SubMenu)
        {
            if(SubMenu.Visible==false)
            {
                hideSubMenu();
                SubMenu.Visible=true;   
            }
            else 
                SubMenu.Visible=false;
        }
        // کد زیر برای درگ کردن فرم با موس است 
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern bool ReleaseCapture();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panelSideMenu.Width == 240)

                panelSideMenu.Width = 56;
            else
                panelSideMenu.Width = 240;
        }
        private void sabt_namayandegi_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        internal string GetTextBoxValue()
        {
            throw new NotImplementedException();
        }

        private void pictureBoxclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void abform(object m_mahsoul)
        {
            panel3.Controls.Clear();
            Form fh = m_mahsoul as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
        //    abform(new productor());
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void moshtari(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void Form_SabtAnbar(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }

        private void ranking(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void setting(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            btnMedia.FlatAppearance.BorderSize = 0; // حذف حاشیه دکمه
            btnMedia.FlatStyle = FlatStyle.Flat; // تنظیم نوع دکمه به Flat
            btnMedia.Padding = new Padding(20, 0, 0, 0); // تنظیم فاصله تصویر از مرز چپ دکمه
            button18.Click += new EventHandler(button_Click);
            button20.Click += new EventHandler(button_Click);
            button2.Click += new EventHandler(button_Click);
            button9.Click += new EventHandler(button_Click);
            button10.Click += new EventHandler(button_Click);
            button12.Click += new EventHandler(button_Click);
            button13.Click += new EventHandler(button_Click);
            button14.Click += new EventHandler(button_Click);
            button15.Click += new EventHandler(button_Click);
            button23.Click += new EventHandler(button_Click);
            button25.Click += new EventHandler(button_Click);
            button26.Click += new EventHandler(button_Click);
            button27.Click += new EventHandler(button_Click);
            button28.Click += new EventHandler(button_Click);
            button29.Click += new EventHandler(button_Click);
        }
        private Button selectedButton2; // تعریف متغیر در اینجا
        private void button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            // اگر دکمه جدیدی کلیک شده، رنگ دکمه قبلی به حالت اولیه باز می‌گردد
            if (selectedButton2 != null)
            {
                selectedButton2.BackColor = Color.FromArgb(64, 64, 64); ;
            }

            // سپس تغییر رنگ دکمه جدید به رنگ مورد نظر (مثلاً قرمز)
            clickedButton.BackColor = Color.Red;

            // ذخیره دکمه انتخاب شده
            selectedButton2 = clickedButton;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // label3.Text = DateTime.Now.ToLongTimeString();
            label3.Text = DateTime.Now.ToString("HH:mm:ss");

        }
        private void label3_Click_1(object sender, EventArgs e)
        {

        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel8_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.White;

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            //    panel8.BackColor = Color.White;

        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            // panel8.BackColor = Color.Blue;
            panel8.BackColor = Color.FromArgb(0, 175, 241);

        }

        private void panel19_MouseEnter(object sender, EventArgs e)
        {
            panel19.BackColor = Color.White;

        }

        private void panel19_MouseLeave(object sender, EventArgs e)
        {
            panel19.BackColor = Color.FromArgb(232, 87, 51);
        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel24_MouseEnter(object sender, EventArgs e)
        {
            panel24.BackColor = Color.White;
        }

        private void panel24_MouseLeave(object sender, EventArgs e)
        {
            panel24.BackColor = Color.FromArgb(161, 79, 147);

        }

        private void panel16_MouseEnter(object sender, EventArgs e)
        {
            panel16.BackColor = Color.White;

        }
        private void panel16_MouseLeave(object sender, EventArgs e)
        {
            panel16.BackColor = Color.FromArgb(0, 144, 86);
        }

        private void panel10_MouseEnter(object sender, EventArgs e)
        {
            panel10.BackColor = Color.White;
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {
            panel10.BackColor = Color.FromArgb(251, 156, 8);
        }

        private void panel22_MouseEnter(object sender, EventArgs e)
        {
            panel22.BackColor = Color.White;
        }

        private void panel22_MouseLeave(object sender, EventArgs e)
        {
            panel22.BackColor = Color.FromArgb(65, 193, 206);
        }


        private void menue1(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();

        }

        private void namayandegi(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private Point offset;
      

        private void main_form_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void main_form_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offset = e.Location; // ذخیره مختصات نسبی ماوس به کنترل panel1
            }
        }
        private void panel3_MouseMove(object sender, MouseEventArgs e)
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

 
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
            if (panelMediaSubMenu.Visible)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form_SabtAnbar(new panel_Namayandegi_Kala());

        }
        private void Form_SabtAnbar2(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void button19_Click(object sender, EventArgs e)
        {
           
        }
        private void Form_foroush(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void Panel_Modiriyat_kala(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void button20_Click(object sender, EventArgs e)
        {
            Panel_Modiriyat_kala(new Panel_Modiriyat_kala());

        }
        private void Form_foroush2(object formhijo)
        {

        }
        private void button17_Click(object sender, EventArgs e)
        {
          

        }

        private void button21_Click(object sender, EventArgs e)
        {
          
        }

        private void button22_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            menue1(new main_form());
        }
        private void button9_Click_1(object sender, EventArgs e)

        {

            Form_foroush(new Panel_foroush());
           
        }
        private void Form_inventory(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Form_inventory(new Form_inventory());
        }

        private void button24_Click(object sender, EventArgs e)
        {
            setting(new setting_form());

        }

        private void button11_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu2);
            if (panelMediaSubMenu2.Visible)
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
            }
            else
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu2);
            if (panelMediaSubMenu2.Visible)
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
            }
            else
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
            }
        }
        private void customers(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            customers(new panel_Act_moshtari());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            
        }

        private void button29_Click(object sender, EventArgs e)
        {
            moshtari(new namayandegi());
        }
        private void namayandegi2(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void button29_Click_1(object sender, EventArgs e)
        {
            namayandegi2(new namayandegi());
        }
        private void setting_form(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void settingBtn_Click(object sender, EventArgs e)
        {
            setting_form(new setting_form());
        }
        private void ranking2(object formhijo)
        {
            panel3.Controls.Clear();
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            ranking2(new Ranking());
        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelLogo1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
