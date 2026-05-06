namespace MISUP.WinForms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle, lblUser, lblPass;
        private System.Windows.Forms.TextBox txtUser, txtPass;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.Button btnLogin, btnExit;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label(); this.lblUser = new System.Windows.Forms.Label(); this.txtUser = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label(); this.txtPass = new System.Windows.Forms.TextBox(); this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button(); this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblTitle.AutoSize = false; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold); this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(51)))), ((int)(((byte)(153))))); this.lblTitle.Location = new System.Drawing.Point(0, 20); this.lblTitle.Name = "lblTitle"; this.lblTitle.Size = new System.Drawing.Size(450, 50); this.lblTitle.Text = "Welcome"; this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUser.AutoSize = true; this.lblUser.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.lblUser.Location = new System.Drawing.Point(50, 100); this.lblUser.Name = "lblUser"; this.lblUser.Size = new System.Drawing.Size(100, 25); this.lblUser.Text = "Tài Khoản:";
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 12F); this.txtUser.Location = new System.Drawing.Point(150, 95); this.txtUser.Name = "txtUser"; this.txtUser.Size = new System.Drawing.Size(200, 34);
            this.lblPass.AutoSize = true; this.lblPass.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.lblPass.Location = new System.Drawing.Point(50, 145); this.lblPass.Name = "lblPass"; this.lblPass.Size = new System.Drawing.Size(100, 25); this.lblPass.Text = "Mật khẩu:";
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 12F); this.txtPass.Location = new System.Drawing.Point(150, 140); this.txtPass.Name = "txtPass"; this.txtPass.Size = new System.Drawing.Size(200, 34); this.txtPass.UseSystemPasswordChar = true;
            this.chkShowPass.AutoSize = true; this.chkShowPass.Font = new System.Drawing.Font("Segoe UI", 9F); this.chkShowPass.Location = new System.Drawing.Point(150, 180); this.chkShowPass.Name = "chkShowPass"; this.chkShowPass.Size = new System.Drawing.Size(150, 24); this.chkShowPass.Text = "Hiển thị mật khẩu"; this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243))))); this.btnLogin.FlatAppearance.BorderSize = 0; this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.btnLogin.ForeColor = System.Drawing.Color.White; this.btnLogin.Location = new System.Drawing.Point(120, 220); this.btnLogin.Name = "btnLogin"; this.btnLogin.Size = new System.Drawing.Size(200, 40); this.btnLogin.Text = "Đăng nhập"; this.btnLogin.UseVisualStyleBackColor = false; this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand; this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))); this.btnExit.FlatAppearance.BorderSize = 0; this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.btnExit.ForeColor = System.Drawing.Color.White; this.btnExit.Location = new System.Drawing.Point(120, 270); this.btnExit.Name = "btnExit"; this.btnExit.Size = new System.Drawing.Size(200, 40); this.btnExit.Text = "Thoát"; this.btnExit.UseVisualStyleBackColor = false; this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand; this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(230))))); this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.btnExit); this.Controls.Add(this.btnLogin); this.Controls.Add(this.chkShowPass); this.Controls.Add(this.txtPass); this.Controls.Add(this.lblPass); this.Controls.Add(this.txtUser); this.Controls.Add(this.lblUser); this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Đăng nhập hệ thống";
            this.ResumeLayout(false); this.PerformLayout();
        }
    }
}
