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
    public partial class FormMarka : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        bool durum;
        private void Markafiltre()
        {
            durum = true;
            con.Open();
            SqlCommand komut = new SqlCommand("select * from  Marka", con);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                if (comboBox1.Text == rd["Kategoriler"].ToString() && textBox1.Text == rd["Marka"].ToString() || comboBox1.Text == "" || textBox1.Text == "")
                {
                    durum = false;
                }

            }
            con.Close();
        }
        public FormMarka()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Markafiltre();
            if (durum == true)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert Into Marka(Kategoriler,Marka) values('" + comboBox1.Text + "','" + textBox1.Text + "') ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Marka Eklendi");

            }
            else
            {
                MessageBox.Show("Bu alanda Marka var ya da boş alan", "Uyarı");
            }
            textBox1.Text = "";
            comboBox1.Text = "";
        }
        private void getCategori()
        {
            con.Open();
            SqlCommand kmt = new SqlCommand("Select * from Kategori", con);
            SqlDataReader read = kmt.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["Kategoriler"].ToString());
            }
            con.Close();
        }
        private void FormMarka_Load(object sender, EventArgs e)
        {

            getCategori();
        }

        private void kategorifiltre()
        {
            durum = true;
            con.Open();
            SqlCommand komut = new SqlCommand("select * from  Kategori", con);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                if (txtCategoriAdd.Text == rd["Kategoriler"].ToString() || txtCategoriAdd.Text == "")
                {
                    durum = false;
                }

            }
            con.Close();
        }
        private void btn_Click(object sender, EventArgs e)
        {
          
            kategorifiltre();
            if (durum == true)
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Kategori(Kategoriler) values('" + txtCategoriAdd.Text + "') ", con);
              
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Kategori Eklendi");

            }
            else
            {
                MessageBox.Show("Bu adda Kategori var", "Uyarı");
            }
            textBox1.Text = "";
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

        private void FormMarka_MouseDown(object sender, MouseEventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            MouseX = MousePosition.X - this.Left;
            MouseY = MousePosition.Y - this.Top;

            timer1.Enabled = true;
        }

        private void FormMarka_MouseUp(object sender, MouseEventArgs e)
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

