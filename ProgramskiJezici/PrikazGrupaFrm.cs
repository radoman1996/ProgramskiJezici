using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;

namespace ProgramskiJezici
{
    public partial class PrikazGrupaFrm : Form
    {
        #region PODACI

        String imeKolekcije;

        String[] kolone = { "Ime", "BrojDokumenata", "Velicina", "Tip" };
        String trenutnaPutanja = "";

        RadSaSlozenimKolekcijama rad = new RadSaSlozenimKolekcijama();

        #endregion

        public String GetPath()
        {
            return this.trenutnaPutanja;
        }

        public void SetPath(String s)
        {
            this.trenutnaPutanja += s;
        }

        public PrikazGrupaFrm(String imeKolekcije)
        {
            InitializeComponent();
            this.imeKolekcije = imeKolekcije;
        }

        private void PrikazGrupaFrm_Load(object sender, EventArgs e)
        {
            String putanja = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/";
            String[] spisakFoldera = rad.spisakFoldera(putanja);
            String[] spisakFajlova = rad.spisakFajlova(putanja);
            rad.kreirajGridView(dataGridViewSpisak, spisakFajlova, spisakFoldera, this.kolone, putanja);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            String temp = GetPath();
            if (temp == "")
            {
                return;
            }
            String s = rad.novaPutanja(temp);
            this.trenutnaPutanja = "";
            SetPath(s);
            String curr = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + temp;
            String putanja = rad.novaPutanja(curr);
            String[] spisakFajlova = rad.spisakFajlova(putanja);
            String[] spisakFoldera = rad.spisakFoldera(putanja);
            rad.kreirajGridView(dataGridViewSpisak, spisakFajlova, spisakFoldera, this.kolone, putanja);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpisak.SelectedRows.Count != 1)
            {
                MessageBox.Show("Potrebno je da izaberete jedan podatak");
                return;
            }
            String tip = dataGridViewSpisak.SelectedRows[0].Cells["Tip"].Value.ToString();
            if (tip == "Fajl")
            {
                MessageBox.Show("Izabrali ste fajl. Nije moguce prikazati strukturu fajla");
                return;
            }
            String imeFoldera = dataGridViewSpisak.SelectedRows[0].Cells["Ime"].Value.ToString();
            String pocetnaPutanja = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/";
            String path = imeFoldera + "/";
            SetPath(path);
            String temp = GetPath();
            String putanja = pocetnaPutanja + temp;
            String[] spisakFajlova = rad.spisakFajlova(putanja);
            String[] spisakFoldera = rad.spisakFoldera(putanja);
            rad.kreirajGridView(dataGridViewSpisak, spisakFajlova, spisakFoldera, this.kolone, putanja);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpisak.SelectedRows.Count > 1)
            {
                MessageBox.Show("Mozete izabrati najvise jedan red");
                return;
            }
            if (dataGridViewSpisak.SelectedRows.Count == 0)
            {
                openFileDialog1.Filter = "PDF(*.pdf)|*.pdf|Text files(*.txt)|*.txt|All files(*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String[] putanje = openFileDialog1.FileNames;
                    String[] nazivi = openFileDialog1.SafeFileNames;
                    String destPath = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath();
                    for (int i = 0; i < putanje.Length; i++)
                    {
                        rad.kopirajFajl(putanje[i], destPath, nazivi[i]);
                    }
                }
            }
            if (dataGridViewSpisak.SelectedRows.Count == 1)
            {
                String tip = dataGridViewSpisak.SelectedRows[0].Cells["Tip"].Value.ToString();
                if (tip == "Fajl")
                {
                    MessageBox.Show("Izaberite folder");
                    return;
                }
                String ime = dataGridViewSpisak.SelectedRows[0].Cells["Ime"].Value.ToString();
                openFileDialog1.Filter = "PDF(*.pdf)|*.pdf|Text files(*.txt)|*.txt|All files(*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String[] putanje = openFileDialog1.FileNames;
                    String[] nazivi = openFileDialog1.SafeFileNames;
                    String destPath = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath() + ime + "/";
                    for (int i = 0; i < putanje.Length; i++)
                    {
                        rad.kopirajFajl(putanje[i], destPath, nazivi[i]);
                    }
                }
            }
            String putanja2 = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath();
            String[] spisakFajlova = rad.spisakFajlova(putanja2);
            String[] spisakFoldera = rad.spisakFoldera(putanja2);
            rad.kreirajGridView(dataGridViewSpisak, spisakFajlova, spisakFoldera, this.kolone, putanja2);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpisak.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekcije koje brisete");
                return;
            }
            for (int i = 0; i < dataGridViewSpisak.SelectedRows.Count; i++)
            {
                String tip = dataGridViewSpisak.SelectedRows[i].Cells["Tip"].Value.ToString();
                String naziv = dataGridViewSpisak.SelectedRows[i].Cells["Ime"].Value.ToString();
                String putanja = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath() + naziv;
                if (tip == "Fajl")
                {
                    int rez = rad.izbrisiFajl(putanja);
                    if (rez == 1)
                    {
                        MessageBox.Show("Nije moguce izbrisati fajl " + naziv);
                    }
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(putanja + "/");
                    rad.izbrisiKolekciju(di);
                    di.Delete();
                }
            }
            String putanja2 = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath();
            String[] spisakFajlova = rad.spisakFajlova(putanja2);
            String[] spisakFoldera = rad.spisakFoldera(putanja2);
            rad.kreirajGridView(dataGridViewSpisak, spisakFajlova, spisakFoldera, this.kolone, putanja2);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Zip file(*.zip)|*.zip";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String putanjazipFajla = openFileDialog1.FileName;
                String naziv = openFileDialog1.SafeFileName;
                int pos = naziv.LastIndexOf('.');
                String nazivkolekcije = naziv.Substring(0, pos);
                String putanja = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath() + nazivkolekcije + "/";
                if (Directory.Exists(putanja))
                {
                    MessageBox.Show("Postoji kolekcija sa istim imenom. Nije moguce izvrsiti import");
                    return;
                }
                using (ZipFile zf = ZipFile.Read(putanjazipFajla))
                {
                    foreach (ZipEntry ze in zf)
                    {
                        ze.Extract(putanja);
                    }
                }
                String putanja2 = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath();
                String[] spisakFajlova = rad.spisakFajlova(putanja2);
                String[] spisakFoldera = rad.spisakFoldera(putanja2);
                rad.kreirajGridView(dataGridViewSpisak, spisakFajlova, spisakFoldera, this.kolone, putanja2);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewSpisak.SelectedRows.Count != 1)
            {
                MessageBox.Show("Potrebno je da izaberete jedan red");
                return;
            }
            String ime = dataGridViewSpisak.SelectedRows[0].Cells["Ime"].Value.ToString();
            String putanja = "../../sve_kolekcije/grupe/" + this.imeKolekcije + "/" + GetPath() + ime + "/";
            using (ZipFile zf = new ZipFile())
            {
                zf.AddDirectory(putanja);
                saveFileDialog1.FileName = ime;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    zf.Save(saveFileDialog1.FileName + ".zip");
                }
            }
        }
    }
}
