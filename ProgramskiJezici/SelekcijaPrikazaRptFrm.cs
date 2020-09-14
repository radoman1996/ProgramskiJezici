using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramskiJezici
{
    public partial class SelekcijaPrikazaRptFrm : Form
    {
        #region PODACI

        String jmbg;

        enum Izbor
        {
            Greska,
            OsnovnPodaci,
            Kolekcije,
            Dokumenta
        }

        private Izbor izbor;

        #endregion

        public SelekcijaPrikazaRptFrm(String jmbg)
        {
            InitializeComponent();
            this.jmbg = jmbg;
            izbor = Izbor.Greska;
        }

        private void SelekcijaPrikazaRptFrm_Load(object sender, EventArgs e)
        {

        }

        private void radioButtonOsnovniPodaci_CheckedChanged(object sender, EventArgs e)
        {
            izbor = Izbor.OsnovnPodaci;
        }

        private void radioButtonKolekcije_CheckedChanged(object sender, EventArgs e)
        {
            izbor = Izbor.Kolekcije;
        }

        private void radioButtonDokumenta_CheckedChanged(object sender, EventArgs e)
        {
            izbor = Izbor.Dokumenta;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (izbor == Izbor.Greska)
            {
                MessageBox.Show("Potrebno je da izaberete kriterijum");
                return;
            }
            String podatak = this.jmbg;
            if (izbor == Izbor.OsnovnPodaci)
            {
                this.Close();
                OsnovniPodaciFrmRpt forma = new OsnovniPodaciFrmRpt(podatak);
                forma.ShowDialog();
                return;
            }
            if (izbor == Izbor.Kolekcije)
            {
                this.Close();
                KolekcijeFrmRpt forma = new KolekcijeFrmRpt(podatak);
                forma.ShowDialog();
                return;
            }
            if (izbor == Izbor.Dokumenta)
            {
                this.Close();
                DokumentaFrmRpt forma = new DokumentaFrmRpt(podatak);
                forma.ShowDialog();
                return;
            }
        }

    }
}
