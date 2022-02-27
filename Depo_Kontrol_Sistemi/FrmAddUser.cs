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
    public partial class FrmAddUser : Form
    {
        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        public FrmAddUser()
        {
            InitializeComponent();
        }

        private void FrmAddUser_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
        }

        private void Lblcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text==textBox3.Text)
            {
                db.Open();
                SqlCommand komut = new SqlCommand("Insert Into Kullanici(K_name,K_pasword) values(@K_name,@K_pasword)", db);
                komut.Parameters.AddWithValue("@K_name", textBox1.Text);
                komut.Parameters.AddWithValue("@K_pasword", textBox2.Text);
             

                komut.ExecuteNonQuery();

                MessageBox.Show("Kullanıcı Kaydı Eklendi");
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Şifre Tekrarları Aynı Değil");
            }
        }
    }
}
