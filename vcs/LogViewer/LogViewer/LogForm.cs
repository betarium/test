using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LogViewer
{
    public partial class LogForm : Form
    {
        public string LogFile { get; set; }
        public DateTime LogFileModified { get; set; }
        public string DefaultTitle { get; set; }

        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            DefaultTitle = Text;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length >= 2)
            {
                LoadFile(args[1]);
                Visible = true;
                logTextBox.ScrollToCaret();
            }
        }

        protected void LoadFile(string file)
        {
            try
            {
                //                string text = File.ReadAllText(file, Encoding.Default);

                using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (TextReader reader = new StreamReader(stream, Encoding.Default))
                    {
                        string text = reader.ReadToEnd();
                        logTextBox.Text = text;
                    }
                }

                logTextBox.SelectionStart = logTextBox.Text.Length;
                logTextBox.ScrollToCaret();
                LogFileModified = File.GetLastWriteTime(file);
                LogFile = file;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
                if (LogFile == file)
                {
                    LogFile = null;
                }
                return;
            }
            Text = Path.GetFileName(file) + " - " + LogFileModified.ToString("yyyy/MM/dd HH:mm:ss") + " - " + DefaultTitle;
            refreshTimer.Start();
        }

        private void LogForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void LogForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                string path = drags[0];
                e.Effect = DragDropEffects.Copy;
                LoadFile(path);
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (LogFile != null)
            {
                if (!File.Exists(LogFile))
                {
                    return;
                }
                DateTime modified = File.GetLastWriteTime(LogFile);
                if (modified != LogFileModified)
                {
                    LoadFile(LogFile);
                }
            }
        }

    }
}
