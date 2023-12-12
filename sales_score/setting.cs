using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sales_score
{

    public partial class setting_form : Form
    {
        SqlConnection con = new SqlConnection("Data Source=CL-0331\\IT2023;Initial Catalog=kavir;Integrated Security=True");
        public setting_form()
        {
            InitializeComponent();
        }
        public static long max_reng_new;
        public static long min_reng_new;
        public static long discount_new;
        private void setting_form_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            
        }

        private void button8_Click(object sender, EventArgs e)
        {

            string max_buy_gold_text;
            string min_buy_gold_text;
            string max_buy_silver_text;
            string min_buy_silver_text;
            string max_buy_bronze_text;
            string min_buy_bronze_text;

            if (radioButton1.Checked)
            {
                

                SqlCommand cmd = new SqlCommand("insert into dbo.levels_gold(max_buy_gold, min_buy_gold, Discount_gold) values(@max_buy_gold, @min_buy_gold, @Discount_gold)", con);
                con.Open();
                // حذف جداکنندههای هزارگان (,) از رشته های ورودی
                max_buy_gold_text = textBox1.Text.Replace(",", "");
                min_buy_gold_text = textBox2.Text.Replace(",", "");
                if (long.Parse(max_buy_gold_text) < long.Parse(min_buy_gold_text))
                {
                    cmd.Parameters.AddWithValue("@max_buy_gold", long.Parse(max_buy_gold_text));
                    cmd.Parameters.AddWithValue("@min_buy_gold", long.Parse(min_buy_gold_text));
                    cmd.Parameters.AddWithValue("@Discount_gold", long.Parse(textBox3.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("sucessfuly!");
                }
                else
                {
                    MessageBox.Show("حداکثر مبلغ خرید نمی تواند از حداقل مبلغ خرید کوچکتر باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                }

            }
            

            else if (radioButton2.Checked)
            {
                SqlCommand cmd = new SqlCommand("insert into dbo.levels_silver(max_buy_silver, min_buy_silver, Discount_silver) values(@max_buy_silver, @min_buy_silver, @Discount_silver)", con);
                con.Open();
                // حذف جداکنندههای هزارگان (,) از رشته های ورودی 
                max_buy_silver_text = textBox1.Text.Replace(",", "");
                min_buy_silver_text = textBox2.Text.Replace(",", "");
                if (long.Parse(max_buy_silver_text) < long.Parse(min_buy_silver_text))
                {
                    cmd.Parameters.AddWithValue("@max_buy_silver", long.Parse(max_buy_silver_text));
                    cmd.Parameters.AddWithValue("@min_buy_silver", long.Parse(min_buy_silver_text));
                    cmd.Parameters.AddWithValue("@Discount_silver", long.Parse(textBox3.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("sucessfuly!");
                }
                else
                {
                    MessageBox.Show("حداکثر مبلغ خرید نمی تواند از حداقل مبلغ خرید کوچکتر باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                }
            }
            else if (radioButton3.Checked)
            { 
                SqlCommand cmd = new SqlCommand("insert into dbo.levels_bronze(max_buy_bronze, min_buy_bronze, Discount_bronze) values(@max_buy_bronze, @min_buy_bronze, @Discount_bronze)", con);
                con.Open();
                // حذف جداکنندههای هزارگان (,) از رشته های ورودی

                max_buy_bronze_text = textBox1.Text.Replace(",", "");
                min_buy_bronze_text = textBox2.Text.Replace(",", "");
                if (long.Parse(max_buy_bronze_text) < long.Parse(min_buy_bronze_text))
                {
                    cmd.Parameters.AddWithValue("@max_buy_bronze", long.Parse(max_buy_bronze_text));
                    cmd.Parameters.AddWithValue("@min_buy_bronze", long.Parse(min_buy_bronze_text));
                    cmd.Parameters.AddWithValue("@Discount_bronze", long.Parse(textBox3.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("sucessfuly!");
                }
                else
                {
                    MessageBox.Show("حداکثر مبلغ خرید نمی تواند از حداقل مبلغ خرید کوچکتر باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                }
            }
        } 

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (textBox2.Text!=""&&textBox2.Text!="0")
            {
                long price;
                price=long.Parse(textBox2.Text,System.Globalization.NumberStyles.Currency);
                textBox2.Text=price.ToString("#,#");
                textBox2.SelectionStart = textBox2.Text.Length;
                // به روز کردن مقدار max_reng_new با حذف ویرگول ها
                string min_reng2 = textBox2.Text;
                min_reng2 = min_reng2.Replace(",", "");
                min_reng_new = long.Parse(min_reng2);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

                if (textBox1.Text != "" && textBox1.Text != "0")
                {
                    decimal price;
                    price = long.Parse(textBox1.Text, System.Globalization.NumberStyles.Currency);
                    textBox1.Text = price.ToString("#,#");
                    textBox1.SelectionStart = textBox1.Text.Length;

                    // به روز کردن مقدار max_reng_new با حذف ویرگول ها
                    string max_reng2 = textBox1.Text;
                    max_reng2 = max_reng2.Replace(",", "");
                    max_reng_new = long.Parse(max_reng2);
                }

            }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
