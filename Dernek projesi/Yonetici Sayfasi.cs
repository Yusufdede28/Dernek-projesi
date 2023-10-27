using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dernek_projesi
{
    public partial class Yonetici_Sayfasi : Form
    {
        public Yonetici_Sayfasi()
        {
            InitializeComponent();
        }

        

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (kontrol == true)
            {
                this.Hide();
                Mevcut_üyeyi_düzenle frm = new Mevcut_üyeyi_düzenle();
                
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen bir üye seçiniz");
            }
        }

        private void Mevcut_Üye_Listesi_Load(object sender, EventArgs e)
        {
            üyeleriGöster();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        void üyeleriGöster()
        {
            OleDbConnection baglanti = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb");
            baglanti.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from Uyebilgileri order by ad", baglanti);
            adtr.Fill(ds,"Okunan veri");
            dataGridView1.DataSource = ds.Tables["Okunan veri"];
            baglanti.Close();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            // Kullanıcı giriş formunu gizle
            this.Hide();

            // mevcut_üye_listesi formunu göster

            Yeni_Uye_Ekleme frm = new Yeni_Uye_Ekleme();
            frm.FormClosed += (s, args) => this.Close();
            frm.Show();
        }

       
        string secili_uye;
        bool kontrol = false;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            secili_uye = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            kontrol= true; 
            Program.KimlikNo= dataGridView1.Rows[secili].Cells[0].Value.ToString();
            Program.Ad = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            Program.Soyad = dataGridView1.Rows[secili].Cells[2].Value.ToString();
            Program.Şehir = dataGridView1.Rows[secili].Cells[3].Value.ToString();
            Program.Yaş = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            Program.KanGrubu = dataGridView1.Rows[secili].Cells[5].Value.ToString();
            Program.AidatTL = dataGridView1.Rows[secili].Cells[6].Value.ToString();
            Program.Aktif= dataGridView1.Rows[secili].Cells[7].Value.ToString();
        }

        private void sil_btn_Click(object sender, EventArgs e)
        {
            if (kontrol == true)
            {
                OleDbConnection baglanti = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb");
                baglanti.Open();
                OleDbCommand sil = new OleDbCommand("delete from Uyebilgileri where [Kimlik No]=@KimlikNo", baglanti);
                sil.Parameters.AddWithValue("@KimlikNo", secili_uye);

                DialogResult onay = MessageBox.Show(secili_uye + " Kimlik numaralı kaydı silmek istediğinize emin misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (onay == DialogResult.Yes)
                {
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Silme işlemi başarılı");
                }
                baglanti.Close();
                üyeleriGöster();
            }
            else
            {
                MessageBox.Show("Lütfen bir kayıt seçin");
            }

           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb");
            baglanti.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * from Uyebilgileri where ad like '"+textBox1.Text+"%'order by ad", baglanti);
            adtr.Fill(ds, "Okunan veri");
            dataGridView1.DataSource = ds.Tables["Okunan veri"];
            baglanti.Close();
        }
    }
}
