using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    partial class ucSanPham
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Button btnThem;
        private Button btnXuatExcel;

        // Dùng FlowLayoutPanel để các thẻ Card tự động sắp xếp không bị đè chữ
        private FlowLayoutPanel pnlDashboard;
        private Panel card1, card2, card3;
        private Label lblTongSPTitle, lblTongSP;
        private Label lblTongGiaTriTitle, lblTongGiaTri;
        private Label lblCanhBaoTonTitle, lblCanhBaoTon;

        private Panel pnlCard;
        private Panel pnlTabs;
        private Panel pnlTabLine;
        private Button btnTabTatCa, btnTabThucPham, btnTabDienTu, btnTabMyPham, btnTabGiaDung, btnTabThoiTrang;

        private Panel pnlFilters;
        private TextBox txtTimKiem;
        private Button btnTim;
        private Button btnSortAsc;
        private Button btnSortDesc;
        private Button btnSua;
        private Button btnXoa;

        private DataGridView dgvData;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.btnThem = new Button();
            this.btnXuatExcel = new Button();

            this.pnlDashboard = new FlowLayoutPanel();
            this.card1 = new Panel();
            this.lblTongSPTitle = new Label();
            this.lblTongSP = new Label();

            this.card2 = new Panel();
            this.lblTongGiaTriTitle = new Label();
            this.lblTongGiaTri = new Label();

            this.card3 = new Panel();
            this.lblCanhBaoTonTitle = new Label();
            this.lblCanhBaoTon = new Label();

            this.pnlCard = new Panel();
            this.dgvData = new DataGridView();
            this.pnlFilters = new Panel();
            this.txtTimKiem = new TextBox();
            this.btnTim = new Button();
            this.btnSortAsc = new Button();
            this.btnSortDesc = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();

            this.pnlTabs = new Panel();
            this.btnTabTatCa = new Button();
            this.btnTabThucPham = new Button();
            this.btnTabDienTu = new Button();
            this.btnTabMyPham = new Button();
            this.btnTabGiaDung = new Button();
            this.btnTabThoiTrang = new Button();
            this.pnlTabLine = new Panel();

            this.pnlHeader.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // 1. pnlHeader
            // ==========================================
            this.pnlHeader.BackColor = Color.Transparent;
            this.pnlHeader.Controls.Add(this.btnXuatExcel);
            this.pnlHeader.Controls.Add(this.btnThem);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 60;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(0, 10);
            this.lblTitle.Text = "Quản Lý Sản Phẩm";

            // Nút Tạo Sản Phẩm (Đã chỉnh tọa độ X và Size rộng rãi)
            this.btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnThem.BackColor = Color.FromArgb(0, 136, 255);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = FlatStyle.Flat;
            this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnThem.ForeColor = Color.White;
            this.btnThem.Location = new Point(940, 10); // Đặt sát lề phải
            this.btnThem.Size = new Size(120, 36);
            this.btnThem.Text = "➕ Tạo SP";
            this.btnThem.Cursor = Cursors.Hand;

            // Nút Xuất File (Đã đẩy lùi ra xa nút Tạo SP để không bao giờ đè lên nhau)
            this.btnXuatExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnXuatExcel.BackColor = Color.White;
            this.btnXuatExcel.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnXuatExcel.FlatStyle = FlatStyle.Flat;
            this.btnXuatExcel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnXuatExcel.ForeColor = Color.FromArgb(80, 80, 80);
            this.btnXuatExcel.Location = new Point(820, 10); // Khoảng cách tới nút kia là 20px
            this.btnXuatExcel.Size = new Size(100, 36);
            this.btnXuatExcel.Text = "📥 Xuất";
            this.btnXuatExcel.Cursor = Cursors.Hand;

            // ==========================================
            // 2. pnlDashboard (Thống kê) - Dùng FlowLayoutPanel
            // ==========================================
            this.pnlDashboard.Dock = DockStyle.Top;
            this.pnlDashboard.Height = 100;
            this.pnlDashboard.Padding = new Padding(0, 10, 0, 10);
            this.pnlDashboard.WrapContents = false; // Ngăn rớt dòng nếu không cần thiết
            this.pnlDashboard.AutoScroll = true; // Cho phép cuộn nếu màn hình quá hẹp
            this.pnlDashboard.Controls.Add(card1);
            this.pnlDashboard.Controls.Add(card2);
            this.pnlDashboard.Controls.Add(card3);

            // Card 1
            this.card1.BackColor = Color.FromArgb(52, 152, 219);
            this.card1.Size = new Size(245, 80);
            this.card1.Margin = new Padding(0, 0, 15, 0); // Khoảng cách giữa các thẻ

            this.lblTongSPTitle.Text = "📦 TỔNG MẶT HÀNG";
            this.lblTongSPTitle.ForeColor = Color.White;
            this.lblTongSPTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTongSPTitle.Location = new Point(15, 15);
            this.lblTongSPTitle.AutoSize = true;

            this.lblTongSP.Text = "0";
            this.lblTongSP.ForeColor = Color.White;
            this.lblTongSP.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTongSP.Location = new Point(15, 35);
            this.lblTongSP.AutoSize = true;

            this.card1.Controls.Add(lblTongSPTitle);
            this.card1.Controls.Add(lblTongSP);

            // Card 2
            this.card2.BackColor = Color.FromArgb(46, 204, 113);
            this.card2.Size = new Size(280, 80); // Mở rộng từ 260 lên 280 để chữ không bị chạm lề
            this.card2.Margin = new Padding(0, 0, 15, 0);

            this.lblTongGiaTriTitle.Text = "💰 TỔNG GIÁ TRỊ KHO";
            this.lblTongGiaTriTitle.ForeColor = Color.White;
            this.lblTongGiaTriTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTongGiaTriTitle.Location = new Point(15, 15);
            this.lblTongGiaTriTitle.AutoSize = true;

            // Giảm Font Size ở đây xuống 14F để tránh khuyết số khi lên hàng chục tỷ
            this.lblTongGiaTri.Text = "0 đ";
            this.lblTongGiaTri.ForeColor = Color.White;
            this.lblTongGiaTri.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTongGiaTri.Location = new Point(15, 38);
            this.lblTongGiaTri.AutoSize = true;

            this.card2.Controls.Add(lblTongGiaTriTitle);
            this.card2.Controls.Add(lblTongGiaTri);

            // Card 3
            this.card3.BackColor = Color.FromArgb(231, 76, 60);
            this.card3.Size = new Size(245, 80);
            this.card3.Margin = new Padding(0, 0, 0, 0);

            this.lblCanhBaoTonTitle.Text = "⚠️ SẮP HẾT HÀNG (<10)";
            this.lblCanhBaoTonTitle.ForeColor = Color.White;
            this.lblCanhBaoTonTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCanhBaoTonTitle.Location = new Point(15, 15);
            this.lblCanhBaoTonTitle.AutoSize = true;

            this.lblCanhBaoTon.Text = "0 SP";
            this.lblCanhBaoTon.ForeColor = Color.White;
            this.lblCanhBaoTon.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblCanhBaoTon.Location = new Point(15, 35);
            this.lblCanhBaoTon.AutoSize = true;

            this.card3.Controls.Add(lblCanhBaoTonTitle);
            this.card3.Controls.Add(lblCanhBaoTon);

            // ==========================================
            // 3. pnlCard (Khung trắng chứa Grid)
            // ==========================================
            this.pnlCard.BackColor = Color.White;
            this.pnlCard.Dock = DockStyle.Fill;
            this.pnlCard.Padding = new Padding(1);
            this.pnlCard.Controls.Add(this.dgvData);
            this.pnlCard.Controls.Add(this.pnlFilters);
            this.pnlCard.Controls.Add(this.pnlTabs);

            // ==========================================
            // 4. pnlTabs
            // ==========================================
            this.pnlTabs.Dock = DockStyle.Top;
            this.pnlTabs.Height = 50;
            this.pnlTabs.BackColor = Color.White;
            this.pnlTabs.Controls.AddRange(new Control[] { btnTabTatCa, btnTabThucPham, btnTabDienTu, btnTabMyPham, btnTabGiaDung, btnTabThoiTrang, pnlTabLine });

            this.pnlTabLine.BackColor = Color.FromArgb(226, 232, 240);
            this.pnlTabLine.Dock = DockStyle.Bottom;
            this.pnlTabLine.Height = 1;

            Button[] tabs = { btnTabTatCa, btnTabThucPham, btnTabDienTu, btnTabMyPham, btnTabGiaDung, btnTabThoiTrang };
            int tabX = 15;
            foreach (var btn in tabs)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Gray;
                btn.Font = new Font("Segoe UI", 10F);
                btn.Cursor = Cursors.Hand;
                btn.Height = 49;
                btn.Top = 0;
                btn.Left = tabX;
                tabX += 110;
            }
            this.btnTabTatCa.Text = "Tất cả"; this.btnTabTatCa.Width = 80; this.btnTabTatCa.Left = 15;
            this.btnTabThucPham.Text = "Thực Phẩm"; this.btnTabThucPham.Width = 110; this.btnTabThucPham.Left = 95;
            this.btnTabDienTu.Text = "Điện Tử"; this.btnTabDienTu.Width = 90; this.btnTabDienTu.Left = 205;
            this.btnTabMyPham.Text = "Mỹ Phẩm"; this.btnTabMyPham.Width = 100; this.btnTabMyPham.Left = 295;
            this.btnTabGiaDung.Text = "Gia Dụng"; this.btnTabGiaDung.Width = 100; this.btnTabGiaDung.Left = 395;
            this.btnTabThoiTrang.Text = "Thời Trang"; this.btnTabThoiTrang.Width = 110; this.btnTabThoiTrang.Left = 495;

            this.btnTabTatCa.ForeColor = Color.FromArgb(0, 136, 255);
            this.btnTabTatCa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // ==========================================
            // 5. pnlFilters (Tìm kiếm, Sắp xếp, Thao tác)
            // ==========================================
            this.pnlFilters.Dock = DockStyle.Top;
            this.pnlFilters.Height = 70;
            this.pnlFilters.BackColor = Color.White;

            this.txtTimKiem.Font = new Font("Segoe UI", 11F);
            this.txtTimKiem.ForeColor = Color.Gray;
            this.txtTimKiem.Location = new Point(15, 20);
            this.txtTimKiem.Size = new Size(250, 32);
            this.txtTimKiem.Text = "🔍 Tìm kiếm mã, tên";

            this.btnTim.BackColor = Color.FromArgb(52, 152, 219);
            this.btnTim.ForeColor = Color.White;
            this.btnTim.FlatStyle = FlatStyle.Flat;
            this.btnTim.FlatAppearance.BorderSize = 0;
            this.btnTim.Text = "Tìm";
            this.btnTim.Size = new Size(70, 32);
            this.btnTim.Location = new Point(275, 20);
            this.btnTim.Cursor = Cursors.Hand;
            this.btnTim.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            this.btnSortAsc.BackColor = Color.White;
            this.btnSortAsc.ForeColor = Color.FromArgb(64, 64, 64);
            this.btnSortAsc.FlatStyle = FlatStyle.Flat;
            this.btnSortAsc.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnSortAsc.Text = "Sắp xếp kho ⬆";
            this.btnSortAsc.Size = new Size(130, 32);
            this.btnSortAsc.Location = new Point(360, 20);
            this.btnSortAsc.Cursor = Cursors.Hand;

            this.btnSortDesc.BackColor = Color.White;
            this.btnSortDesc.ForeColor = Color.FromArgb(64, 64, 64);
            this.btnSortDesc.FlatStyle = FlatStyle.Flat;
            this.btnSortDesc.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnSortDesc.Text = "Sắp xếp kho ⬇";
            this.btnSortDesc.Size = new Size(130, 32);
            this.btnSortDesc.Location = new Point(500, 20);
            this.btnSortDesc.Cursor = Cursors.Hand;

            this.btnSua.BackColor = Color.FromArgb(243, 156, 18);
            this.btnSua.ForeColor = Color.White;
            this.btnSua.FlatStyle = FlatStyle.Flat;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.Text = "✏️ Sửa";
            this.btnSua.Size = new Size(90, 32);
            this.btnSua.Location = new Point(640, 20);
            this.btnSua.Cursor = Cursors.Hand;
            this.btnSua.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            this.btnXoa.BackColor = Color.FromArgb(231, 76, 60);
            this.btnXoa.ForeColor = Color.White;
            this.btnXoa.FlatStyle = FlatStyle.Flat;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.Size = new Size(90, 32);
            this.btnXoa.Location = new Point(740, 20);
            this.btnXoa.Cursor = Cursors.Hand;
            this.btnXoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            this.pnlFilters.Controls.AddRange(new Control[] { txtTimKiem, btnTim, btnSortAsc, btnSortDesc, btnSua, btnXoa });

            // ==========================================
            // 6. dgvData
            // ==========================================
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;

            // Khóa kéo dãn để tránh xô lệch bảng
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;

            this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.BackgroundColor = Color.White;
            this.dgvData.BorderStyle = BorderStyle.None;
            this.dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.FromArgb(249, 250, 251);
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.ForeColor = Color.FromArgb(99, 115, 129);
            headerStyle.SelectionBackColor = Color.FromArgb(249, 250, 251);
            headerStyle.Padding = new Padding(15, 10, 10, 10); // Thêm padding cho header
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvData.ColumnHeadersHeight = 50;
            this.dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = Color.White;
            cellStyle.Font = new Font("Segoe UI", 10F);
            cellStyle.ForeColor = Color.FromArgb(33, 43, 54);
            cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255);
            cellStyle.SelectionForeColor = Color.Black;
            cellStyle.Padding = new Padding(15, 0, 10, 0); // Thêm padding cho cell
            this.dgvData.DefaultCellStyle = cellStyle;

            this.dgvData.Dock = DockStyle.Fill;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = Color.FromArgb(226, 232, 240);
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 50;
            this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ==========================================
            // ucSanPham
            // ==========================================
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 246, 248);
            this.Padding = new Padding(20, 20, 20, 20); // Tạo khoảng trống xung quanh
            this.Size = new Size(1120, 740); // Size chuẩn mở rộng ra

            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlDashboard.ResumeLayout(false);
            this.card1.ResumeLayout(false); this.card1.PerformLayout();
            this.card2.ResumeLayout(false); this.card2.PerformLayout();
            this.card3.ResumeLayout(false); this.card3.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}