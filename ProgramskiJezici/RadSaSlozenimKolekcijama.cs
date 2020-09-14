using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Ionic.Zip;

namespace ProgramskiJezici
{
    class RadSaSlozenimKolekcijama
    {

        PomocnaKlasa pomocna = new PomocnaKlasa();

        #region BAZA

        public bool sacuvajKolekciju(String ime, String jmbg, String tip, SqlConnection conn)
        {
            String upit = "INSERT INTO Kolekcija VALUES(@imeParam, @korisnikParam, @datumKreiranjaParam, @datumModifikovanjaParam, @tipParam, @brojParam, @velicinaParam, @childParam, @lockParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("korisnikParam", SqlDbType.VarChar);
                komanda.Parameters.Add("datumKreiranjaParam", SqlDbType.Date);
                komanda.Parameters.Add("datumModifikovanjaParam", SqlDbType.Date);
                komanda.Parameters.Add("tipParam", SqlDbType.VarChar);
                komanda.Parameters.Add("brojParam", SqlDbType.Int);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters.Add("childParam", SqlDbType.Int);
                komanda.Parameters.Add("lockParam", SqlDbType.Int);

                komanda.Parameters["imeParam"].Value = ime;
                komanda.Parameters["korisnikParam"].Value = jmbg;
                komanda.Parameters["datumKreiranjaParam"].Value = DateTime.Today;
                komanda.Parameters["datumModifikovanjaParam"].Value = DateTime.Today;
                komanda.Parameters["tipParam"].Value = tip;
                komanda.Parameters["brojParam"].Value = 0;
                komanda.Parameters["velicinaParam"].Value = 0;
                komanda.Parameters["childParam"].Value = 1;
                komanda.Parameters["lockParam"].Value = 0;

                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspjesno cuvanje kolekcije");
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska pri cuvanju kolekcije " + err);
                return false;
            }
        }

        #endregion

        #region FAJLOVI

