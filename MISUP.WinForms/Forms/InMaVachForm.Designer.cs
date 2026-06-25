using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    partial class InMaVachForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Panel pnlBarcode;
        private Label lblMaPhieu;
        private Label lblInfo;
        private Button btnPrint;
        private Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.pnlBarcode = new Panel();
            this.lblMaPhieu = new Label();
            this.lblInfo = new Label();
            this.btnPrint = new Button();
            this.btnClose = new Button();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "In Mã Vạch Phiếu Nhập Kho";

            // pnlBarcode (Khu vực vẽ mã vạch)
            this.pnlBarcode.BackColor = Color.White;
            this.pnlBarcode.BorderStyle = BorderStyle.FixedSingle;
            this.pnlBarcode.Location = new Point(20, 70);
            this.pnlBarcode.Size = new Size(350, 100);
            this.pnlBarcode.Paint += new PaintEventHandler(this.pnlBarcode_Paint);

            // lblMaPhieu
            this.lblMaPhieu.AutoSize = false;
            this.lblMaPhieu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblMaPhieu.Location = new Point(20, 180);
            this.lblMaPhieu.Size = new Size(350, 30);
            this.lblMaPhieu.TextAlign = ContentAlignment.MiddleCenter;
            this.lblMaPhieu.Text = "PON0000";

            // lblInfo
            this.lblInfo.AutoSize = false;
            this.lblInfo.Font = new Font("Segoe UI", 10F);
            this.lblInfo.ForeColor = Color.Gray;
            this.lblInfo.Location = new Point(20, 220);
            this.lblInfo.Size = new Size(350, 45);
            this.lblInfo.TextAlign = ContentAlignment.MiddleCenter;

            // btnPrint
            this.btnPrint.BackColor = Color.FromArgb(0, 136, 255);
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.Location = new Point(200, 280);
            this.btnPrint.Size = new Size(130, 40);
            this.btnPrint.Text = "🖨️ Xác nhận in";
            this.btnPrint.Cursor = Cursors.Hand;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            // btnClose
            this.btnClose.BackColor = Color.White;
            this.btnClose.FlatAppearance.BorderColor = Color.LightGray;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClose.Location = new Point(100, 280);
            this.btnClose.Size = new Size(90, 40);
            this.btnClose.Text = "Đóng";
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Click += (s, e) => this.Close();

            // Form Properties
            this.BackColor = Color.White;
            this.ClientSize = new Size(390, 340);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlBarcode);
            this.Controls.Add(this.lblMaPhieu);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Mã Vạch";
            this.Load += new System.EventHandler(this.InMaVachForm_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}