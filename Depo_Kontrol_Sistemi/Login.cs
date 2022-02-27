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

namespace Depo_Kontrol_Sistemi
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        SqlDataReader dr;
        SqlCommand com;

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            db.Open();
 

            com = new SqlCommand();
          
            com.Connection = db;

            com.CommandText = "Select * from Kullanici where K_name='" + txtUserName.Text + "' and K_pasword='" + txtPassword.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {

                FormSatis Open = new FormSatis();
                this.Hide();
                Open.ShowDialog();
              
            }
            else
            {
                MessageBox.Show("Giriş Başarısız");
            }


  db.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            FrmAddUser ekle = new FrmAddUser();
          ekle.ShowDialog();
        }
    }
}
