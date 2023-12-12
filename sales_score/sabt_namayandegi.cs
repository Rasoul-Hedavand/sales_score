using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sales_score
{
    public partial class sabt_namayandegi : Form
    {
        public sabt_namayandegi()
        {
            InitializeComponent();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern bool ReleaseCapture();

        private void sabt_namayandegi_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           
        }
        private void comboBoxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCity.Items.Clear();
            comboBoxCity.Text = "";
            string selectedProvince = comboBoxProvince.SelectedItem.ToString();

            if (selectedProvince == "اردبیل")
            {
                comboBoxCity.Items.Add("اردبیل");
            }
            else if (selectedProvince == "اصفهان")
            {
                comboBoxCity.Items.Add("اصفهان3");
                comboBoxCity.Items.Add("هزارجریب");
                comboBoxCity.Items.Add("اصفهان مسجد سید(4)");
                comboBoxCity.Items.Add("دهق");
                comboBoxCity.Items.Add("سجاد اصفهان");
                comboBoxCity.Items.Add("اصفهان5");
                comboBoxCity.Items.Add("پاسارگاد اصفهان");
                comboBoxCity.Items.Add("اصفهان سروش");
            }
            else if (selectedProvince == "البرز")
            {
                comboBoxCity.Items.Add("کرج");
                comboBoxCity.Items.Add("مهرشهر");
                comboBoxCity.Items.Add("مارلیک");
            }
            if (selectedProvince == "ایلام")
            {
                comboBoxCity.Items.Add("ایلام");
            }
            else if (selectedProvince == "آذربایجان شرقی")
            {
                comboBoxCity.Items.Add("تبریز");
                comboBoxCity.Items.Add("مراغه");
                comboBoxCity.Items.Add("تبریز3");
                comboBoxCity.Items.Add("ارومیه");
            }
            else if (selectedProvince == "هرمزگان")
            {
                comboBoxCity.Items.Add("کیش");
                comboBoxCity.Items.Add("بندر عباس");
                comboBoxCity.Items.Add("میناب");
                comboBoxCity.Items.Add("قشم");
            }
            else if (selectedProvince == "بوشهر")
            {
                comboBoxCity.Items.Add("بوشهر");
                comboBoxCity.Items.Add("بوشهر جدید");
            }
            else if (selectedProvince == "تهران")
            {
                comboBoxCity.Items.Add("باملند");
                comboBoxCity.Items.Add("ستارخان");
                comboBoxCity.Items.Add("تهرانسر");
                comboBoxCity.Items.Add("17شهریور");
                comboBoxCity.Items.Add("مدرس");
                comboBoxCity.Items.Add("تهرانپارس");
                comboBoxCity.Items.Add("شهریار");
                comboBoxCity.Items.Add("شهرری");
                comboBoxCity.Items.Add("میدان امام حسین");
                comboBoxCity.Items.Add("دماوند");
                comboBoxCity.Items.Add("پروژه جردن");
                comboBoxCity.Items.Add("اسلامشهر");
                comboBoxCity.Items.Add("شهرقدس");
                comboBoxCity.Items.Add("خزانه");
                comboBoxCity.Items.Add("پاکدشت");
                comboBoxCity.Items.Add("ورامین");
                comboBoxCity.Items.Add("سعدی");
                comboBoxCity.Items.Add("پردیس");
                comboBoxCity.Items.Add("منیریه");
                comboBoxCity.Items.Add("امام خمینی");
                comboBoxCity.Items.Add("هنگام");
                comboBoxCity.Items.Add("افسریه");
                comboBoxCity.Items.Add("جردن");
                comboBoxCity.Items.Add("قرچک");
                comboBoxCity.Items.Add("شهر ری2");
                comboBoxCity.Items.Add("خانی آباد");
                comboBoxCity.Items.Add("پل رومی");
                comboBoxCity.Items.Add("تهرانپارس2");
                comboBoxCity.Items.Add("هفت حوض");
                comboBoxCity.Items.Add("مولوی");
                comboBoxCity.Items.Add("راه آهن");
                comboBoxCity.Items.Add("گیلاوند");
                comboBoxCity.Items.Add("ایرانمال");
            }
            else if (selectedProvince == "خراسان رضوی")
            {
                comboBoxCity.Items.Add("نیشابور");
                comboBoxCity.Items.Add("مشهد");
                comboBoxCity.Items.Add("مشهدطوس");
                comboBoxCity.Items.Add("سبزوار");
                comboBoxCity.Items.Add("تایباد");
                comboBoxCity.Items.Add("مشهد3");
                comboBoxCity.Items.Add("کاشمر");
                comboBoxCity.Items.Add("مشهد4");
                comboBoxCity.Items.Add("تربت حیدریه");
                comboBoxCity.Items.Add("مشهد4");
            }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {

        }

        private void sabt_namayandegi_Load(object sender, EventArgs e)
        {

        }

        private void sabt_namayandegi_MouseDown_1(object sender, MouseEventArgs e)
        {
           
        }
        private Point offset; // مختصات نسبی ماوس به کنترل panel1
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offset = e.Location; // ذخیره مختصات نسبی ماوس به کنترل panel1
            }
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

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
       
        }
    }
}
