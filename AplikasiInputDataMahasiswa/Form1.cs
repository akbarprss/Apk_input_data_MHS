using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiInputDataMahasiswa
{
    public partial class Form1 : Form
    {
        //deklarasi objwk collection
        private List<Mahasiswa> list = new List<Mahasiswa>();

        //constructor
        public Form1()
        {
            InitializeComponent();
            InisialisasiListView();

           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            Mahasiswa mhs = new Mahasiswa();

            mhs.Nim = txtNim.Text;
            mhs.Nama = txtNama.Text;
            mhs.Kelas = txtKelas.Text;
            mhs.Nilai = int.Parse(txtNilai.Text);
            if (mhs.Nilai > 81)
            {
                mhs.Nilaihuruf = "A";
            } else if (mhs.Nilai > 61 )
            {
                mhs.Nilaihuruf = "B";
            } else if (mhs.Nilai > 41)
            {
                mhs.Nilaihuruf = "C";
            }else if (mhs.Nilai >21)
            {
                mhs.Nilaihuruf = "D";
            }else if (mhs.Nilai <21)
            {
                mhs.Nilaihuruf = "E";
            }
            
            //tambahkan objek mahasiswa ke collection
            list.Add(mhs);

            var msg = "Data Mahasiswa berhasil disimpan.";

            //tampilkan dialog informasi
            MessageBox.Show(msg, "informasi", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            //reset dari input
            ResetForm();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void lvwMahasiswa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;
            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nim", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Kelas", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai Huruf", 70, HorizontalAlignment.Center);
        }

        private void ResetForm()
        {
            txtNim.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            txtNilai.Text= "0";
            
            txtNim.Focus();
        }


        private void TampilkanData()
        {
            lvwMahasiswa.Items.Clear();

            foreach ( var mhs in list)
            {
                var noUrut = lvwMahasiswa.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.Nim);
                item.SubItems.Add(mhs.Nama);
                item.SubItems.Add(mhs.Kelas);
                item.SubItems.Add(mhs.Nilai.ToString());
                item.SubItems.Add(mhs.Nilaihuruf);

                lvwMahasiswa.Items.Add(item);
            }
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data Mahasiswa ingin dihapus?", "Komfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (konfirmasi == DialogResult.Yes)
                {
                    //ambil index yang dipilih
                    var index = lvwMahasiswa.SelectedIndices[0];

                    //hapus objek mahasiswa dari list
                    list.RemoveAt(index);

                    //refresh tampilan listview
                    TampilkanData();

                }
            }
            else //data belum dipilih
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private bool NumericOnly(KeyPressEventArgs e)
        {
            var strValid = "0123456789";
            if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // inputan selain angka
                if (strValid.IndexOf(e.KeyChar) < 0)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private void txtNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumericOnly(e);
        }
    }
}
