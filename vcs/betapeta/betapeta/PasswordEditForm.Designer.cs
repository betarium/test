namespace Betarium.Betapeta
{
    partial class PasswordEditForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.AccountList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UrlText = new System.Windows.Forms.TextBox();
            this.CopyUrlButton = new System.Windows.Forms.Button();
            this.UserText = new System.Windows.Forms.TextBox();
            this.AccountText = new System.Windows.Forms.TextBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CopyUserButton = new System.Windows.Forms.Button();
            this.CopyPasswordButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AccountList
            // 
            this.AccountList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.AccountList.FullRowSelect = true;
            this.AccountList.Location = new System.Drawing.Point(2, 2);
            this.AccountList.Name = "AccountList";
            this.AccountList.Size = new System.Drawing.Size(284, 189);
            this.AccountList.TabIndex = 0;
            this.AccountList.UseCompatibleStateImageBehavior = false;
            this.AccountList.View = System.Windows.Forms.View.Details;
            this.AccountList.SelectedIndexChanged += new System.EventHandler(this.AccountList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Account";
            this.columnHeader1.Width = 248;
            // 
            // UrlText
            // 
            this.UrlText.Location = new System.Drawing.Point(72, 222);
            this.UrlText.Name = "UrlText";
            this.UrlText.Size = new System.Drawing.Size(214, 19);
            this.UrlText.TabIndex = 4;
            // 
            // CopyUrlButton
            // 
            this.CopyUrlButton.Location = new System.Drawing.Point(204, 247);
            this.CopyUrlButton.Name = "CopyUrlButton";
            this.CopyUrlButton.Size = new System.Drawing.Size(75, 23);
            this.CopyUrlButton.TabIndex = 6;
            this.CopyUrlButton.Text = "Copy";
            this.CopyUrlButton.UseVisualStyleBackColor = true;
            this.CopyUrlButton.Click += new System.EventHandler(this.CopyUrlButton_Click);
            // 
            // UserText
            // 
            this.UserText.Location = new System.Drawing.Point(72, 287);
            this.UserText.Name = "UserText";
            this.UserText.Size = new System.Drawing.Size(124, 19);
            this.UserText.TabIndex = 8;
            // 
            // AccountText
            // 
            this.AccountText.Location = new System.Drawing.Point(72, 197);
            this.AccountText.Name = "AccountText";
            this.AccountText.Size = new System.Drawing.Size(214, 19);
            this.AccountText.TabIndex = 2;
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(72, 312);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(124, 19);
            this.PasswordText.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "URL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password";
            // 
            // CopyUserButton
            // 
            this.CopyUserButton.Location = new System.Drawing.Point(204, 285);
            this.CopyUserButton.Name = "CopyUserButton";
            this.CopyUserButton.Size = new System.Drawing.Size(75, 23);
            this.CopyUserButton.TabIndex = 9;
            this.CopyUserButton.Text = "Copy";
            this.CopyUserButton.UseVisualStyleBackColor = true;
            this.CopyUserButton.Click += new System.EventHandler(this.CopyUserButton_Click);
            // 
            // CopyPasswordButton
            // 
            this.CopyPasswordButton.Location = new System.Drawing.Point(204, 312);
            this.CopyPasswordButton.Name = "CopyPasswordButton";
            this.CopyPasswordButton.Size = new System.Drawing.Size(75, 23);
            this.CopyPasswordButton.TabIndex = 12;
            this.CopyPasswordButton.Text = "Copy";
            this.CopyPasswordButton.UseVisualStyleBackColor = true;
            this.CopyPasswordButton.Click += new System.EventHandler(this.CopyPasswordButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(10, 337);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 13;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(123, 247);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 5;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // PasswordEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 368);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AccountText);
            this.Controls.Add(this.CopyPasswordButton);
            this.Controls.Add(this.CopyUserButton);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.CopyUrlButton);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.UserText);
            this.Controls.Add(this.UrlText);
            this.Controls.Add(this.AccountList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordEditForm";
            this.Text = "パスワード";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PasswordEditForm_FormClosing);
            this.Load += new System.EventHandler(this.PasswordEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView AccountList;
        private System.Windows.Forms.TextBox UrlText;
        private System.Windows.Forms.Button CopyUrlButton;
        private System.Windows.Forms.TextBox UserText;
        private System.Windows.Forms.TextBox AccountText;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CopyUserButton;
        private System.Windows.Forms.Button CopyPasswordButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button OpenButton;
    }
}