namespace MISUP.WinForms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Các Panel chia bố cục
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;

        // Cột Trái (Branding)
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSlogan;
        private System.Windows.Forms.Label lblVersion;

        // Cột Phải (Login Form)
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Panel pnlUserLine;
        private System.Windows.Forms.Panel pnlPassLine;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.pnlUserLine = new System.Windows.Forms.Panel();
            this.pnlPassLine = new System.Windows.Forms.Panel();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblClose = new System.Windows.Forms.Label();

            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // CỘT TRÁI (BRANDING) - Tông màu giống Sidebar MainForm
            // ==========================================
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this.pnlLeft.Controls.Add(this.lblVersion);
            this.pnlLeft.Controls.Add(this.lblSlogan);
            this.pnlLeft.Controls.Add(this.lblLogo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(300, 450);

            // lblLogo
            this.lblLogo.AutoSize = false;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 150);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(300, 50);
            this.lblLogo.Text = "MISUP ERP";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblSlogan
            this.lblSlogan.AutoSize = false;
            this.lblSlogan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSlogan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(180)))), ((int)(((byte)(190)))));
            this.lblSlogan.Location = new System.Drawing.Point(0, 200);
            this.lblSlogan.Name = "lblSlogan";
            this.lblSlogan.Size = new System.Drawing.Size(300, 60);
            this.lblSlogan.Text = "Hệ thống Quản trị\nVận hành Siêu thị";
            this.lblSlogan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblVersion
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(10, 420);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Text = "Phiên bản 2.0.1";

            // ==========================================
            // CỘT PHẢI (LOGIN FORM)
            // ==========================================
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.lblClose);
            this.pnlRight.Controls.Add(this.btnExit);
            this.pnlRight.Controls.Add(this.btnLogin);
            this.pnlRight.Controls.Add(this.chkShowPass);
            this.pnlRight.Controls.Add(this.pnlPassLine);
            this.pnlRight.Controls.Add(this.txtPass);
            this.pnlRight.Controls.Add(this.lblPass);
            this.pnlRight.Controls.Add(this.pnlUserLine);
            this.pnlRight.Controls.Add(this.txtUser);
            this.pnlRight.Controls.Add(this.lblUser);
            this.pnlRight.Controls.Add(this.lblTitle);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(300, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(400, 450);

            // lblClose (Nút X đóng góc trên)
            this.lblClose.AutoSize = true;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblClose.ForeColor = System.Drawing.Color.Gray;
            this.lblClose.Location = new System.Drawing.Point(365, 10);
            this.lblClose.Name = "lblClose";
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.btnExit_Click); // Tái sử dụng sự kiện thoát
            this.lblClose.MouseEnter += (s, e) => { lblClose.ForeColor = System.Drawing.Color.Red; };
            this.lblClose.MouseLeave += (s, e) => { lblClose.ForeColor = System.Drawing.Color.Gray; };

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.lblTitle.Location = new System.Drawing.Point(40, 60);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "ĐĂNG NHẬP";

            // --- Tài khoản ---
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.Gray;
            this.lblUser.Location = new System.Drawing.Point(40, 130);
            this.lblUser.Name = "lblUser";
            this.lblUser.Text = "Tên đăng nhập";

            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.txtUser.Location = new System.Drawing.Point(45, 160);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(300, 27);

            this.pnlUserLine.BackColor = System.Drawing.Color.LightGray;
            this.pnlUserLine.Location = new System.Drawing.Point(45, 190);
            this.pnlUserLine.Name = "pnlUserLine";
            this.pnlUserLine.Size = new System.Drawing.Size(300, 2);

            // Sự kiện đổi màu viền khi Focus
            this.txtUser.Enter += (s, e) => { pnlUserLine.BackColor = System.Drawing.Color.FromArgb(0, 136, 255); };
            this.txtUser.Leave += (s, e) => { pnlUserLine.BackColor = System.Drawing.Color.LightGray; };

            // --- Mật khẩu ---
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPass.ForeColor = System.Drawing.Color.Gray;
            this.lblPass.Location = new System.Drawing.Point(40, 210);
            this.lblPass.Name = "lblPass";
            this.lblPass.Text = "Mật khẩu";

            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(56)))), ((int)(((byte)(70)))));
            this.txtPass.Location = new System.Drawing.Point(45, 240);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(300, 27);
            this.txtPass.UseSystemPasswordChar = true;

            this.pnlPassLine.BackColor = System.Drawing.Color.LightGray;
            this.pnlPassLine.Location = new System.Drawing.Point(45, 270);
            this.pnlPassLine.Name = "pnlPassLine";
            this.pnlPassLine.Size = new System.Drawing.Size(300, 2);

            // Sự kiện đổi màu viền khi Focus
            this.txtPass.Enter += (s, e) => { pnlPassLine.BackColor = System.Drawing.Color.FromArgb(0, 136, 255); };
            this.txtPass.Leave += (s, e) => { pnlPassLine.BackColor = System.Drawing.Color.LightGray; };

            // --- Tùy chọn ---
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkShowPass.ForeColor = System.Drawing.Color.Gray;
            this.chkShowPass.Location = new System.Drawing.Point(45, 285);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Text = "Hiển thị mật khẩu";
            this.chkShowPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);

            // --- Nút bấm ---
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(136)))), ((int)(((byte)(255))))); // Xanh Sapo
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(45, 335);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(140, 45);
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Gray;
            this.btnExit.Location = new System.Drawing.Point(205, 335);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(140, 45);
            this.btnExit.Text = "Thoát";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // ==========================================
            // THIẾT LẬP FORM CHÍNH
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // Bỏ viền Form
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập MISUP ERP";

            // Xử lý kéo Form khi không có viền (Click giữ vào Panel trái hoặc phải để kéo)
            this.pnlLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.pnlRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);

            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}