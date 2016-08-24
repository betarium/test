using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Betarium.SiteCrawler.Properties;
using System.IO;

namespace Betarium.SiteCrawler
{
    public partial class SiteCrawlerForm : Form
    {
        public SiteCrawlerForm()
        {
            InitializeComponent();
        }

        private void SiteCrawlerForm_Load(object sender, EventArgs e)
        {
            TargetUrlText.Text = Settings.Default.TargetUrl;
        }

        private void SiteCrawlerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.TargetUrl = TargetUrlText.Text;
            Settings.Default.Save();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            string targetUrl = TargetUrlText.Text;
            if (string.IsNullOrEmpty(targetUrl))
            {
                MessageBox.Show("アドレスを入力してください。");
                return;
            }

            Uri uri = null;
            if (!Uri.TryCreate(targetUrl, UriKind.Absolute, out uri))
            {
                MessageBox.Show("アドレスの形式に誤りがあります。");
                return;
            }

            string cacheDir = Path.GetTempPath();
            cacheDir = Path.Combine(cacheDir, Application.ProductName, uri.Host);
            if (!Directory.Exists(cacheDir))
            {
                Directory.CreateDirectory(cacheDir);
            }

            SiteCrawlerService service = new SiteCrawlerService();
            service.CacheDirectory = cacheDir;
            service.MaxFollow = Settings.Default.MaxFollow;
            Dictionary<string, SiteCrawlerService.SiteInfo> downloadCache = new Dictionary<string, SiteCrawlerService.SiteInfo>();
            service.DownloadAll(targetUrl, downloadCache);

            StringBuilder buffer = new StringBuilder();
            foreach (var site in downloadCache.Values)
            {
                buffer.AppendLine(site.Path + "\t" + site.Title + "\t" + (int)site.ErrorCode);
            }
            string path = Path.Combine(Application.StartupPath, "site.txt");
            File.WriteAllText(path, buffer.ToString(), new UTF8Encoding(true));

            downloadCache.Clear();

            MessageBox.Show("完了しました。");
        }
    }
}
