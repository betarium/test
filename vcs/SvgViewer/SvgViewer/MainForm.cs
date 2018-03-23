using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvgViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public string BackgroundColor { get; set; }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BackgroundColor = Properties.Settings.Default.BackgroundColor;

            AllowDrop = true;
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                string path = Environment.GetCommandLineArgs()[1];
                LoadImage(path);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadImage(files[0]);
        }

        protected void LoadImage(string path)
        {
            var html = "<html><head><meta http-equiv='X-UA-Compatible' content='IE=10' /></head><body style='background-color: " + BackgroundColor + "'><img src='" + path + "'></body></html>";
            Browser.DocumentText = html;
        }

        protected void LoadImageSvg(string svg)
        {
            var html = "<html><head><meta http-equiv='X-UA-Compatible' content='IE=10' /></head><body style='background-color: " + BackgroundColor + "'>" + svg + "</body></html>";
            Browser.DocumentText = html;
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Browser.DocumentText.StartsWith("<svg"))
            {
                LoadImageSvg(Browser.DocumentText);
            }
        }
    }
}
