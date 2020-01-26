using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login_Form
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string username = txtUsername.Text;
            
            if (password=="password" && username=="username" )
            {
                Main_Menu frm = new Main_Menu();
                frm.Show();
                this.Hide();
            }
            else
            {
                if (MessageBox.Show("Wrong Password or Username", "", MessageBoxButtons.OK) == System.Windows.Forms.DialogResult.OK)
                {
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }

            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                Application.Exit();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

        }

        
    }
}
