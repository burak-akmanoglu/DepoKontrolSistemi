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
    public partial class FormSatis : Form
    {
        public FormSatis()
        {

            InitializeComponent();
        }
        
        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        DataSet ds = new DataSet();

        private void SepetListele()
        {
            db.Open();
            SqlDataAdapter adr = new SqlDataAdapter("Select * from Sepet", db);
            adr.Fill(ds, "Sepet");
            dataGridView1.DataSource = ds.Tables["Sepet"];
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[1].Visible = false;
            //dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
            db.Close();

        }
        private void hesap()
        {
            try
            {
                db.Open();
                SqlCommand cmd = new SqlCommand("Select Sum(toplamFiyat) from Sepet", db);
                lblTotalPrice.Text = cmd.ExecuteScalar() + "TL";
                db.Close();
            }
            catch (Exception)
            {

                ;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            db.Open();
            SqlCommand komut = new SqlCommand("Delete from Sepet where Barkodno='" + dataGridView1.CurrentRow.Cells["Barkodno"].Value.ToString() + "'", db);
            komut.ExecuteNonQuery();
            db.Close();

            MessageBox.Show("Ürün sepetten çıkarıldı");
            ds.Tables["Sepet"].Clear();
            SepetListele();
            hesap();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmMusteriEkle ekle = new frmMusteriEkle();
            ekle.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmMusteriListecs ekle = new frmMusteriListecs();
            ekle.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormAddProduct ekleme = new FormAddProduct();
            ekleme.ShowDialog();
        }

     
        private void btnMarka_Click(object sender, EventArgs e)
        {
            FormMarka mrk = new FormMarka();
            mrk.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormProductList pl = new FormProductList();
            pl.Show();
        }

        private void Satis_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'depoDataSet.Satis' table. You can move, or remove it, as needed.
       
            // TODO: This line of code loads data into the 'depoDataSet1.Satis' table. You can move, or remove it, as needed.
 

            SepetListele();
            hesap();
        }

        private void txtM_id_TextChanged(object sender, EventArgs e)
        {
            if (txtM_id.Text == "")
            {
                txtAd.Text = "";
                txtTel.Text = "";
            }
            db.Open();
            SqlCommand cmd = new SqlCommand("select * from Musteri where m_id like '" + txtM_id.Text + "'", db);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                txtTel.Text = oku["telefon"].ToString();
                txtAd.Text = oku["adsoyad"].ToString();
            }
            db.Close();
        }

        private void txtBarkod_TextChanged(object sender, EventArgs e)
        {
            CleanContent();
            db.Open();
            SqlCommand cmd = new SqlCommand("select * from Urun where Barkodno like '" + txtBarkod.Text + "'", db);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                txtUrunAdi.Text = oku["Urun_Adi"].ToString();
                txtSatisFiyati.Text = oku["Satis_Fiyati"].ToString();
            }
            db.Close();
        }

        private void CleanContent()
        {
            if (txtBarkod.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtMiktari)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
        }
        bool durum;
        private void barkodnoControl()
        {
            durum = true;
            db.Open();
            SqlCommand komut = new SqlCommand("Select * from Sepet", db);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                if (txtBarkod.Text == rd["Barkodno"].ToString())
                {
                    durum = false;
                }
            }
            db.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            barkodnoControl();
            if (durum == true)
            {
                db.Open();
                SqlCommand cmd = new SqlCommand("Insert into Sepet(M_id,AdSoyad,Telefon,Barkodno,UrunAdi,Miktari,SatisFiyat,ToplamFiyat,Tarih) values(@M_id,@AdSoyad,@Telefon,@Barkodno,@UrunAdi,@Miktari,@SatisFiyat,@ToplamFiyat,@Tarih)", db);
                cmd.Parameters.AddWithValue("@M_id", txtM_id.Text);
                cmd.Parameters.AddWithValue("@AdSoyad", txtAd.Text);
                cmd.Parameters.AddWithValue("@Telefon", txtTel.Text);
                cmd.Parameters.AddWithValue("@Barkodno", txtBarkod.Text);
                cmd.Parameters.AddWithValue("@UrunAdi", txtUrunAdi.Text);
                cmd.Parameters.AddWithValue("@Miktari", int.Parse(txtMiktari.Text));
                cmd.Parameters.AddWithValue("@SatisFiyat", double.Parse(txtSatisFiyati.Text));
                cmd.Parameters.AddWithValue("@ToplamFiyat", double.Parse(txtToplamFiyat.Text));
                cmd.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();

                db.Close();
            }
            else
            {
                db.Open();
                SqlCommand cmd2 = new SqlCommand("Update Sepet set Miktari=Miktari+'" + int.Parse(txtMiktari.Text) + "' where Barkodno='" + txtBarkod.Text + "'", db);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("Update Sepet set ToplamFiyat=Miktari*SatisFiyat where Barkodno='" + txtBarkod.Text + "' ", db);
                cmd3.ExecuteNonQuery();
             
                db.Close();
            }
          
            txtMiktari.Text = "1";
            ds.Tables["Sepet"].Clear();
            SepetListele();
            hesap();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtMiktari)
                    {
                        item.Text = "";
                    }
                }

            }

        }

        private void txtMiktari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double v = (double.Parse(txtMiktari.Text) * double.Parse(txtSatisFiyati.Text));
                txtToplamFiyat.Text = v.ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void txtSatisFiyati_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double v = (double.Parse(txtMiktari.Text) * double.Parse(txtSatisFiyati.Text));
                txtToplamFiyat.Text = v.ToString();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void btnSatisiptal_Click(object sender, EventArgs e)
        {
            db.Open();
            SqlCommand komut = new SqlCommand("Delete from Sepet", db);
            komut.ExecuteNonQuery();
            db.Close();

            MessageBox.Show("Ürünler sepetten çıkarıldı");
            ds.Tables["Sepet"].Clear();

            SepetListele();
            hesap();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormSatisList listeleme = new FormSatisList();
            listeleme.ShowDialog();
        }

        private void BtnSatisYap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {

                db.Open();
                SqlCommand cmd = new SqlCommand("Insert into Satis(M_id,AdSoyad,Telefon,Barkodno,UrunAdi,Miktari,SatisFiyat,ToplamFiyat,Tarih) values(@M_id,@AdSoyad,@Telefon,@Barkodno,@UrunAdi,@Miktari,@SatisFiyat,@ToplamFiyat,@Tarih)", db);
                cmd.Parameters.AddWithValue("@M_id", txtM_id.Text);
                cmd.Parameters.AddWithValue("@AdSoyad", txtAd.Text);
                cmd.Parameters.AddWithValue("@Telefon", txtTel.Text);
                cmd.Parameters.AddWithValue("@Barkodno", dataGridView1.Rows[i].Cells["BarkodNo"].Value.ToString());
                cmd.Parameters.AddWithValue("@UrunAdi", dataGridView1.Rows[i].Cells["UrunAdi"].Value.ToString());
                cmd.Parameters.AddWithValue("@Miktari", int.Parse(dataGridView1.Rows[i].Cells["Miktari"].Value.ToString()));
                cmd.Parameters.AddWithValue("@SatisFiyat", double.Parse(dataGridView1.Rows[i].Cells["SatisFiyat"].Value.ToString()));
                cmd.Parameters.AddWithValue("@ToplamFiyat", double.Parse(dataGridView1.Rows[i].Cells["ToplamFiyat"].Value.ToString()));
                cmd.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
                
                SqlCommand komut2 = new SqlCommand("Update Urun set Miktari=Miktari-'" + int.Parse(dataGridView1.Rows[i].Cells["Miktari"].Value.ToString()) + "' where Barkodno='" + dataGridView1.Rows[i].Cells["Barkodno"].Value.ToString() + "'",db);
                komut2.ExecuteNonQuery();
            
                db.Close();
            }
            db.Open();
            SqlCommand komut = new SqlCommand("Delete from Sepet", db);
            komut.ExecuteNonQuery();
            db.Close();
            ds.Tables["Sepet"].Clear();
            SepetListele();
            hesap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void btnBarkodoku_Click(object sender, EventArgs e)
        {
            FrmBarkod ekle = new FrmBarkod();
            ekle.Show();
        }

        private void btnKabul_Click(object sender, EventArgs e)
        {
            txtBarkod.Text = FrmBarkod.barkodnumara.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDetails ekle = new FrmDetails();
            ekle.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;
        }

    }
}
