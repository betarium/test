namespace SQLServerBackupManager
{
    partial class MainForm
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
            this.databaseListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BackupButton = new System.Windows.Forms.Button();
            this.ServerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backupFolderText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // databaseListView
            // 
            this.databaseListView.CheckBoxes = true;
            this.databaseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.databaseListView.FullRowSelect = true;
            this.databaseListView.Location = new System.Drawing.Point(12, 30);
            this.databaseListView.Name = "databaseListView";
            this.databaseListView.Size = new System.Drawing.Size(570, 258);
            this.databaseListView.TabIndex = 0;
            this.databaseListView.UseCompatibleStateImageBehavior = false;
            this.databaseListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Database";
            this.columnHeader1.Width = 407;
            // 
            // BackupButton
            // 
            this.BackupButton.Location = new System.Drawing.Point(507, 326);
            this.BackupButton.Name = "BackupButton";
            this.BackupButton.Size = new System.Drawing.Size(75, 23);
            this.BackupButton.TabIndex = 1;
            this.BackupButton.Text = "Backup";
            this.BackupButton.UseVisualStyleBackColor = true;
            this.BackupButton.Click += new System.EventHandler(this.BackupButton_Click);
            // 
            // ServerName
            // 
            this.ServerName.AutoSize = true;
            this.ServerName.Location = new System.Drawing.Point(12, 9);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(67, 12);
            this.ServerName.TabIndex = 2;
            this.ServerName.Text = "ServerName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Backup Folder";
            // 
            // backupFolderText
            // 
            this.backupFolderText.Location = new System.Drawing.Point(111, 300);
            this.backupFolderText.Name = "backupFolderText";
            this.backupFolderText.Size = new System.Drawing.Size(471, 19);
            this.backupFolderText.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 361);
            this.Controls.Add(this.backupFolderText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.BackupButton);
            this.Controls.Add(this.databaseListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "SQL Server Backup Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView databaseListView;
        private System.Windows.Forms.Button BackupButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label ServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox backupFolderText;
    }
}

