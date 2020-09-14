namespace ProgramskiJezici
{
    partial class PrikazivanjeDokumenataFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewDokumenta = new System.Windows.Forms.DataGridView();
            this.btnIzbrisiDokument = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDokumenta)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDokumenta
            // 
            this.dataGridViewDokumenta.AllowUserToAddRows = false;
            this.dataGridViewDokumenta.AllowUserToDeleteRows = false;
            this.dataGridViewDokumenta.AllowUserToResizeColumns = false;
            this.dataGridViewDokumenta.AllowUserToResizeRows = false;
            this.dataGridViewDokumenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDokumenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDokumenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDokumenta.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDokumenta.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewDokumenta.Name = "dataGridViewDokumenta";
            this.dataGridViewDokumenta.Size = new System.Drawing.Size(526, 251);
            this.dataGridViewDokumenta.TabIndex = 0;
            // 
            // btnIzbrisiDokument
            // 
            this.btnIzbrisiDokument.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzbrisiDokument.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIzbrisiDokument.Image = global::ProgramskiJezici.Properties.Resources.delete_737_475058;
            this.btnIzbrisiDokument.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIzbrisiDokument.Location = new System.Drawing.Point(25, 274);
            this.btnIzbrisiDokument.Name = "btnIzbrisiDokument";
            this.btnIzbrisiDokument.Size = new System.Drawing.Size(161, 32);
            this.btnIzbrisiDokument.TabIndex = 1;
            this.btnIzbrisiDokument.Text = "Izbriši dokument";
            this.btnIzbrisiDokument.UseVisualStyleBackColor = true;
            this.btnIzbrisiDokument.Click += new System.EventHandler(this.btnIzbrisiDokument_Click);
            // 
            // PrikazivanjeDokumenataFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(526, 324);
            this.Controls.Add(this.btnIzbrisiDokument);
            this.Controls.Add(this.dataGridViewDokumenta);
            this.MaximumSize = new System.Drawing.Size(542, 363);
            this.MinimumSize = new System.Drawing.Size(542, 363);
            this.Name = "PrikazivanjeDokumenataFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prikaz dokumenata";
            this.Load += new System.EventHandler(this.PrikazivanjeDokumenataFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDokumenta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDokumenta;
        private System.Windows.Forms.Button btnIzbrisiDokument;
    }
}