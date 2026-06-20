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
        private Button btnXemBieuDo; // Nút mới
        private Button btnXuatExcel;

        private FlowLayoutPanel pnlDashboard;
        private Panel card1, card2, card3;
        private Label lblThongKe1, lblThongKe2, lblThongKe3;

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
            this.cmbLoaiBaoCao = new ComboBox();
            this.lblTuNgay = new Label();
            this.dtpTuNgay = new DateTimePicker();
            this.lblDenNgay = new Label();
            this.dtpDenNgay = new DateTimePicker();
            this.btnXem = new Button();
            this.btnXemBieuDo = new Button();
            this.btnXuatExcel = new Button();

            this.pnlDashboard = new FlowLayoutPanel();
            this.card1 = new Panel(); this.lblThongKe1 = new Label();
            this.card2 = new Panel(); this.lblThongKe2 = new Label();
            this.card3 = new Panel(); this.lblThongKe3 = new Label();

            this.pnlCard = new Panel();
            this.dgvData = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.card1.SuspendLayout(); this.card2.SuspendLayout(); this.card3.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // Header
            this.pnlHeader.BackColor = Color.Transparent; this.pnlHeader.Controls.Add(this.lblTitle); this.pnlHeader.Dock = DockStyle.Top; this.pnlHeader.Height = 50;
            this.lblTitle.AutoSize = true; this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold); this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70); this.lblTitle.Location = new Point(0, 5); this.lblTitle.Text = "Báo Cáo Tổng Hợp & Thống Kê";

            // Toolbar
            this.pnlToolbar.BackColor = Color.White;
            this.pnlToolbar.Controls.AddRange(new Control[] { cmbLoaiBaoCao, lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, btnXem, btnXemBieuDo, btnXuatExcel });
            this.pnlToolbar.Dock = DockStyle.Top; this.pnlToolbar.Height = 70;

            this.cmbLoaiBaoCao.Font = new Font("Segoe UI", 11F); this.cmbLoaiBaoCao.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLoaiBaoCao.Items.AddRange(new string[] { "Báo cáo Tồn Kho Hiện Tại", "Báo cáo Hàng Sắp Hết", "Báo cáo Hàng Cận Date" }); this.cmbLoaiBaoCao.SelectedIndex = 0;
            this.cmbLoaiBaoCao.Size = new Size(250, 32); this.cmbLoaiBaoCao.Location = new Point(15, 20);

            this.lblTuNgay.Text = "Từ:"; this.lblTuNgay.Location = new Point(280, 25); this.lblTuNgay.AutoSize = true;
            this.dtpTuNgay.Format = DateTimePickerFormat.Short; this.dtpTuNgay.Location = new Point(310, 20); this.dtpTuNgay.Size = new Size(110, 25); this.dtpTuNgay.Font = new Font("Segoe UI", 10F);

            this.lblDenNgay.Text = "Đến:"; this.lblDenNgay.Location = new Point(430, 25); this.lblDenNgay.AutoSize = true;
            this.dtpDenNgay.Format = DateTimePickerFormat.Short; this.dtpDenNgay.Location = new Point(470, 20); this.dtpDenNgay.Size = new Size(110, 25); this.dtpDenNgay.Font = new Font("Segoe UI", 10F);

            // Nút Xem Báo Cáo
            this.btnXem.BackColor = Color.FromArgb(52, 152, 219); this.btnXem.ForeColor = Color.White; this.btnXem.FlatStyle = FlatStyle.Flat; this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnXem.Text = "🔄 Xem Báo Cáo"; this.btnXem.Size = new Size(140, 32); this.btnXem.Location = new Point(600, 20); this.btnXem.Cursor = Cursors.Hand;

            // Nút Xem Biểu Đồ (Mới)
            this.btnXemBieuDo.BackColor = Color.FromArgb(142, 68, 173); this.btnXemBieuDo.ForeColor = Color.White; this.btnXemBieuDo.FlatStyle = FlatStyle.Flat; this.btnXemBieuDo.FlatAppearance.BorderSize = 0;
            this.btnXemBieuDo.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnXemBieuDo.Text = "📊 Xem Biểu Đồ"; this.btnXemBieuDo.Size = new Size(150, 32); this.btnXemBieuDo.Location = new Point(750, 20); this.btnXemBieuDo.Cursor = Cursors.Hand;

            // Nút Xuất Excel
            this.btnXuatExcel.BackColor = Color.FromArgb(39, 174, 96); this.btnXuatExcel.ForeColor = Color.White; this.btnXuatExcel.FlatStyle = FlatStyle.Flat; this.btnXuatExcel.FlatAppearance.BorderSize = 0;
            this.btnXuatExcel.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.btnXuatExcel.Text = "📥 Xuất CSV"; this.btnXuatExcel.Size = new Size(110, 32); this.btnXuatExcel.Location = new Point(910, 20); this.btnXuatExcel.Cursor = Cursors.Hand;

            // Dashboard
            this.pnlDashboard.Dock = DockStyle.Top; this.pnlDashboard.Height = 100; this.pnlDashboard.Padding = new Padding(0, 10, 0, 10);
            this.pnlDashboard.Controls.Add(card1); this.pnlDashboard.Controls.Add(card2); this.pnlDashboard.Controls.Add(card3);

            this.card1.BackColor = Color.FromArgb(142, 68, 173); this.card1.Size = new Size(245, 80); this.card1.Margin = new Padding(0, 0, 15, 0);
            Label l1 = new Label() { Text = "📊 TỔNG MÃ HÀNG", ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(15, 15) };
            this.lblThongKe1.Text = "0 SKU"; this.lblThongKe1.ForeColor = Color.White; this.lblThongKe1.Font = new Font("Segoe UI", 16F, FontStyle.Bold); this.lblThongKe1.AutoSize = true; this.lblThongKe1.Location = new Point(15, 35);
            this.card1.Controls.Add(l1); this.card1.Controls.Add(lblThongKe1);

            this.card2.BackColor = Color.FromArgb(39, 174, 96); this.card2.Size = new Size(270, 80); this.card2.Margin = new Padding(0, 0, 15, 0);
            Label l2 = new Label() { Text = "💰 TỔNG TÀI SẢN KHO", ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(15, 15) };
            this.lblThongKe2.Text = "0 đ"; this.lblThongKe2.ForeColor = Color.White; this.lblThongKe2.Font = new Font("Segoe UI", 14F, FontStyle.Bold); this.lblThongKe2.AutoSize = true; this.lblThongKe2.Location = new Point(15, 38);
            this.card2.Controls.Add(l2); this.card2.Controls.Add(lblThongKe2);

            this.card3.BackColor = Color.FromArgb(230, 126, 34); this.card3.Size = new Size(245, 80); this.card3.Margin = new Padding(0, 0, 0, 0);
            Label l3 = new Label() { Text = "📦 TỔNG SL VẬT LÝ", ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(15, 15) };
            this.lblThongKe3.Text = "0 cái"; this.lblThongKe3.ForeColor = Color.White; this.lblThongKe3.Font = new Font("Segoe UI", 16F, FontStyle.Bold); this.lblThongKe3.AutoSize = true; this.lblThongKe3.Location = new Point(15, 35);
            this.card3.Controls.Add(l3); this.card3.Controls.Add(lblThongKe3);

            // Card Panel chứa Grid
            this.pnlCard.BackColor = Color.White; this.pnlCard.Dock = DockStyle.Fill; this.pnlCard.Padding = new Padding(2);
            this.pnlCard.Controls.Add(this.dgvData);
            this.pnlCard.Controls.Add(this.pnlToolbar);

            // dgvData
            this.dgvData.Dock = DockStyle.Fill; this.dgvData.BackgroundColor = Color.White; this.dgvData.BorderStyle = BorderStyle.None; this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvData.AllowUserToResizeColumns = false; this.dgvData.AllowUserToResizeRows = false;
            headerStyle.BackColor = Color.FromArgb(249, 250, 251); headerStyle.ForeColor = Color.FromArgb(99, 115, 129); headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); headerStyle.SelectionBackColor = Color.FromArgb(249, 250, 251); headerStyle.Padding = new Padding(15, 10, 10, 10);
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle; this.dgvData.ColumnHeadersHeight = 50; this.dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            cellStyle.BackColor = Color.White; cellStyle.ForeColor = Color.FromArgb(33, 43, 54); cellStyle.Font = new Font("Segoe UI", 10F); cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); cellStyle.SelectionForeColor = Color.Black; cellStyle.Padding = new Padding(15, 0, 10, 0);
            this.dgvData.DefaultCellStyle = cellStyle; this.dgvData.EnableHeadersVisualStyles = false; this.dgvData.GridColor = Color.FromArgb(226, 232, 240); this.dgvData.ReadOnly = true; this.dgvData.RowHeadersVisible = false; this.dgvData.RowTemplate.Height = 50; this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect; this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.AutoScaleDimensions = new SizeF(8F, 16F); this.AutoScaleMode = AutoScaleMode.Font; this.BackColor = Color.FromArgb(244, 246, 248);
            this.Padding = new Padding(20);
            this.Controls.Add(this.pnlCard); this.Controls.Add(this.pnlDashboard); this.Controls.Add(this.pnlHeader); this.Name = "ucBaoCao"; this.Size = new Size(1120, 740);

            this.pnlHeader.ResumeLayout(false); this.pnlHeader.PerformLayout(); this.pnlToolbar.ResumeLayout(false); this.pnlToolbar.PerformLayout();
            this.pnlDashboard.ResumeLayout(false); this.card1.ResumeLayout(false); this.card1.PerformLayout(); this.card2.ResumeLayout(false); this.card2.PerformLayout(); this.card3.ResumeLayout(false); this.card3.PerformLayout();
            this.pnlCard.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit(); this.ResumeLayout(false);
        }
    }
}