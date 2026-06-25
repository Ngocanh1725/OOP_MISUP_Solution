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
        private Button btnSua;
        private Button btnXoa;
        private TextBox txtTimKiem;
        private Button btnTim;

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
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.txtTimKiem = new TextBox();
            this.btnTim = new Button();
            this.pnlCard = new Panel();
            this.dgvData = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // Header
            this.pnlHeader.BackColor = Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 50;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(0, 5);
            this.lblTitle.Text = "Danh Mục Đối Tác / Nhà Cung Cấp";

            // Toolbar
            this.pnlToolbar.BackColor = Color.White;
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.Controls.Add(this.btnSua);
            this.pnlToolbar.Controls.Add(this.btnXoa);
            this.pnlToolbar.Controls.Add(this.txtTimKiem);
            this.pnlToolbar.Controls.Add(this.btnTim);
            this.pnlToolbar.Dock = DockStyle.Top;
            this.pnlToolbar.Height = 70;

            this.txtTimKiem.Font = new Font("Segoe UI", 11F); this.txtTimKiem.Size = new Size(300, 32); this.txtTimKiem.Location = new Point(15, 20); this.txtTimKiem.Text = "🔍 Tìm theo tên, SĐT đối tác..."; this.txtTimKiem.ForeColor = Color.Gray;
            this.btnTim.BackColor = Color.FromArgb(52, 152, 219); this.btnTim.ForeColor = Color.White; this.btnTim.FlatStyle = FlatStyle.Flat; this.btnTim.FlatAppearance.BorderSize = 0; this.btnTim.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnTim.Text = "Tìm"; this.btnTim.Size = new Size(70, 32); this.btnTim.Location = new Point(325, 20); this.btnTim.Cursor = Cursors.Hand;

            this.btnThem.BackColor = Color.FromArgb(46, 204, 113); this.btnThem.ForeColor = Color.White; this.btnThem.FlatStyle = FlatStyle.Flat; this.btnThem.FlatAppearance.BorderSize = 0; this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnThem.Text = "➕ Thêm đối tác"; this.btnThem.Size = new Size(150, 32); this.btnThem.Location = new Point(410, 20); this.btnThem.Cursor = Cursors.Hand;
            this.btnSua.BackColor = Color.FromArgb(243, 156, 18); this.btnSua.ForeColor = Color.White; this.btnSua.FlatStyle = FlatStyle.Flat; this.btnSua.FlatAppearance.BorderSize = 0; this.btnSua.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnSua.Text = "✏️ Sửa"; this.btnSua.Size = new Size(90, 32); this.btnSua.Location = new Point(570, 20); this.btnSua.Cursor = Cursors.Hand;
            this.btnXoa.BackColor = Color.FromArgb(231, 76, 60); this.btnXoa.ForeColor = Color.White; this.btnXoa.FlatStyle = FlatStyle.Flat; this.btnXoa.FlatAppearance.BorderSize = 0; this.btnXoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnXoa.Text = "🗑️ Xóa"; this.btnXoa.Size = new Size(90, 32); this.btnXoa.Location = new Point(670, 20); this.btnXoa.Cursor = Cursors.Hand;

            // Card Panel
            this.pnlCard.BackColor = Color.White;
            this.pnlCard.Dock = DockStyle.Fill;
            this.pnlCard.Padding = new Padding(2);
            this.pnlCard.Controls.Add(this.dgvData);
            this.pnlCard.Controls.Add(this.pnlToolbar);

            // DataGridView (KHÓA KÉO DÃN)
            this.dgvData.Dock = DockStyle.Fill; this.dgvData.BackgroundColor = Color.White; this.dgvData.BorderStyle = BorderStyle.None; this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvData.AllowUserToResizeColumns = false; this.dgvData.AllowUserToResizeRows = false;

            headerStyle.BackColor = Color.FromArgb(249, 250, 251); headerStyle.ForeColor = Color.FromArgb(99, 115, 129); headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); headerStyle.SelectionBackColor = Color.FromArgb(249, 250, 251); headerStyle.Padding = new Padding(15, 10, 10, 10);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle; this.dgvData.ColumnHeadersHeight = 50; this.dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            cellStyle.BackColor = Color.White; cellStyle.ForeColor = Color.FromArgb(33, 43, 54); cellStyle.Font = new Font("Segoe UI", 10F); cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); cellStyle.SelectionForeColor = Color.Black; cellStyle.Padding = new Padding(15, 0, 10, 0);
            this.dgvData.DefaultCellStyle = cellStyle; this.dgvData.EnableHeadersVisualStyles = false; this.dgvData.GridColor = Color.FromArgb(226, 232, 240); this.dgvData.ReadOnly = true; this.dgvData.RowHeadersVisible = false; this.dgvData.RowTemplate.Height = 50; this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect; this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.AutoScaleDimensions = new SizeF(8F, 16F); this.AutoScaleMode = AutoScaleMode.Font; this.BackColor = Color.FromArgb(244, 246, 248);
            this.Padding = new Padding(20);
            this.Controls.Add(this.pnlCard); this.Controls.Add(this.pnlHeader); this.Name = "ucNhaCungCap"; this.Size = new Size(1120, 740);

            this.pnlHeader.ResumeLayout(false); this.pnlHeader.PerformLayout(); this.pnlToolbar.ResumeLayout(false); this.pnlToolbar.PerformLayout(); this.pnlCard.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit(); this.ResumeLayout(false);
        }
    }
}