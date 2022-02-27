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
    public partial class FrmRepoMax : Form
    {
        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        public FrmRepoMax()
        {
            InitializeComponent();
        }
        bool durum;
        void mukerrer()
        {
            db.Open();
            SqlCommand komut = new SqlCommand("select * from Dolum where KategoriAd='"+comboBox1.Text+"' ", db);
          
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
        private void getCategori()
        {
            db.Open();
            SqlCommand kmt = new SqlCommand("Select * from Kategori", db);
            SqlDataReader read = kmt.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["Kategoriler"].ToString());
            }
            db.Close();
        }
        private void FrmRepoMax_Load(object sender, EventArgs e)
        {
            getCategori();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mukerrer();
            if (durum == true)
            {
                db.Open();
                SqlCommand komut = new SqlCommand("Insert into Dolum (KategoriAd,KategoriDepoMax) values(@KategoriAd,@KategoriDepoMax)", db);
                komut.Parameters.AddWithValue("@KategoriAd", comboBox1.Text);
                komut.Parameters.AddWithValue("@KategoriDepoMax", textBox1.Text);
                komut.ExecuteNonQuery();
                db.Close();
                MessageBox.Show("Kayıt başarılı");
            }
            else
            {
                MessageBox.Show("Bu kayıt zaten var");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        int MouseX, MouseY;

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            MouseX = MousePosition.X - this.Left;
            MouseY = MousePosition.Y - this.Top;

            timer1.Enabled = true;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;
        }
    }
}
