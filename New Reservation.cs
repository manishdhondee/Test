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
    public partial class New_Reservation : Form
    {
        public static string SetValueForText1 = "";
        public static string SetValueForText2 = "";
        

        public New_Reservation()
        {
            InitializeComponent();

            //LV properties
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Plate No", 55);
            listView1.Columns.Add("Make", 45);
            listView1.Columns.Add("Model", 45);
            listView1.Columns.Add("Colour", 45);
            listView1.Columns.Add("Transmission", 75);
            listView1.Columns.Add("Price", 60);
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            
            
                SetValueForText1 = txtPlateNo.Text;
                SetValueForText2 = txtPrice.Text;
                
           
            Confirm_Reservation frm = new Confirm_Reservation();
            frm.Show();
            this.Hide();
        }

        SqlConnection cn = new SqlConnection("Data Source=LAPTOP-U9ONR0HB\\SQLEXPRESS;Initial Catalog=CarRental;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            cn.Open();
            cmd.CommandText = "select * from CAR";
            cmd.Connection = cn;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lv = new ListViewItem(dr[0].ToString());
                lv.SubItems.Add(dr[1].ToString());
                lv.SubItems.Add(dr[2].ToString());
                lv.SubItems.Add(dr[3].ToString());
                lv.SubItems.Add(dr[4].ToString());
                listView1.Items.Add(lv);

            }

            cn.Close();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Reservations frm = new Reservations();
            frm.Show();
            this.Hide();
        }

       

        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                //fill the text boxes
                txtPlateNo.Text = item.SubItems[1].Text;
                txtMake.Text = item.SubItems[2].Text;
                txtModel.Text = item.SubItems[3].Text;
                txtColour.Text = item.SubItems[4].Text;
                txtTransmission.Text = item.SubItems[5].Text;
                txtPrice.Text = item.SubItems[6].Text;

            }


            return;


        }

        private void btnMinimize_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                Application.Exit();
        }
    }
}
