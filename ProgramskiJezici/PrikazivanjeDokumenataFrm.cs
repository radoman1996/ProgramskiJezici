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

namespace ProgramskiJezici
{
    public partial class PrikazivanjeDokumenataFrm : Form
    {
        #region PODACI

        PomocnaKlasa pomocna = new PomocnaKlasa();

        int id;
        String username;
        String imeKolekcije;
        int zakljucano;
        SqlConnection konekcija;
        RadSaFajlovima radSaFajlovima = new RadSaFajlovima();

        #endregion

        public PrikazivanjeDokumenataFrm(int idKolekcije, String user, String ime, int kljuc, SqlConnection conn)
        {
            InitializeComponent();
            this.id = idKolekcije;
            this.konekcija = conn;
            this.username = user;
            this.imeKolekcije = ime;
            this.zakljucano = kljuc;
        }

        private void PrikazivanjeDokumenataFrm_Load(object sender, EventArgs e)
        {
            prikaziDokumenta(this.id);
        }
        
        private void btnIzbrisiDokument_Click(object sender, EventArgs e)
        {
            if (zakljucano == 1)
            {
                DialogResult info = MessageBox.Show("Kolekcija je zakljucana. Brisanje dokumenata nije dozvoljeno", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridViewDokumenta.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete dokument za brisanje");
                return;
            }
            int broj = dataGridViewDokumenta.SelectedRows.Count;
            int brojac = 0;
            DialogResult dr = MessageBox.Show("Zelite li da izbrisete izabrana dokumenta?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                for (int i = 0; i < broj; i++)
                {
                    String fajl = dataGridViewDokumenta.SelectedRows[i].Cells["Naziv"].Value.ToString();
                    String tip = dataGridViewDokumenta.SelectedRows[i].Cells["Tip"].Value.ToString();
                    Console.WriteLine(fajl);
                    Console.WriteLine(tip);
                    Console.WriteLine(username);
                    Console.WriteLine(imeKolekcije);
                    if (brisiDokumenta(id, fajl) == 0)
                    {
                        radSaFajlovima.izbrisiFajl(fajl + "." + tip, username, imeKolekcije);
                        brojac += 1;
                    }
                }
                MessageBox.Show("Izbrisano " + brojac + " od " + broj + " dokumenata");
                int brDoc = pomocna.brojDokumenata(this.id, konekcija);
                if (brDoc == -1)
                {
                    return;
                }
                float velKB = pomocna.velicinaKolekcijeKB(this.id, konekcija);
                if (velKB == -1)
                {
                    return;
                }
                int konacno = pomocna.updateKolekcijeDokumentaIVelicina(this.id, brDoc, velKB, konekcija);
                if (konacno == 0)
                {
                    int datum = pomocna.updateDatumModifikovanja(this.id, this.konekcija);
                    if (datum == 0)
                    {
                        //MessageBox.Show("Uspjesno ste dodali fajlove u kolekciju");
                    }
                }
            }
            prikaziDokumenta(this.id);
        }

        #region METODE
        public void prikaziDokumenta(int idKolekcije)
        {
            DataTable table = new DataTable();
            String upit = "SELECT * FROM Dokument WHERE IdKolekcije=@idParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = konekcija;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = idKolekcije;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dataGridViewDokumenta.DataSource = table;
                dataGridViewDokumenta.Columns["IdKolekcije"].Visible = false;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska sa dokumentima " + err);
            }
        }

        public int brisiDokumenta(int idKolekcije, String naziv)
        {
            String upit = "DELETE FROM Dokument WHERE (IdKolekcije=@idParam AND Naziv=@nazivParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = konekcija;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("nazivParam", SqlDbType.VarChar);
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.Parameters["nazivParam"].Value = naziv;
                komanda.ExecuteNonQuery();
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri brisanju dokumenata " + err);
                return 1;
            }
        }
        #endregion

    }
}