        public void kreirajKolekciju(String naziv, String putanja, String user)
        {
            String path = putanja + naziv + "/";
            Console.WriteLine("Path je " + path);
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                MessageBox.Show("Postoji kolekcija sa istim imenom");
                return;
            }
            di.Create();
        }

        public void kopirajFolder(String putanja1, String putanja2, String putanja3, String naziv)
        {
            using(ZipFile zf = new ZipFile())
            {
                zf.AddDirectory(putanja1);
                zf.Save(putanja2 + naziv + ".zip");
            }
            using(ZipFile zf = ZipFile.Read(putanja2 + naziv + ".zip"))
            {
                foreach(ZipEntry ze in zf)
                {
                    ze.Extract(putanja3 + naziv);
                }
            }
            FileInfo fi = new FileInfo(putanja1 + ".zip");
            fi.Delete();
        }

        public int brojFajlova(DirectoryInfo di)
        {
            int ukupno = 0;
            FileInfo[] fi = di.GetFiles();
            ukupno += fi.Length;
            DirectoryInfo[] dirs = di.GetDirectories();
            foreach(DirectoryInfo dir in dirs)
            {
                ukupno += brojFajlova(dir);
            }
            return ukupno;
        }

        public float sizeDir(DirectoryInfo di)
        {
            float sumaT = 0;
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo file in fi)
            {
                sumaT += file.Length;
            }
            DirectoryInfo[] dir = di.GetDirectories();
            foreach (DirectoryInfo d in dir)
            {
                sumaT += sizeDir(d);
            }

            return sumaT;
        }

        public float[] sizeFile(String[]fajlovi, String putanja)
        {
            float[] podaci = new float[fajlovi.Length];
            for(int i = 0; i < podaci.Length; i++)
            {
                FileInfo fi = new FileInfo(putanja + fajlovi[i]);
                double d = fi.Length / 1024.0;
                float rez = (float)Math.Ceiling(d);
                podaci[i] = rez;
            }
            return podaci;
        }

        public String[] spisakFajlova(String putanja)
        {
            String[] curr = Directory.GetFiles(putanja);
            String[] konacno = new String[curr.Length];
            String[] temp;
            for (int i = 0; i < curr.Length; i++)
            {
                temp = curr[i].Split('/');
                konacno[i] = temp[temp.Length - 1];
            }
            return konacno;
        }

        public String[] spisakFoldera(String putanja)
        {
            String[] curr = Directory.GetDirectories(putanja);
            String[] konacno = new String[curr.Length];
            String[] temp;
            for (int i = 0; i < curr.Length; i++)
            {
                temp = curr[i].Split('/');
                konacno[i] = temp[temp.Length - 1];
            }
            return konacno;
        }

        public String novaPutanja(String putanja)
        {
            int pos = putanja.LastIndexOf('/');
            String temp = putanja.Substring(0, pos);
            pos = temp.LastIndexOf('/');
            String konacno = temp.Substring(0, pos + 1);
            return konacno;
        }

        public int izbrisiFajl(String putanja)
        {
            FileInfo fi = new FileInfo(putanja);
            try
            {
                fi.Delete();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public void izbrisiKolekciju(DirectoryInfo di)
        {
            FileInfo[] fi = di.GetFiles();
            foreach(FileInfo file in fi)
            {
                file.Delete();
            }
            DirectoryInfo[] dirs = di.GetDirectories();
            foreach(DirectoryInfo dir in dirs)
            {
                izbrisiKolekciju(dir);
                dir.Delete();
            }
        }

        public void kopirajFajl(String putanja1, String putanja2, String naziv)
        {
            FileInfo fi = new FileInfo(putanja1);
            if (File.Exists(putanja2 + naziv))
            {
                DialogResult dr = MessageBox.Show("Postoji fajl. Zelite li da zamjenite fajlove?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    File.Delete(putanja2 + naziv);
                    fi.CopyTo(putanja2 + naziv);
                }
            }
            else
            {
                fi.CopyTo(putanja2 + naziv);
            }
        }

        #endregion

        #region METODE

        public void updateGrid(DataGridView dgv, String putanja, int id, SqlConnection conn)
        {
            for(int i = 0; i < dgv.RowCount; i++)
            {
                String tip = dgv.Rows[i].Cells["Tip"].Value.ToString();
                if (tip == "xxx")
                {
                    String naziv = dgv.Rows[i].Cells["Ime"].Value.ToString();
                    DirectoryInfo di = new DirectoryInfo(putanja + naziv + "/");
                    int ukupno = brojFajlova(di);
                    float size1 = sizeDir(di);
                    double val = size1 / 1024.0;
                    float size = (float)Math.Ceiling(val);
                    dgv.Rows[i].Cells["BrojDokumenata"].Value = ukupno;
                    dgv.Rows[i].Cells["Velicina"].Value = size.ToString();
                    pomocna.updateKolekcijeDokumentaIVelicina(id, ukupno, size, conn);
                    pomocna.updateDatumModifikovanja(id, conn);
                }
            }
        }

        public void kreirajGridView(DataGridView dgv, String[] fajlovi, String[] folderi, String[] kolone, String putanja)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < kolone.Length; i++)
            {
                DataColumn kolona = new DataColumn(kolone[i]);
                table.Columns.Add(kolona);
            }
            for (int i = 0; i < folderi.Length; i++)
            {
                DataRow red = table.NewRow();
                DirectoryInfo di = new DirectoryInfo(putanja + folderi[i] + "/");
                int broj = brojFajlova(di);
                float velicina = sizeDir(di);
                red[0] = folderi[i];
                red[1] = broj;
                red[2] = velicina;
                red[3] = "Folder";
                table.Rows.Add(red);
            }
            float[] velicinaFajlova = sizeFile(fajlovi, putanja);
            for (int i = 0; i < fajlovi.Length; i++)
            {
                DataRow red = table.NewRow();
                red[0] = fajlovi[i];
                red[1] = 0;
                red[2] = velicinaFajlova[i];
                red[3] = "Fajl";
                table.Rows.Add(red);
            }
            dgv.DataSource = table;
        }

        #endregion

    }
}
