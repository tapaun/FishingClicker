namespace FishingClicker
{
    partial class PlayerLogIn
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginButton = new Button();
            signupButton = new Button();
            labelPassword = new Label();
            labelName = new Label();
            usernameTxtbox = new TextBox();
            passwordTxtbox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // loginButton
            // 
            loginButton.BackColor = Color.SteelBlue;
            loginButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loginButton.ForeColor = SystemColors.ButtonFace;
            loginButton.Location = new Point(248, 213);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(108, 32);
            loginButton.TabIndex = 0;
            loginButton.Text = "Log In";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // signupButton
            // 
            signupButton.BackColor = Color.SteelBlue;
            signupButton.FlatAppearance.BorderColor = Color.Black;
            signupButton.FlatAppearance.BorderSize = 3;
            signupButton.FlatAppearance.MouseDownBackColor = Color.Turquoise;
            signupButton.FlatAppearance.MouseOverBackColor = Color.Aqua;
            signupButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            signupButton.ForeColor = SystemColors.ButtonFace;
            signupButton.Location = new Point(362, 213);
            signupButton.Name = "signupButton";
            signupButton.Size = new Size(108, 32);
            signupButton.TabIndex = 1;
            signupButton.Text = "Sign Up";
            signupButton.UseVisualStyleBackColor = false;
            signupButton.Click += signupButton_Click;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPassword.ForeColor = SystemColors.ButtonFace;
            labelPassword.Location = new Point(178, 150);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(80, 20);
            labelPassword.TabIndex = 3;
            labelPassword.Text = "Password:";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelName.ForeColor = SystemColors.ButtonFace;
            labelName.Location = new Point(174, 116);
            labelName.Name = "labelName";
            labelName.Size = new Size(84, 20);
            labelName.TabIndex = 4;
            labelName.Text = "Username:";
            // 
            // usernameTxtbox
            // 
            usernameTxtbox.Location = new Point(264, 113);
            usernameTxtbox.Name = "usernameTxtbox";
            usernameTxtbox.Size = new Size(206, 23);
            usernameTxtbox.TabIndex = 5;
            // 
            // passwordTxtbox
            // 
            passwordTxtbox.Location = new Point(264, 147);
            passwordTxtbox.Name = "passwordTxtbox";
            passwordTxtbox.PasswordChar = '*';
            passwordTxtbox.Size = new Size(206, 23);
            passwordTxtbox.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 7F);
            label1.ForeColor = Color.Yellow;
            label1.Location = new Point(476, 121);
            label1.Name = "label1";
            label1.Size = new Size(25, 12);
            label1.TabIndex = 7;
            label1.Text = "-----";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 7F);
            label2.ForeColor = Color.Yellow;
            label2.Location = new Point(476, 154);
            label2.Name = "label2";
            label2.Size = new Size(25, 12);
            label2.TabIndex = 8;
            label2.Text = "-----";
            // 
            // PlayerLogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateBlue;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passwordTxtbox);
            Controls.Add(usernameTxtbox);
            Controls.Add(labelName);
            Controls.Add(labelPassword);
            Controls.Add(signupButton);
            Controls.Add(loginButton);
            Name = "PlayerLogIn";
            Size = new Size(723, 347);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loginButton;
        private Button signupButton;
        private Label labelPassword;
        private Label labelName;
        private TextBox usernameTxtbox;
        private TextBox passwordTxtbox;
        private Label label1;
        private Label label2;
    }
}
