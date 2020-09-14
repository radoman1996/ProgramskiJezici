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
    public partial class KreirajGrupuFrm : Form
    {
        #region PODACI

        SqlConnection konekcija;
        int idGrupe;
        String username;

        RadSaGrupama grupa = new RadSaGrupama();
        PomocnaKlasa pomocna = new PomocnaKlasa();

        #endregion

        public KreirajGrupuFrm(int idGrupe, String username, SqlConnection konekcija)
        {
            InitializeComponent();
            this.idGrupe = idGrupe;
            this.username = username;
            this.konekcija = konekcija;
        }

        private void KreirajGrupuFrm_Load(object sender, EventArgs e)
        {
            textBoxIDGrupe.Text = this.idGrupe.ToString();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            if (textBoxImeGrupe.Text == "")
            {
                MessageBox.Show("Polje Ime mora biti popunjeno");
                return;
            }
            String naziv = textBoxImeGrupe.Text;
            if (!grupa.ispravnoIme(naziv, this.konekcija))
            {
                MessageBox.Show("Postoji grupa sa istim imenom.\n" + "Unesite drugo ime");
                return;
            }
            grupa.sacuvajGrupu(this.idGrupe, naziv, this.konekcija); //cuvanje grupe
            String jmbg = pomocna.GetJmbgKorisnika(this.username, this.konekcija); //jmbg korisnika
            grupa.sacuvajGrupaUser(this.idGrupe, jmbg, this.konekcija); //cuvanje u grupa_user
            grupa.kreirajGrupuFolder(naziv);
            MessageBox.Show("Uspjesno kreiranje grupe");
            this.Close();
        }
    }
}
