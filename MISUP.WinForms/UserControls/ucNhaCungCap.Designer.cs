using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    partial class ucNhaCungCap
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlToolbar;
        private Button btnThem;
        private TextBox txtTimKiem;
        private Button btnTim;
        private DataGridView dgvData;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.pnlToolbar = new Panel();
            this.btnThem = new Button();
            this.txtTimKiem = new TextBox();
            this.btnTim = new Button();
            this.dgvData = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            this.pnlHeader.BackColor = Color.White; this.pnlHeader.Controls.Add(this.lblTitle); this.pnlHeader.Dock = DockStyle.Top; this.pnlHeader.Height = 60;
            this.lblTitle.AutoSize = true; this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold); this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70); this.lblTitle.Location = new Point(20, 15); this.lblTitle.Text = "Danh Mục Đối Tác / Nhà Cung Cấp";

            this.pnlToolbar.BackColor = Color.FromArgb(245, 246, 250); this.pnlToolbar.Controls.Add(this.btnThem); this.pnlToolbar.Controls.Add(this.txtTimKiem); this.pnlToolbar.Controls.Add(this.btnTim); this.pnlToolbar.Dock = DockStyle.Top; this.pnlToolbar.Height = 65; this.pnlToolbar.Padding = new Padding(20, 15, 20, 15);

            this.btnThem.BackColor = Color.FromArgb(46, 204, 113); this.btnThem.ForeColor = Color.White; this.btnThem.FlatStyle = FlatStyle.Flat; this.btnThem.FlatAppearance.BorderSize = 0; this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnThem.Text = "➕ Thêm đối tác"; this.btnThem.Size = new Size(150, 35); this.btnThem.Location = new Point(20, 15); this.btnThem.Cursor = Cursors.Hand;
            this.txtTimKiem.Font = new Font("Segoe UI", 11F); this.txtTimKiem.Size = new Size(300, 32); this.txtTimKiem.Location = new Point(190, 16); this.txtTimKiem.Text = "Tìm theo tên, SĐT đối tác..."; this.txtTimKiem.ForeColor = Color.Gray;
            this.btnTim.BackColor = Color.FromArgb(52, 152, 219); this.btnTim.ForeColor = Color.White; this.btnTim.FlatStyle = FlatStyle.Flat; this.btnTim.FlatAppearance.BorderSize = 0; this.btnTim.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnTim.Text = "Tìm"; this.btnTim.Size = new Size(70, 33); this.btnTim.Location = new Point(500, 15); this.btnTim.Cursor = Cursors.Hand;

            this.dgvData.Dock = DockStyle.Fill; this.dgvData.BackgroundColor = Color.White; this.dgvData.BorderStyle = BorderStyle.None; this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            headerStyle.BackColor = Color.FromArgb(249, 250, 251); headerStyle.ForeColor = Color.FromArgb(99, 115, 129); headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); headerStyle.SelectionBackColor = Color.FromArgb(249, 250, 251);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle; this.dgvData.ColumnHeadersHeight = 50;
            cellStyle.BackColor = Color.White; cellStyle.ForeColor = Color.FromArgb(33, 43, 54); cellStyle.Font = new Font("Segoe UI", 10F); cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); cellStyle.SelectionForeColor = Color.Black;
            this.dgvData.DefaultCellStyle = cellStyle; this.dgvData.EnableHeadersVisualStyles = false; this.dgvData.GridColor = Color.FromArgb(226, 232, 240); this.dgvData.ReadOnly = true; this.dgvData.RowHeadersVisible = false; this.dgvData.RowTemplate.Height = 45; this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect; this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.AutoScaleDimensions = new SizeF(8F, 16F); this.AutoScaleMode = AutoScaleMode.Font; this.BackColor = Color.White;
            this.Controls.Add(this.dgvData); this.Controls.Add(this.pnlToolbar); this.Controls.Add(this.pnlHeader); this.Name = "ucNhaCungCap"; this.Size = new Size(1120, 740);

            this.pnlHeader.ResumeLayout(false); this.pnlHeader.PerformLayout(); this.pnlToolbar.ResumeLayout(false); this.pnlToolbar.PerformLayout(); ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit(); this.ResumeLayout(false);
        }
    }
}