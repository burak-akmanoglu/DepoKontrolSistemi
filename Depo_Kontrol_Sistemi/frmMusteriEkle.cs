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
using System.Data.Sql;

namespace Depo_Kontrol_Sistemi
{

    public partial class frmMusteriEkle : Form
    {

        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");

        public frmMusteriEkle()
        {
            InitializeComponent();
        }
        bool durum;
        void mukerrer()
        {
            db.Open();
            SqlCommand komut = new SqlCommand("select * from Musteri where m_id='" + txtmid.Text + "' ", db);

            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            db.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mukerrer();
            if (durum == true)
            {
                db.Open();
                SqlCommand komut = new SqlCommand("Insert Into Musteri(m_id,adsoyad,telefon,adres,email) values(@m_id,@adsoyad,@telefon,@adres,@email)", db);
                komut.Parameters.AddWithValue("@m_id", txtmid.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtadsoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTel.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.Parameters.AddWithValue("@email", txtEmail.Text);

                komut.ExecuteNonQuery();

                MessageBox.Show("Müşteri Kaydı Eklendi");
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
                MessageBox.Show("Bu numarada Müşteri var");
            }


        }

        private void frmMusteriEkle_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtTel.Text = txtmid.Text;
        }
        private void txtTel_TextChanged(object sender, EventArgs e)
        {

            txtmid.Text = txtTel.Text;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void Lblcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
