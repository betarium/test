namespace Betarium.Betapeta
{
    partial class MemoForm
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
            this.MemoText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MemoText
            // 
            this.MemoText.BackColor = System.Drawing.SystemColors.Window;
            this.MemoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MemoText.Location = new System.Drawing.Point(0, 0);
            this.MemoText.Multiline = true;
            this.MemoText.Name = "MemoText";
            this.MemoText.ReadOnly = true;
            this.MemoText.Size = new System.Drawing.Size(165, 45);
            this.MemoText.TabIndex = 0;
            this.MemoText.DoubleClick += new System.EventHandler(this.MemoText_DoubleClick);
            // 
            // MemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(165, 45);
            this.Controls.Add(this.MemoText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MemoForm";
            this.Load += new System.EventHandler(this.MemoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox MemoText;

    }
}