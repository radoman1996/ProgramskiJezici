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
    public partial class SlozenaKolekcijaFrm : Form
    {

        #region PODACI

        PomocnaKlasa pomocna = new PomocnaKlasa();
        RadSaSlozenimKolekcijama rad = new RadSaSlozenimKolekcijama();

        String username;
        SqlConnection konekcija;
        String[] naziviKolekcija;
        DataGridView dgv;

        String pocetnaPutanja = "../../sve_kolekcije/";

        #endregion

        public SlozenaKolekcijaFrm(String username, String[]nazivi, DataGridView dgv, SqlConnection conn)
        {
            InitializeComponent();
            this.username = username;
            this.konekcija = conn;
            this.naziviKolekcija = new String[nazivi.Length];
            for(int i = 0; i < nazivi.Length; i++)
            {
                this.naziviKolekcija[i] = nazivi[i];
            }
            this.dgv = dgv;
        }

        private void SlozenaKolekcijaFrm_Load(object sender, EventArgs e)
        {

        }

        private void btnSacuvajSK_Click(object sender, EventArgs e)
        {
            if (textBoxNazivSK.Text == "")
            {
                MessageBox.Show("Potrebno je da unesete naziv kolekcije");
                return;
            }
            String jmbg = pomocna.GetJmbgKorisnika(this.username, this.konekcija);
            int provjeraKolekcije = pomocna.postojiKolekcija(textBoxNazivSK.Text, jmbg, this.konekcija);
            if (provjeraKolekcije == 0)
            {
                if (rad.sacuvajKolekciju(textBoxNazivSK.Text, jmbg, "xxx", this.konekcija))
                {
                    String putanja = pocetnaPutanja + this.username + "/";
                    rad.kreirajKolekciju(textBoxNazivSK.Text, putanja, this.username);
                    for(int i = 0; i < naziviKolekcija.Length; i++)
                    {
                        String putanja1 = putanja + naziviKolekcija[i];
                        String putanja2 = putanja;
                        String putanja3 = putanja + textBoxNazivSK.Text + "/";
                        rad.kopirajFolder(putanja1, putanja2, putanja3, naziviKolekcija[i]);
                    }
                    pomocna.prikazMojihKolekcija(jmbg, this.dgv, this.konekcija);
                    int id = pomocna.GetIdKolekcije(jmbg, textBoxNazivSK.Text, this.konekcija);
                    rad.updateGrid(this.dgv, putanja, id, this.konekcija);
                    this.Close();
                }
            }
        }

        private void btnExitSK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
