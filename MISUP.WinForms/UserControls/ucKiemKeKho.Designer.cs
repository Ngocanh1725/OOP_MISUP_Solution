using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    partial class ucKiemKeKho
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlToolbar;
        private Button btnThem;
        private TextBox txtTimKiem;
        private Button btnTim;
        private ComboBox cmbTrangThai;
        private Panel pnlCard;
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
            this.cmbTrangThai = new ComboBox();
            this.pnlCard = new Panel();
            this.dgvData = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // Header
            this.pnlHeader.BackColor = Color.Transparent; this.pnlHeader.Controls.Add(this.lblTitle); this.pnlHeader.Dock = DockStyle.Top; this.pnlHeader.Height = 50;
            this.lblTitle.AutoSize = true; this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold); this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70); this.lblTitle.Location = new Point(0, 5); this.lblTitle.Text = "Kiểm Kê & Cân Bằng Kho";

            // Toolbar
            this.pnlToolbar.BackColor = Color.White; this.pnlToolbar.Controls.Add(this.btnThem); this.pnlToolbar.Controls.Add(this.txtTimKiem); this.pnlToolbar.Controls.Add(this.btnTim); this.pnlToolbar.Controls.Add(this.cmbTrangThai); this.pnlToolbar.Dock = DockStyle.Top; this.pnlToolbar.Height = 70;

            this.cmbTrangThai.Font = new Font("Segoe UI", 11F); this.cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTrangThai.Items.AddRange(new string[] { "Tất cả trạng thái", "Đã cân bằng", "Đang xử lý", "Đã hủy" }); this.cmbTrangThai.SelectedIndex = 0;
            this.cmbTrangThai.Size = new Size(160, 32); this.cmbTrangThai.Location = new Point(15, 20);

            this.txtTimKiem.Font = new Font("Segoe UI", 11F); this.txtTimKiem.Size = new Size(250, 32); this.txtTimKiem.Location = new Point(185, 20); this.txtTimKiem.Text = "🔍 Tìm mã phiếu..."; this.txtTimKiem.ForeColor = Color.Gray;

            this.btnTim.BackColor = Color.FromArgb(52, 152, 219); this.btnTim.ForeColor = Color.White; this.btnTim.FlatStyle = FlatStyle.Flat; this.btnTim.FlatAppearance.BorderSize = 0;
            this.btnTim.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnTim.Text = "Tìm"; this.btnTim.Size = new Size(70, 32); this.btnTim.Location = new Point(445, 20); this.btnTim.Cursor = Cursors.Hand;

            this.btnThem.BackColor = Color.FromArgb(46, 204, 113); this.btnThem.ForeColor = Color.White; this.btnThem.FlatStyle = FlatStyle.Flat; this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnThem.Text = "📋 Tạo phiếu kiểm kê"; this.btnThem.Size = new Size(180, 32); this.btnThem.Location = new Point(530, 20); this.btnThem.Cursor = Cursors.Hand;

            // Card Panel
            this.pnlCard.BackColor = Color.White; this.pnlCard.Dock = DockStyle.Fill; this.pnlCard.Padding = new Padding(2);
            this.pnlCard.Controls.Add(this.dgvData); this.pnlCard.Controls.Add(this.pnlToolbar);

            // dgvData
            this.dgvData.Dock = DockStyle.Fill; this.dgvData.BackgroundColor = Color.White; this.dgvData.BorderStyle = BorderStyle.None; this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvData.AllowUserToResizeColumns = false; this.dgvData.AllowUserToResizeRows = false;
            headerStyle.BackColor = Color.FromArgb(249, 250, 251); headerStyle.ForeColor = Color.FromArgb(99, 115, 129); headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); headerStyle.SelectionBackColor = Color.FromArgb(249, 250, 251); headerStyle.Padding = new Padding(15, 10, 10, 10);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle; this.dgvData.ColumnHeadersHeight = 50; this.dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            cellStyle.BackColor = Color.White; cellStyle.ForeColor = Color.FromArgb(33, 43, 54); cellStyle.Font = new Font("Segoe UI", 10F); cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); cellStyle.SelectionForeColor = Color.Black; cellStyle.Padding = new Padding(15, 0, 10, 0);
            this.dgvData.DefaultCellStyle = cellStyle; this.dgvData.EnableHeadersVisualStyles = false; this.dgvData.GridColor = Color.FromArgb(226, 232, 240); this.dgvData.ReadOnly = true; this.dgvData.RowHeadersVisible = false; this.dgvData.RowTemplate.Height = 50; this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect; this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.AutoScaleDimensions = new SizeF(8F, 16F); this.AutoScaleMode = AutoScaleMode.Font; this.BackColor = Color.FromArgb(244, 246, 248);
            this.Padding = new Padding(20);
            this.Controls.Add(this.pnlCard); this.Controls.Add(this.pnlHeader); this.Name = "ucKiemKeKho"; this.Size = new Size(1120, 740);

            this.pnlHeader.ResumeLayout(false); this.pnlHeader.PerformLayout(); this.pnlToolbar.ResumeLayout(false); this.pnlToolbar.PerformLayout(); this.pnlCard.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit(); this.ResumeLayout(false);
        }
    }
}