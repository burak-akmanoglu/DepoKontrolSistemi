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
    public partial class FrmDetails : Form
    {
        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        public FrmDetails()
        {
            InitializeComponent();
            pbar.Value = 0;
        }
        bool durum;
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
        private void getDolumdepo()
        {

        }

        private void FrmDetails_Load(object sender, EventArgs e)
        {

            getCategori();
            pbar.Value = 0;
            pbar.Text = pbar.Value.ToString() + "%";


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            label2.Text = "";
            db.Open();
            SqlCommand cmd = new SqlCommand("select Sum(Miktari) as a from Urun where Kategori='" + comboBox1.Text + "'", db);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                label1.Text = rd["a"].ToString();
            }
            db.Close();

            db.Open();
            SqlCommand kmt = new SqlCommand("Select * from Dolum where KategoriAd='" + comboBox1.Text + "'", db);
            SqlDataReader read = kmt.ExecuteReader();
            if (read.Read())
            {
                label2.Text = read["KategoriDepoMax"].ToString();

            }
            db.Close();
            if (label2.Text == "" || label2.Text == "0" || label1.Text=="" ||label1.Text=="0")
            {
                MessageBox.Show("Kategori hakkında bir girdi girilmemiş");
                pbar.Text = "0%";
            }
            else
            {
                var x = Convert.ToInt32(label2.Text);
                var y = Convert.ToInt32(label1.Text);
                pbar.Value = (100 * y) / x;

                pbar.Text = pbar.Value.ToString() + "%";
            }




        }
        int MouseX, MouseY;



        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            MouseX = MousePosition.X - this.Left;
            MouseY = MousePosition.Y - this.Top;

            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            FrmRepoMax ekle = new FrmRepoMax();
            ekle.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;
        }
    }
}
