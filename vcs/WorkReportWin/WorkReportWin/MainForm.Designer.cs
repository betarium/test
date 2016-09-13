namespace WorkReportWin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.beginDateInput = new System.Windows.Forms.TextBox();
            this.endDateInput = new System.Windows.Forms.TextBox();
            this.beginButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.workDateCtrl = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.startupCtrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.shutdownCtrl = new System.Windows.Forms.TextBox();
            this.beginButtonEdit = new System.Windows.Forms.Button();
            this.endButtonEdit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.beginDateField = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.endDateField = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "始業時間";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "終業時間";
            // 
            // beginDateInput
            // 
            this.beginDateInput.Location = new System.Drawing.Point(66, 62);
            this.beginDateInput.Name = "beginDateInput";
            this.beginDateInput.Size = new System.Drawing.Size(72, 19);
            this.beginDateInput.TabIndex = 1;
            // 
            // endDateInput
            // 
            this.endDateInput.Location = new System.Drawing.Point(66, 94);
            this.endDateInput.Name = "endDateInput";
            this.endDateInput.Size = new System.Drawing.Size(72, 19);
            this.endDateInput.TabIndex = 4;
            // 
            // beginButton
            // 
            this.beginButton.Location = new System.Drawing.Point(147, 60);
            this.beginButton.Name = "beginButton";
            this.beginButton.Size = new System.Drawing.Size(75, 23);
            this.beginButton.TabIndex = 2;
            this.beginButton.Text = "始業";
            this.beginButton.UseVisualStyleBackColor = true;
            this.beginButton.Click += new System.EventHandler(this.beginButton_Click);
            // 
            // endButton
            // 
            this.endButton.Location = new System.Drawing.Point(147, 92);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(75, 23);
            this.endButton.TabIndex = 5;
            this.endButton.Text = "終業";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // workDateCtrl
            // 
            this.workDateCtrl.CustomFormat = "yyyy-MM-dd (ddd)";
            this.workDateCtrl.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.workDateCtrl.Location = new System.Drawing.Point(66, 32);
            this.workDateCtrl.Name = "workDateCtrl";
            this.workDateCtrl.Size = new System.Drawing.Size(156, 19);
            this.workDateCtrl.TabIndex = 0;
            this.workDateCtrl.Value = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.workDateCtrl.ValueChanged += new System.EventHandler(this.workDateCtrl_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "勤務日";
            // 
            // TrayIcon
            // 
            this.TrayIcon.Text = "勤怠";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "起動時間";
            // 
            // startupCtrl
            // 
            this.startupCtrl.Location = new System.Drawing.Point(66, 180);
            this.startupCtrl.Name = "startupCtrl";
            this.startupCtrl.ReadOnly = true;
            this.startupCtrl.Size = new System.Drawing.Size(100, 19);
            this.startupCtrl.TabIndex = 10;
            this.startupCtrl.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "終了時間";
            // 
            // shutdownCtrl
            // 
            this.shutdownCtrl.Location = new System.Drawing.Point(66, 205);
            this.shutdownCtrl.Name = "shutdownCtrl";
            this.shutdownCtrl.ReadOnly = true;
            this.shutdownCtrl.Size = new System.Drawing.Size(100, 19);
            this.shutdownCtrl.TabIndex = 10;
            this.shutdownCtrl.TabStop = false;
            // 
            // beginButtonEdit
            // 
            this.beginButtonEdit.Location = new System.Drawing.Point(228, 60);
            this.beginButtonEdit.Name = "beginButtonEdit";
            this.beginButtonEdit.Size = new System.Drawing.Size(75, 23);
            this.beginButtonEdit.TabIndex = 3;
            this.beginButtonEdit.Text = "修正";
            this.beginButtonEdit.UseVisualStyleBackColor = true;
            this.beginButtonEdit.Click += new System.EventHandler(this.beginButtonEdit_Click);
            // 
            // endButtonEdit
            // 
            this.endButtonEdit.Location = new System.Drawing.Point(228, 92);
            this.endButtonEdit.Name = "endButtonEdit";
            this.endButtonEdit.Size = new System.Drawing.Size(75, 23);
            this.endButtonEdit.TabIndex = 6;
            this.endButtonEdit.Text = "修正";
            this.endButtonEdit.UseVisualStyleBackColor = true;
            this.endButtonEdit.Click += new System.EventHandler(this.endButtonEdit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "始業時間";
            // 
            // beginDateField
            // 
            this.beginDateField.Location = new System.Drawing.Point(66, 130);
            this.beginDateField.Name = "beginDateField";
            this.beginDateField.ReadOnly = true;
            this.beginDateField.Size = new System.Drawing.Size(100, 19);
            this.beginDateField.TabIndex = 10;
            this.beginDateField.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "終業時間";
            // 
            // endDateField
            // 
            this.endDateField.Location = new System.Drawing.Point(66, 155);
            this.endDateField.Name = "endDateField";
            this.endDateField.ReadOnly = true;
            this.endDateField.Size = new System.Drawing.Size(100, 19);
            this.endDateField.TabIndex = 10;
            this.endDateField.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(315, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.applicationToolStripMenuItem.Text = "アプリケーション(&A)";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exportToolStripMenuItem.Text = "エクスポート(&E)";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.quitToolStripMenuItem.Text = "終了(&Q)";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 228);
            this.Controls.Add(this.shutdownCtrl);
            this.Controls.Add(this.endDateField);
            this.Controls.Add(this.beginDateField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.startupCtrl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.workDateCtrl);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.endButtonEdit);
            this.Controls.Add(this.beginButtonEdit);
            this.Controls.Add(this.beginButton);
            this.Controls.Add(this.endDateInput);
            this.Controls.Add(this.beginDateInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "勤怠";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox beginDateInput;
        private System.Windows.Forms.TextBox endDateInput;
        private System.Windows.Forms.Button beginButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.DateTimePicker workDateCtrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox startupCtrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox shutdownCtrl;
        private System.Windows.Forms.Button beginButtonEdit;
        private System.Windows.Forms.Button endButtonEdit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox beginDateField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox endDateField;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;

    }
}

