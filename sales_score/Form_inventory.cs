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
    public partial class Form_inventory : Form
    {
        public Form_inventory()
        {
            InitializeComponent();
        }
       
        private void productor_Load(object sender, EventArgs e)
        {

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }


    }
}
