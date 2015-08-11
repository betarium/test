namespace LogViewer
{
    partial class LogForm
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
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(806, 424);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.WordWrap = false;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 10000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // LogForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 424);
            this.Controls.Add(this.logTextBox);
            this.Name = "LogForm";
            this.Text = "ログビューア";
            this.Load += new System.EventHandler(this.LogForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.LogForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.LogForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Timer refreshTimer;
    }
}

