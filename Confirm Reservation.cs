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
    public partial class Confirm_Reservation : Form
    {
        public Confirm_Reservation()
        {
            InitializeComponent();

            txtPlateNo.Text = New_Reservation.SetValueForText1;
            txtPrice.Text = New_Reservation.SetValueForText2;
            
        }

        private void btnCalculatePayment_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection("Data Source=localhost;Initial Catalog=LoginScreen;Integrated Security=True");
            SqlCommand com = new SqlCommand();
            com.Connection = sc;
            sc.Open();
            SqlDataReader read = (null);
            com.CommandText = ("select PRICE from CAR WHERE txtNIC.Text=NIC");
            read = com.ExecuteReader();
            txtPrice.Text = (read["PRICE"].ToString());
            sc.Close();


            double numdays = double.Parse(txtNumDays.Text);

            double price = double.Parse(txtPrice.Text);

            double payment = numdays * price;
            txtPayment.Text = payment.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are sure you want to cancel ? ", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                Main_Menu frm = new Main_Menu();
                frm.Show();
                this.Close();
            }
        }



        public String connStr = "Data Source=LAPTOP-xxxxxxxx\\SQLEXPRESS;Initial Catalog=CarRental;Integrated Security=True";
        private void btnConfirm_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(connStr);
            con.Open();

          

            if (MessageBox.Show("Do you want confirm the reservation?", "Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
             
                   if (MessageBox.Show("Reservation has been confirmed!", "Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                    {
                        Main_Menu form = new Main_Menu();
                        form.Show();
                        this.Close();
                    }
                                           
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                String q = "insert into RESERVATION(RESERVATIONID, NIC, PLATENO, STARTDATE, ENDDATE, PAYMENT)values('" + txtReservationID.Text + "','" + txtNIC.Text.ToString() + "','" + txtPlateNo.Text.ToString() + "','" + this.dateTimePicker1 + "','" + this.dateTimePicker2 + "','" + txtPayment.Text.ToString() + "')";
                SqlCommand cmd = new SqlCommand(q, con);
            }

        }

        

        private void btnBack_Click(object sender, EventArgs e)
        {
            New_Reservation frm = new New_Reservation();
            frm.Show();
            this.Close();
        }


      
        

        private void btnNumOfDays_Click(object sender, EventArgs e)
        {

            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            TimeSpan duration = end - start;
            int days = duration.Days;
            txtNumDays.Text = days.ToString();
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
