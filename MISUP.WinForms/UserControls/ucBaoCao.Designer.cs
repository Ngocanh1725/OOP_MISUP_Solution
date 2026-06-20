using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    partial class ucBaoCao
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlToolbar;
        private ComboBox cmbLoaiBaoCao;
        private DateTimePicker dtpTuNgay;
        private DateTimePicker dtpDenNgay;
        private Label lblTuNgay;
        private Label lblDenNgay;
        private Button btnXem;
        private Button btnXuatExcel;

        private Panel pnlDashboard;
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
            this.cmbLoaiBaoCao = new ComboBox();
            this.lblTuNgay = new Label();
            this.dtpTuNgay = new DateTimePicker();
            this.lblDenNgay = new Label();
            this.dtpDenNgay = new DateTimePicker();
            this.btnXem = new Button();
            this.btnXuatExcel = new Button();

            this.pnlDashboard = new Panel();
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
            this.lblTitle.Text = "Báo Cáo Tổng Hợp";

            // pnlToolbar
            this.pnlToolbar.BackColor = Color.FromArgb(245, 246, 250);
            this.pnlToolbar.Controls.AddRange(new Control[] { cmbLoaiBaoCao, lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, btnXem, btnXuatExcel });
            this.pnlToolbar.Dock = DockStyle.Top;
            this.pnlToolbar.Height = 65;
            this.pnlToolbar.Padding = new Padding(20, 15, 20, 15);

            this.cmbLoaiBaoCao.Font = new Font("Segoe UI", 11F); this.cmbLoaiBaoCao.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLoaiBaoCao.Items.AddRange(new string[] { "Báo cáo Tồn Kho Hiện Tại", "Báo cáo Hàng Sắp Hết", "Báo cáo Hàng Cận Date" }); this.cmbLoaiBaoCao.SelectedIndex = 0;
            this.cmbLoaiBaoCao.Size = new Size(250, 32); this.cmbLoaiBaoCao.Location = new Point(20, 16);

            this.lblTuNgay.Text = "Từ:"; this.lblTuNgay.Location = new Point(290, 20); this.lblTuNgay.AutoSize = true;
            this.dtpTuNgay.Format = DateTimePickerFormat.Short; this.dtpTuNgay.Location = new Point(320, 16); this.dtpTuNgay.Size = new Size(110, 25); this.dtpTuNgay.Font = new Font("Segoe UI", 10F);

            this.lblDenNgay.Text = "Đến:"; this.lblDenNgay.Location = new Point(450, 20); this.lblDenNgay.AutoSize = true;
            this.dtpDenNgay.Format = DateTimePickerFormat.Short; this.dtpDenNgay.Location = new Point(490, 16); this.dtpDenNgay.Size = new Size(110, 25); this.dtpDenNgay.Font = new Font("Segoe UI", 10F);

            this.btnXem.BackColor = Color.FromArgb(52, 152, 219); this.btnXem.ForeColor = Color.White; this.btnXem.FlatStyle = FlatStyle.Flat; this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnXem.Text = "🔄 Xem Báo Cáo"; this.btnXem.Size = new Size(140, 33); this.btnXem.Location = new Point(620, 14); this.btnXem.Cursor = Cursors.Hand;

            this.btnXuatExcel.BackColor = Color.FromArgb(39, 174, 96); this.btnXuatExcel.ForeColor = Color.White; this.btnXuatExcel.FlatStyle = FlatStyle.Flat; this.btnXuatExcel.FlatAppearance.BorderSize = 0;
            this.btnXuatExcel.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnXuatExcel.Text = "📥 Xuất CSV"; this.btnXuatExcel.Size = new Size(110, 33); this.btnXuatExcel.Location = new Point(770, 14); this.btnXuatExcel.Cursor = Cursors.Hand;

            // pnlDashboard
            this.pnlDashboard.Dock = DockStyle.Top;
            this.pnlDashboard.Height = 100;
            this.pnlDashboard.BackColor = Color.FromArgb(244, 246, 248);

            // dgvData
            this.dgvData.Dock = DockStyle.Fill;
            this.dgvData.BackgroundColor = Color.White; this.dgvData.BorderStyle = BorderStyle.None; this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            headerStyle.BackColor = Color.FromArgb(41, 128, 185); headerStyle.ForeColor = Color.White; headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); headerStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle; this.dgvData.ColumnHeadersHeight = 45;
            cellStyle.BackColor = Color.White; cellStyle.ForeColor = Color.FromArgb(33, 43, 54); cellStyle.Font = new Font("Segoe UI", 10F); cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); cellStyle.SelectionForeColor = Color.Black;
            this.dgvData.DefaultCellStyle = cellStyle;
            this.dgvData.EnableHeadersVisualStyles = false; this.dgvData.GridColor = Color.FromArgb(226, 232, 240); this.dgvData.ReadOnly = true; this.dgvData.RowHeadersVisible = false; this.dgvData.RowTemplate.Height = 35; this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect; this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ucBaoCao
            this.AutoScaleDimensions = new SizeF(8F, 16F); this.AutoScaleMode = AutoScaleMode.Font; this.BackColor = Color.White;
            this.Controls.Add(this.dgvData); this.Controls.Add(this.pnlDashboard); this.Controls.Add(this.pnlToolbar); this.Controls.Add(this.pnlHeader); this.Name = "ucBaoCao"; this.Size = new Size(1120, 740);

            this.pnlHeader.ResumeLayout(false); this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false); this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
        }
    }
}