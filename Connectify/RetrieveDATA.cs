using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connectify
{
    public partial class RetrieveDATA : Form
    {
        public RetrieveDATA()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cbTables.DataSource = DBConnect.ListTables();
        }

        private void btnAccess_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = DBConnect.getData("SELECT * FROM " + cbTables.Text);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
