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
    public partial class Connector : Form
    {
        public Connector()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            rdbtnWindows.Checked = true;
            txtPort.Enabled = false;
            DataTable datatable = SqlDataSourceEnumerator.Instance.GetDataSources();
            cbServer.DataSource = datatable;
            cbServer.DisplayMember = "ServerName";
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
                strConnect = "Server="+cbServer.Text+";Database=" + cbDatabase.Text + ";Trusted_Connection=True;";
            }
            else
            {
                strConnect = "Server=" + cbServer.Text +","+txtPort.Text+";Database=" + cbDatabase.Text + ";User Id=" + txtUsername.Text + "; Password =" + txtPassword.Text + ";";
            }
            SqlConnection sqlconn = new SqlConnection(strConnect);
            try
            {
                if (cbDatabase.Text == "")
                {
                    MessageBox.Show("Vui long chon database !!");
                    cbDatabase.Focus();
                }
                else
                {
                    Connectify.Properties.Settings.Default.ConnectionStr = strConnect;
                    Connectify.Properties.Settings.Default.Save();
                    //sqlconn.Open();
                    MessageBox.Show("Login Succeed!");
                    Console.Beep();
                    this.Hide();
                    RetrieveDATA form = new RetrieveDATA();
                    form.Show();
                }
            }
            catch
            {
                MessageBox.Show("Failed to connect ! Please try again !");
                sqlconn.Close();
            }
        }

        private void cbDatabase_DropDown(object sender, EventArgs e)
        {
            string strConnect = "";
            if (rdbtnWindows.Checked)
            {
                strConnect = "Server=" + cbServer.Text + ";Trusted_Connection=True;";
            }
            else
            {
                strConnect = "Server=" + cbServer.Text + ","+txtPort.Text+";User Id=" + txtUsername.Text + "; Password =" + txtPassword.Text + ";";
            }
            try
            {
                SqlConnection sqlconn = new SqlConnection(strConnect);
                Connectify.Properties.Settings.Default.ConnectionStr = strConnect;
                Connectify.Properties.Settings.Default.Save();

                sqlconn.Open();
                cbDatabase.DataSource = DBConnect.GetDatabaseList();
            }catch
            {
                MessageBox.Show("No Server match found!! Please try again!");
            }
        }

        private void cbPort_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPort.Checked == false)
            {
                txtPort.ResetText();
                txtPort.Enabled = false;
            }
            else
            {
                txtPort.Enabled = true;
                txtPort.Text = "1433";
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            DialogResult ans = MessageBox.Show("Do you want to exit ??", "WARING!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ans == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
