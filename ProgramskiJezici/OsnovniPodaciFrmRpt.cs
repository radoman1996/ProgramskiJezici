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
    public partial class OsnovniPodaciFrmRpt : Form
    {

        String jmbg;

        public OsnovniPodaciFrmRpt(String jmbg)
        {
            InitializeComponent();
            this.jmbg = jmbg;
        }

        private void OsnovniPodaciFrmRpt_Load(object sender, EventArgs e)
        {
            if (this.jmbg == "")
            {
                OsnovniPodaciRpt opr = new OsnovniPodaciRpt();
                this.crystalReportViewer1.ReportSource = opr;
            }
            else
            {
                OsnovniPodaciKorisnikaRpt opkr = new OsnovniPodaciKorisnikaRpt();
                opkr.SetParameterValue("jmbg", this.jmbg);
                this.crystalReportViewer1.ReportSource = opkr;
            }
        }
    }
}
