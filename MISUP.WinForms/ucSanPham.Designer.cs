namespace MISUP.WinForms
{
    partial class ucSanPham
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridView dgvData;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel(); this.btnTim = new System.Windows.Forms.Button(); this.txtTimKiem = new System.Windows.Forms.TextBox(); this.dgvData = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit(); this.SuspendLayout();

            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke; this.pnlTop.Controls.Add(this.btnTim); this.pnlTop.Controls.Add(this.txtTimKiem); this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top; this.pnlTop.Location = new System.Drawing.Point(0, 0); this.pnlTop.Name = "pnlTop"; this.pnlTop.Size = new System.Drawing.Size(780, 60);
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F); this.txtTimKiem.Location = new System.Drawing.Point(560, 15); this.txtTimKiem.Name = "txtTimKiem"; this.txtTimKiem.Size = new System.Drawing.Size(150, 32);
            this.btnTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219))))); this.btnTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnTim.ForeColor = System.Drawing.Color.White; this.btnTim.Location = new System.Drawing.Point(720, 14); this.btnTim.Name = "btnTim"; this.btnTim.Size = new System.Drawing.Size(70, 34); this.btnTim.Text = "Tìm"; this.btnTim.UseVisualStyleBackColor = false; this.btnTim.Click += new System.EventHandler(this.btnTim_Click);

            this.dgvData.AllowUserToAddRows = false; this.dgvData.AllowUserToDeleteRows = false; this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvData.BackgroundColor = System.Drawing.Color.White; this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft; cellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(168))))); cellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); cellStyle.ForeColor = System.Drawing.Color.White; cellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight; cellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvData.ColumnHeadersDefaultCellStyle = cellStyle; this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize; this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvData.EnableHeadersVisualStyles = false; this.dgvData.Location = new System.Drawing.Point(0, 60); this.dgvData.Name = "dgvData"; this.dgvData.ReadOnly = true; this.dgvData.RowHeadersVisible = false; this.dgvData.RowTemplate.Height = 35; this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvData.Size = new System.Drawing.Size(780, 480);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.White; this.Controls.Add(this.dgvData); this.Controls.Add(this.pnlTop); this.Name = "ucSanPham"; this.Size = new System.Drawing.Size(780, 540); this.Load += new System.EventHandler(this.ucSanPham_Load);
            this.pnlTop.ResumeLayout(false); this.pnlTop.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit(); this.ResumeLayout(false);
        }
    }
}
