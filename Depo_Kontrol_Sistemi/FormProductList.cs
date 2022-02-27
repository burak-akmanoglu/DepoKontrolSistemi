using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Depo_Kontrol_Sistemi
{
    public partial class FormProductList : Form
    {
        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True;MultipleActiveResultSets=True;");
        DataSet ds = new DataSet();
        private void getCategori()
        {
            db.Open();
            SqlCommand kmt = new SqlCommand("Select * from Kategori", db);
            SqlDataReader read = kmt.ExecuteReader();
            while (read.Read())
            {
                cmbKategori.Items.Add(read["Kategoriler"].ToString());
            }
            db.Close();
        }
        public FormProductList()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormProductList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'products.Urun' table. You can move, or remove it, as needed.
           
            // TODO: This line of code loads data into the 'depoDataSet3.Urun' table. You can move, or remove it, as needed.
       
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            // TODO: This line of code loads data into the 'depoDataSet1.Urun' table. You can move, or remove it, as needed.
            dataGridView1.Columns[0].Visible = false;
            UrunListele();
            getCategori();
        }

        private void UrunListele()
        {
            db.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from Urun ", db);
            sda.Fill(ds, "Urun");
            dataGridView1.DataSource = ds.Tables["Urun"];
            db.Close();
        }

        bool Depodurum;
        private void DolumUyarısı()
        {
            Depodurum = true;
            db.Open();
            SqlCommand komut = new SqlCommand("select Sum(Miktari) as totalmiktar from Urun where Kategori = '" + cmbKategori.Text + "'", db);
            SqlCommand cmd = new SqlCommand("select KategoriDepoMax from Dolum Where KategoriAd='" + cmbKategori.Text + "'", db);
            SqlDataReader rd = komut.ExecuteReader();
            SqlDataReader dr = cmd.ExecuteReader();
            if (rd.Read()&& dr.Read())
            {
                int totalmiktar2 = Convert.ToInt32(rd["totalmiktar"]);
                int miktar2 = Convert.ToInt32(V_txtMiktar.Text);
                int maxmiktar = Convert.ToInt32(dr["KategoriDepoMax"]);

                if (totalmiktar2 + miktar2 > maxmiktar)
                {
                    Depodurum = false;
                }

            }
            db.Close();
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DolumUyarısı();
            if (Depodurum == true)
            {


                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                SqlCommand komut = new SqlCommand("Update Urun set Urun_Adi=@Urun_Adi, Kategori=@Kategori,Marka=@Marka,Miktari=@Miktari,Alis_Fiyati=@Alis_Fiyati,Satis_Fiyati=@Satis_Fiyati where Barkodno=@Barkodno", db);
                if (V_txtBarkod.Text != "")
                {
                    komut.Parameters.AddWithValue("@Barkodno", V_txtBarkod.Text);
                    komut.Parameters.AddWithValue("@Urun_Adi", V_txtUrunAd.Text);
                    komut.Parameters.AddWithValue("@Miktari", int.Parse(V_txtMiktar.Text));
                    komut.Parameters.AddWithValue("@Alis_Fiyati", double.Parse(V_txtAlisFiyati.Text));
                    komut.Parameters.AddWithValue("@Satis_Fiyati", double.Parse(V_SatisFiyati.Text));

                    komut.Parameters.AddWithValue("@Kategori", cmbKategori.Text);
                    komut.Parameters.AddWithValue("@Marka", cmbMarka.Text);

                    //  MessageBox.Show("Barkod No Seçili Değil");


                    komut.ExecuteNonQuery();
                    if (db.State == ConnectionState.Open)
                    {
                        db.Close();
                    }

                    ds.Tables["Urun"].Clear();
                    UrunListele();
                    MessageBox.Show("Güncellendi");
                }
                else
                {
                    MessageBox.Show("Barkod No Seçili Değil");
                }
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {

                        item.Text = "";
                    }
                    if (item is ComboBox)
                    {

                        item.Text = "";
                    }

                }
            }
            else
            {
                MessageBox.Show("Depolama Alanı Bu Miktarı Karşılayamaz");
            }

        }
    
        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
            cmbMarka.Items.Clear();
            cmbMarka.Text = "";

            SqlCommand kmt = new SqlCommand("Select * from Marka where Kategoriler='" + cmbKategori.SelectedItem + "'", db);
            SqlDataReader read = kmt.ExecuteReader();
            while (read.Read())
            {
                cmbMarka.Items.Add(read["Marka"].ToString());
            }
            if (db.State == ConnectionState.Open)
            {
                db.Close();
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            db.Open();
            SqlCommand cmd = new SqlCommand("Delete from Urun where Barkodno = '" + dataGridView1.CurrentRow.Cells["Barkodno"].Value.ToString() + "'", db);
            cmd.ExecuteNonQuery();
            db.Close();
            ds.Tables["Urun"].Clear();
            UrunListele();
            MessageBox.Show("Urun Kaydı Silindi");
        }

        private void txtSearchBarkod_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();

            db.Open();
            SqlDataAdapter adt = new SqlDataAdapter("select * from Urun where Barkodno like '%" + txtSearchBarkod.Text + "%' ", db);
            adt.Fill(tablo);
            dataGridView1.DataSource = tablo;
            db.Close();
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
            FrmBarkod ekle = new FrmBarkod();
            ekle.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            txtSearchBarkod.Text = FrmBarkod.barkodnumara.ToString();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            V_txtBarkod.Text = dataGridView1.CurrentRow.Cells["Barkodno"].Value.ToString();
            cmbKategori.Text = dataGridView1.CurrentRow.Cells["Kategori"].Value.ToString();
            cmbMarka.Text = dataGridView1.CurrentRow.Cells["Marka"].Value.ToString();
            V_txtUrunAd.Text = dataGridView1.CurrentRow.Cells["Urun_Adi"].Value.ToString();
            V_txtMiktar.Text = dataGridView1.CurrentRow.Cells["Miktari"].Value.ToString();
            V_txtAlisFiyati.Text = dataGridView1.CurrentRow.Cells["Alis_Fiyati"].Value.ToString();
            V_SatisFiyati.Text = dataGridView1.CurrentRow.Cells["Satis_Fiyati"].Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;
        }
    }
}
