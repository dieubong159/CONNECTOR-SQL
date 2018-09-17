using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connectify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            rdbtnWindows.Checked = true;
            txtServer.Text = "<local>";
        }

        private void rdbtnWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnWindows.Checked)
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            string strConnect = "";
            if (rdbtnWindows.Checked)
            {
                strConnect = "Server=localhost;Database=" + txtDatabase.Text + ";Trusted_Connection=True;";
            }
            else
            {
                strConnect = "Server=" + txtServer.Text + ";Database=" + txtDatabase.Text + ";User Id=" + txtUsername.Text + "; Password =" + txtPassword.Text + ";";
            }
            SqlConnection sqlconn = new SqlConnection(strConnect);
            try
            {
                if (txtDatabase.Text == "")
                {
                    MessageBox.Show("Vui long nhap ten database !!");
                    txtDatabase.Focus();
                }
                else
                {
                    Connectify.Properties.Settings.Default.ConnectionStr = strConnect;
                    Connectify.Properties.Settings.Default.Save();
                    sqlconn.Open();
                    MessageBox.Show("Login Succeed!");
                    Console.Beep();
                    this.Hide();
                    Form2 form = new Form2();
                    form.Show();
                }
            }
            catch
            {
                MessageBox.Show("Failed to connect ! Please try again !");
                sqlconn.Close();
            }
        }
    }
}
