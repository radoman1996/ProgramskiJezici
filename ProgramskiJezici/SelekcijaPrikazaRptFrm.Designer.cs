namespace ProgramskiJezici
{
    partial class SelekcijaPrikazaRptFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonOsnovniPodaci = new System.Windows.Forms.RadioButton();
            this.radioButtonKolekcije = new System.Windows.Forms.RadioButton();
            this.radioButtonDokumenta = new System.Windows.Forms.RadioButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonOsnovniPodaci
            // 
            this.radioButtonOsnovniPodaci.AutoSize = true;
            this.radioButtonOsnovniPodaci.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButtonOsnovniPodaci.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonOsnovniPodaci.Location = new System.Drawing.Point(65, 22);
            this.radioButtonOsnovniPodaci.Name = "radioButtonOsnovniPodaci";
            this.radioButtonOsnovniPodaci.Size = new System.Drawing.Size(149, 24);
            this.radioButtonOsnovniPodaci.TabIndex = 0;
            this.radioButtonOsnovniPodaci.TabStop = true;
            this.radioButtonOsnovniPodaci.Text = "Osnovni podaci";
            this.radioButtonOsnovniPodaci.UseVisualStyleBackColor = true;
            this.radioButtonOsnovniPodaci.CheckedChanged += new System.EventHandler(this.radioButtonOsnovniPodaci_CheckedChanged);
            // 
            // radioButtonKolekcije
            // 
            this.radioButtonKolekcije.AutoSize = true;
            this.radioButtonKolekcije.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButtonKolekcije.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonKolekcije.Location = new System.Drawing.Point(65, 55);
            this.radioButtonKolekcije.Name = "radioButtonKolekcije";
            this.radioButtonKolekcije.Size = new System.Drawing.Size(98, 24);
            this.radioButtonKolekcije.TabIndex = 1;
            this.radioButtonKolekcije.TabStop = true;
            this.radioButtonKolekcije.Text = "Kolekcije";
            this.radioButtonKolekcije.UseVisualStyleBackColor = true;
            this.radioButtonKolekcije.CheckedChanged += new System.EventHandler(this.radioButtonKolekcije_CheckedChanged);
            // 
            // radioButtonDokumenta
            // 
            this.radioButtonDokumenta.AutoSize = true;
            this.radioButtonDokumenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButtonDokumenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDokumenta.Location = new System.Drawing.Point(65, 88);
            this.radioButtonDokumenta.Name = "radioButtonDokumenta";
            this.radioButtonDokumenta.Size = new System.Drawing.Size(119, 24);
            this.radioButtonDokumenta.TabIndex = 2;
            this.radioButtonDokumenta.TabStop = true;
            this.radioButtonDokumenta.Text = "Dokumenta";
            this.radioButtonDokumenta.UseVisualStyleBackColor = true;
            this.radioButtonDokumenta.CheckedChanged += new System.EventHandler(this.radioButtonDokumenta_CheckedChanged);
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::ProgramskiJezici.Properties.Resources.next_512;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(145, 118);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(132, 33);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Dalje";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::ProgramskiJezici.Properties.Resources.door_exit_join_icon__27;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnExit.Location = new System.Drawing.Point(9, 118);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(132, 33);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Izađi";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // SelekcijaPrikazaRptFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(284, 166);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.radioButtonDokumenta);
            this.Controls.Add(this.radioButtonKolekcije);
            this.Controls.Add(this.radioButtonOsnovniPodaci);
            this.MaximumSize = new System.Drawing.Size(300, 205);
            this.MinimumSize = new System.Drawing.Size(300, 205);
            this.Name = "SelekcijaPrikazaRptFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selekcija prikaza";
            this.Load += new System.EventHandler(this.SelekcijaPrikazaRptFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonOsnovniPodaci;
        private System.Windows.Forms.RadioButton radioButtonKolekcije;
        private System.Windows.Forms.RadioButton radioButtonDokumenta;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnNext;
    }
}