using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    partial class ucNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlToolbar;
        private Button btnThem;
        private Button btnInPhieu;
        private TextBox txtTimKiem;
        private Button btnTim;
        private ComboBox cmbTrangThai;
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
            this.btnInPhieu = new Button();
            this.txtTimKiem = new TextBox();
            this.btnTim = new Button();
            this.cmbTrangThai = new ComboBox();
            this.dgvData = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 60;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Text = "Quản Lý Phiếu Nhập Kho";

            // pnlToolbar
            this.pnlToolbar.BackColor = Color.FromArgb(245, 246, 250);
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.Controls.Add(this.btnInPhieu);
            this.pnlToolbar.Controls.Add(this.txtTimKiem);
            this.pnlToolbar.Controls.Add(this.btnTim);
            this.pnlToolbar.Controls.Add(this.cmbTrangThai);
            this.pnlToolbar.Dock = DockStyle.Top;
            this.pnlToolbar.Height = 65;
            this.pnlToolbar.Padding = new Padding(20, 15, 20, 15);

            this.btnThem.BackColor = Color.FromArgb(0, 136, 255); this.btnThem.ForeColor = Color.White; this.btnThem.FlatStyle = FlatStyle.Flat; this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnThem.Text = "➕ Tạo phiếu nhập"; this.btnThem.Size = new Size(160, 35); this.btnThem.Location = new Point(20, 15); this.btnThem.Cursor = Cursors.Hand;

            this.btnInPhieu.BackColor = Color.White; this.btnInPhieu.ForeColor = Color.FromArgb(64, 64, 64); this.btnInPhieu.FlatStyle = FlatStyle.Flat; this.btnInPhieu.FlatAppearance.BorderColor = Color.LightGray;
            this.btnInPhieu.Font = new Font("Segoe UI", 10F); this.btnInPhieu.Text = "🖨️ In mã vạch"; this.btnInPhieu.Size = new Size(120, 35); this.btnInPhieu.Location = new Point(190, 15); this.btnInPhieu.Cursor = Cursors.Hand;

            this.cmbTrangThai.Font = new Font("Segoe UI", 11F); this.cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTrangThai.Items.AddRange(new string[] { "Tất cả trạng thái", "Đã nhập kho", "Đang vận chuyển", "Đã hủy" }); this.cmbTrangThai.SelectedIndex = 0;
            this.cmbTrangThai.Size = new Size(160, 32); this.cmbTrangThai.Location = new Point(330, 16);

            this.txtTimKiem.Font = new Font("Segoe UI", 11F); this.txtTimKiem.Size = new Size(250, 32); this.txtTimKiem.Location = new Point(500, 16); this.txtTimKiem.Text = "Tìm theo mã phiếu..."; this.txtTimKiem.ForeColor = Color.Gray;

            this.btnTim.BackColor = Color.FromArgb(52, 152, 219); this.btnTim.ForeColor = Color.White; this.btnTim.FlatStyle = FlatStyle.Flat; this.btnTim.FlatAppearance.BorderSize = 0;
            this.btnTim.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnTim.Text = "Tìm"; this.btnTim.Size = new Size(70, 33); this.btnTim.Location = new Point(760, 15); this.btnTim.Cursor = Cursors.Hand;

            // dgvData
            this.dgvData.Dock = DockStyle.Fill;
            this.dgvData.BackgroundColor = Color.White; this.dgvData.BorderStyle = BorderStyle.None; this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            headerStyle.BackColor = Color.FromArgb(249, 250, 251); headerStyle.ForeColor = Color.FromArgb(99, 115, 129); headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); headerStyle.SelectionBackColor = Color.FromArgb(249, 250, 251);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle; this.dgvData.ColumnHeadersHeight = 50;
            cellStyle.BackColor = Color.White; cellStyle.ForeColor = Color.FromArgb(33, 43, 54); cellStyle.Font = new Font("Segoe UI", 10F); cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); cellStyle.SelectionForeColor = Color.Black;
            this.dgvData.DefaultCellStyle = cellStyle;
            this.dgvData.EnableHeadersVisualStyles = false; this.dgvData.GridColor = Color.FromArgb(226, 232, 240); this.dgvData.ReadOnly = true; this.dgvData.RowHeadersVisible = false; this.dgvData.RowTemplate.Height = 45; this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect; this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ucNhapHang
            this.AutoScaleDimensions = new SizeF(8F, 16F); this.AutoScaleMode = AutoScaleMode.Font; this.BackColor = Color.White;
            this.Controls.Add(this.dgvData); this.Controls.Add(this.pnlToolbar); this.Controls.Add(this.pnlHeader); this.Name = "ucNhapHang"; this.Size = new Size(1120, 740);

            this.pnlHeader.ResumeLayout(false); this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false); this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
        }
    }
}