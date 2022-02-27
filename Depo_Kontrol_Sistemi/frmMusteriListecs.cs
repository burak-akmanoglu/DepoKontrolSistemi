using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Windows.Forms;

namespace Depo_Kontrol_Sistemi
{
    public partial class frmMusteriListecs : Form
    {
        public frmMusteriListecs()
        {
            InitializeComponent();
        }

        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        DataSet dst = new DataSet();

        private void frmMusteriListecs_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'depoDataSet2.Musteri' table. You can move, or remove it, as needed.
    

            // TODO: This line of code loads data into the 'depoDataSet1.Musteri' table. You can move, or remove it, as needed.
    
            Kayit();
        }

        private void Kayit()

        {
            db.Open();
            SqlDataAdapter dt = new SqlDataAdapter("select * from Musteri", db);
            dt.Fill(dst, "Musteri");
            dataGridView1.DataSource = dst.Tables["Musteri"];
            db.Close();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtadsoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtId.Text = dataGridView1.CurrentRow.Cells["m_id"].Value.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            db.Open();
            SqlCommand kmt = new SqlCommand("Update Musteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where m_id=@m_id", db);
            kmt.Parameters.AddWithValue("@adsoyad", txtadsoyad.Text);
            kmt.Parameters.AddWithValue("@telefon", txtTel.Text);
            kmt.Parameters.AddWithValue("@adres", txtAdres.Text);
            kmt.Parameters.AddWithValue("@email", txtEmail.Text);
            kmt.Parameters.AddWithValue("@m_id", txtId.Text);

            kmt.ExecuteNonQuery();
            db.Close();

            dst.Tables["Musteri"].Clear();
            Kayit();
            MessageBox.Show("Müşteri Kaydı Güncellendi");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            db.Open();
            SqlCommand cmd = new SqlCommand("Delete from Musteri where m_id = '" + dataGridView1.CurrentRow.Cells["m_id"].Value.ToString() + "'", db);
            cmd.ExecuteNonQuery();
            db.Close();
            dst.Tables["Musteri"].Clear();
            Kayit();
            MessageBox.Show("Müşteri Kaydı Silindi");
        }

        private void txtTelAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();

            db.Open();
            SqlDataAdapter adt = new SqlDataAdapter("select * from Musteri where m_id like '%" + txtTelAra.Text + "%' ", db);
            adt.Fill(tablo);
            dataGridView1.DataSource = tablo;
            db.Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int MouseX, MouseY;


        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            MouseX = MousePosition.X - this.Left;
            MouseY = MousePosition.Y - this.Top;

            timer1.Enabled = true;
        }



        private void panel2_MouseUp_1(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;
        }




    }
}
