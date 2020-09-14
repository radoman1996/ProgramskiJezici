namespace ProgramskiJezici
{
    partial class PridruziSeGrupiFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIDGrupe = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxImeGrupe = new System.Windows.Forms.TextBox();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnPridruziSe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID grupe";
            // 
            // textBoxIDGrupe
            // 
            this.textBoxIDGrupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIDGrupe.Location = new System.Drawing.Point(108, 61);
            this.textBoxIDGrupe.Name = "textBoxIDGrupe";
            this.textBoxIDGrupe.Size = new System.Drawing.Size(164, 26);
            this.textBoxIDGrupe.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ime grupe";
            // 
            // textBoxImeGrupe
            // 
            this.textBoxImeGrupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxImeGrupe.Location = new System.Drawing.Point(108, 23);
            this.textBoxImeGrupe.Name = "textBoxImeGrupe";
            this.textBoxImeGrupe.ReadOnly = true;
            this.textBoxImeGrupe.Size = new System.Drawing.Size(164, 26);
            this.textBoxImeGrupe.TabIndex = 5;
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIzlaz.Image = global::ProgramskiJezici.Properties.Resources.door_exit_join_icon__27;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(8, 105);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(129, 35);
            this.btnIzlaz.TabIndex = 3;
            this.btnIzlaz.Text = "Izađi";
            this.btnIzlaz.UseVisualStyleBackColor = true;
            this.btnIzlaz.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnPridruziSe
            // 
            this.btnPridruziSe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPridruziSe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPridruziSe.Image = global::ProgramskiJezici.Properties.Resources.add_group_512;
            this.btnPridruziSe.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPridruziSe.Location = new System.Drawing.Point(145, 105);
            this.btnPridruziSe.Name = "btnPridruziSe";
            this.btnPridruziSe.Size = new System.Drawing.Size(129, 35);
            this.btnPridruziSe.TabIndex = 2;
            this.btnPridruziSe.Text = "Pridruži se  ";
            this.btnPridruziSe.UseVisualStyleBackColor = true;
            this.btnPridruziSe.Click += new System.EventHandler(this.btnPridruziSe_Click);
            // 
            // PridruziSeGrupiFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(284, 162);
            this.Controls.Add(this.textBoxImeGrupe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.btnPridruziSe);
            this.Controls.Add(this.textBoxIDGrupe);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 201);
            this.MinimumSize = new System.Drawing.Size(300, 201);
            this.Name = "PridruziSeGrupiFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pridruži se grupi";
            this.Load += new System.EventHandler(this.PridruziSeGrupiFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIDGrupe;
        private System.Windows.Forms.Button btnPridruziSe;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxImeGrupe;
    }
}