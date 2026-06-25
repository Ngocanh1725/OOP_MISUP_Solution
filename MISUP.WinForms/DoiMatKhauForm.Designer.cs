using System.Drawing;
using System.Windows.Forms;

namespace MISUP.WinForms.Forms
{
    partial class DoiMatKhauForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle, lblOld, lblNew, lblConfirm;
        private TextBox txtOld, txtNew, txtConfirm;
        private Button btnSave, btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblOld = new Label();
            this.txtOld = new TextBox();
            this.lblNew = new Label();
            this.txtNew = new TextBox();
            this.lblConfirm = new Label();
            this.txtConfirm = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Font định dạng chung
            Font labelFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            Font inputFont = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 56, 70);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Đổi mật khẩu cá nhân";

            // Mật khẩu cũ
            this.lblOld.AutoSize = true;
            this.lblOld.Font = labelFont;
            this.lblOld.Location = new Point(25, 80);
            this.lblOld.Text = "Mật khẩu hiện tại (*)";

            this.txtOld.Font = inputFont;
            this.txtOld.Location = new Point(25, 105);
            this.txtOld.Size = new Size(330, 32);
            this.txtOld.UseSystemPasswordChar = true;

            // Mật khẩu mới
            this.lblNew.AutoSize = true;
            this.lblNew.Font = labelFont;
            this.lblNew.Location = new Point(25, 150);
            this.lblNew.Text = "Mật khẩu mới (Tối thiểu 6 ký tự) (*)";

            this.txtNew.Font = inputFont;
            this.txtNew.Location = new Point(25, 175);
            this.txtNew.Size = new Size(330, 32);
            this.txtNew.UseSystemPasswordChar = true;

            // Xác nhận mật khẩu mới
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = labelFont;
            this.lblConfirm.Location = new Point(25, 220);
            this.lblConfirm.Text = "Nhập lại mật khẩu mới (*)";

            this.txtConfirm.Font = inputFont;
            this.txtConfirm.Location = new Point(25, 245);
            this.txtConfirm.Size = new Size(330, 32);
            this.txtConfirm.UseSystemPasswordChar = true;

            // Buttons
            this.btnSave.BackColor = Color.FromArgb(0, 136, 255);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(225, 310);
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu thay đổi";
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.btnCancel.BackColor = Color.White;
            this.btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.Location = new Point(125, 310);
            this.btnCancel.Size = new Size(90, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += (s, e) => this.Close();

            // Form Properties
            this.BackColor = Color.White;
            this.ClientSize = new Size(390, 380);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblOld); this.Controls.Add(this.txtOld);
            this.Controls.Add(this.lblNew); this.Controls.Add(this.txtNew);
            this.Controls.Add(this.lblConfirm); this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.btnSave); this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Bảo mật tài khoản";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}