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

        private Panel pnlDashboard;
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

            this.pnlDashboard = new Panel();
            this.card1 = new Panel(); this.lblTongSPTitle = new Label(); this.lblTongSP = new Label();
            this.card2 = new Panel(); this.lblTongGiaTriTitle = new Label(); this.lblTongGiaTri = new Label();
            this.card3 = new Panel(); this.lblCanhBaoTonTitle = new Label(); this.lblCanhBaoTon = new Label();

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
            this.card1.SuspendLayout(); this.card2.SuspendLayout(); this.card3.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();

            // 1. pnlHeader
            this.pnlHeader.BackColor = Color.Transparent;
            this.pnlHeader.Controls.Add(this.btnXuatExcel);
            this.pnlHeader.Controls.Add(this.btnThem);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 60;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Text = "Quản Lý Sản Phẩm";

            this.btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnThem.BackColor = Color.FromArgb(0, 136, 255);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = FlatStyle.Flat;
            this.btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnThem.ForeColor = Color.White;
            this.btnThem.Location = new Point(980, 12);
            this.btnThem.Size = new Size(120, 36);
            this.btnThem.Text = "➕ Thêm mới";
            this.btnThem.Cursor = Cursors.Hand;

            this.btnXuatExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnXuatExcel.BackColor = Color.White;
            this.btnXuatExcel.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnXuatExcel.FlatStyle = FlatStyle.Flat;
            this.btnXuatExcel.Font = new Font("Segoe UI", 10F);
            this.btnXuatExcel.ForeColor = Color.FromArgb(80, 80, 80);
            this.btnXuatExcel.Location = new Point(865, 12);
            this.btnXuatExcel.Size = new Size(100, 36);
            this.btnXuatExcel.Text = "📥 Xuất file";
            this.btnXuatExcel.Cursor = Cursors.Hand;

            // 2. pnlDashboard (Thống kê)
            this.pnlDashboard.Dock = DockStyle.Top;
            this.pnlDashboard.Height = 90;
            this.pnlDashboard.Padding = new Padding(20, 0, 20, 10);
            this.pnlDashboard.Controls.Add(card3);
            this.pnlDashboard.Controls.Add(card2);
            this.pnlDashboard.Controls.Add(card1);

            // Card 1
            this.card1.BackColor = Color.FromArgb(52, 152, 219);
            this.card1.Size = new Size(240, 70);
            this.card1.Location = new Point(20, 5);
            this.lblTongSPTitle.Text = "📦 TỔNG MẶT HÀNG"; this.lblTongSPTitle.ForeColor = Color.White; this.lblTongSPTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.lblTongSPTitle.Location = new Point(10, 10); this.lblTongSPTitle.AutoSize = true;
            this.lblTongSP.Text = "0"; this.lblTongSP.ForeColor = Color.White; this.lblTongSP.Font = new Font("Segoe UI", 16F, FontStyle.Bold); this.lblTongSP.Location = new Point(10, 30); this.lblTongSP.AutoSize = true;
            this.card1.Controls.Add(lblTongSPTitle); this.card1.Controls.Add(lblTongSP);

            // Card 2
            this.card2.BackColor = Color.FromArgb(46, 204, 113);
            this.card2.Size = new Size(240, 70);
            this.card2.Location = new Point(280, 5);
            this.lblTongGiaTriTitle.Text = "💰 TỔNG GIÁ TRỊ KHO"; this.lblTongGiaTriTitle.ForeColor = Color.White; this.lblTongGiaTriTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.lblTongGiaTriTitle.Location = new Point(10, 10); this.lblTongGiaTriTitle.AutoSize = true;
            this.lblTongGiaTri.Text = "0 đ"; this.lblTongGiaTri.ForeColor = Color.White; this.lblTongGiaTri.Font = new Font("Segoe UI", 16F, FontStyle.Bold); this.lblTongGiaTri.Location = new Point(10, 30); this.lblTongGiaTri.AutoSize = true;
            this.card2.Controls.Add(lblTongGiaTriTitle); this.card2.Controls.Add(lblTongGiaTri);

            // Card 3
            this.card3.BackColor = Color.FromArgb(231, 76, 60);
            this.card3.Size = new Size(240, 70);
            this.card3.Location = new Point(540, 5);
            this.lblCanhBaoTonTitle.Text = "⚠️ SẮP HẾT HÀNG (<10)"; this.lblCanhBaoTonTitle.ForeColor = Color.White; this.lblCanhBaoTonTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold); this.lblCanhBaoTonTitle.Location = new Point(10, 10); this.lblCanhBaoTonTitle.AutoSize = true;
            this.lblCanhBaoTon.Text = "0 SP"; this.lblCanhBaoTon.ForeColor = Color.White; this.lblCanhBaoTon.Font = new Font("Segoe UI", 16F, FontStyle.Bold); this.lblCanhBaoTon.Location = new Point(10, 30); this.lblCanhBaoTon.AutoSize = true;
            this.card3.Controls.Add(lblCanhBaoTonTitle); this.card3.Controls.Add(lblCanhBaoTon);

            // 3. pnlCard (Khung trắng chứa Data)
            this.pnlCard.BackColor = Color.White;
            this.pnlCard.Dock = DockStyle.Fill;
            this.pnlCard.Padding = new Padding(1);
            this.pnlCard.Controls.Add(this.dgvData);
            this.pnlCard.Controls.Add(this.pnlFilters);
            this.pnlCard.Controls.Add(this.pnlTabs);

            // 4. pnlTabs
            this.pnlTabs.Dock = DockStyle.Top;
            this.pnlTabs.Height = 50;
            this.pnlTabs.BackColor = Color.White;
            this.pnlTabs.Controls.AddRange(new Control[] { btnTabTatCa, btnTabThucPham, btnTabDienTu, btnTabMyPham, btnTabGiaDung, btnTabThoiTrang, pnlTabLine });

            this.pnlTabLine.BackColor = Color.FromArgb(226, 232, 240);
            this.pnlTabLine.Dock = DockStyle.Bottom;
            this.pnlTabLine.Height = 1;

            Button[] tabs = { btnTabTatCa, btnTabThucPham, btnTabDienTu, btnTabMyPham, btnTabGiaDung, btnTabThoiTrang };
            int tabX = 20;
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
            this.btnTabTatCa.Text = "Tất cả"; this.btnTabTatCa.Width = 80; this.btnTabTatCa.Left = 20;
            this.btnTabThucPham.Text = "Thực Phẩm"; this.btnTabThucPham.Width = 110; this.btnTabThucPham.Left = 100;
            this.btnTabDienTu.Text = "Điện Tử"; this.btnTabDienTu.Width = 90; this.btnTabDienTu.Left = 210;
            this.btnTabMyPham.Text = "Mỹ Phẩm"; this.btnTabMyPham.Width = 100; this.btnTabMyPham.Left = 300;
            this.btnTabGiaDung.Text = "Gia Dụng"; this.btnTabGiaDung.Width = 100; this.btnTabGiaDung.Left = 400;
            this.btnTabThoiTrang.Text = "Thời Trang"; this.btnTabThoiTrang.Width = 110; this.btnTabThoiTrang.Left = 500;

            this.btnTabTatCa.ForeColor = Color.FromArgb(0, 136, 255);
            this.btnTabTatCa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // 5. pnlFilters (Tìm kiếm, Sắp xếp, Thao tác)
            this.pnlFilters.Dock = DockStyle.Top;
            this.pnlFilters.Height = 60;
            this.pnlFilters.BackColor = Color.White;

            this.txtTimKiem.Font = new Font("Segoe UI", 11F);
            this.txtTimKiem.ForeColor = Color.Gray;
            this.txtTimKiem.Location = new Point(20, 15);
            this.txtTimKiem.Size = new Size(250, 32);
            this.txtTimKiem.Text = "🔍 Tìm kiếm mã, tên";

            this.btnTim.BackColor = Color.FromArgb(52, 152, 219); this.btnTim.ForeColor = Color.White; this.btnTim.FlatStyle = FlatStyle.Flat; this.btnTim.FlatAppearance.BorderSize = 0;
            this.btnTim.Text = "Tìm"; this.btnTim.Size = new Size(60, 32); this.btnTim.Location = new Point(280, 15); this.btnTim.Cursor = Cursors.Hand;

            this.btnSortAsc.BackColor = Color.White; this.btnSortAsc.ForeColor = Color.FromArgb(64, 64, 64); this.btnSortAsc.FlatStyle = FlatStyle.Flat; this.btnSortAsc.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnSortAsc.Text = "Sắp xếp kho ⬆"; this.btnSortAsc.Size = new Size(130, 32); this.btnSortAsc.Location = new Point(350, 15); this.btnSortAsc.Cursor = Cursors.Hand;

            this.btnSortDesc.BackColor = Color.White; this.btnSortDesc.ForeColor = Color.FromArgb(64, 64, 64); this.btnSortDesc.FlatStyle = FlatStyle.Flat; this.btnSortDesc.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            this.btnSortDesc.Text = "Sắp xếp kho ⬇"; this.btnSortDesc.Size = new Size(130, 32); this.btnSortDesc.Location = new Point(490, 15); this.btnSortDesc.Cursor = Cursors.Hand;

            this.btnSua.BackColor = Color.FromArgb(243, 156, 18); this.btnSua.ForeColor = Color.White; this.btnSua.FlatStyle = FlatStyle.Flat; this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.Text = "✏️ Sửa"; this.btnSua.Size = new Size(80, 32); this.btnSua.Location = new Point(630, 15); this.btnSua.Cursor = Cursors.Hand;

            this.btnXoa.BackColor = Color.FromArgb(231, 76, 60); this.btnXoa.ForeColor = Color.White; this.btnXoa.FlatStyle = FlatStyle.Flat; this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.Text = "🗑️ Xóa"; this.btnXoa.Size = new Size(80, 32); this.btnXoa.Location = new Point(720, 15); this.btnXoa.Cursor = Cursors.Hand;

            this.pnlFilters.Controls.AddRange(new Control[] { txtTimKiem, btnTim, btnSortAsc, btnSortDesc, btnSua, btnXoa });

            // 6. dgvData
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
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
            this.dgvData.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvData.ColumnHeadersHeight = 50;

            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = Color.White;
            cellStyle.Font = new Font("Segoe UI", 10F);
            cellStyle.ForeColor = Color.FromArgb(33, 43, 54);
            cellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255);
            cellStyle.SelectionForeColor = Color.Black;
            this.dgvData.DefaultCellStyle = cellStyle;

            this.dgvData.Dock = DockStyle.Fill;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = Color.FromArgb(226, 232, 240);
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 50;
            this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ucSanPham
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 246, 248);
            this.Padding = new Padding(20, 0, 20, 20);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.pnlHeader);
            this.Size = new Size(1120, 740);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlDashboard.ResumeLayout(false);
            this.card1.ResumeLayout(false); this.card2.ResumeLayout(false); this.card3.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}