using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WorkReportWin
{
    public partial class MainForm : Form
    {
        private string ConfigDir;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TrayIcon.Icon = Icon;

            string documentFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ConfigDir = Path.Combine(documentFolder, Application.ProductName);

            workDateCtrl.Value = DateTime.Today;

            SaveData(SaveDataMode.Startup);

            /*
            if (beginDateInput.Text == "")
            {
                beginDateInput.Text = DateTime.Now.ToString("HH:mm");
            }
            else
            {
                beginButton.Enabled = false;
                if (endDateInput.Text == "")
                {
                    endDateInput.Text = DateTime.Now.ToString("HH:mm");
                }
                else
                {
                    endButton.Enabled = false;
                }
                if (WindowState == FormWindowState.Minimized)
                {
                    Visible = false;
                }
            }
             * */
            if (beginDateField.Text != "")
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    Visible = false;
                }
            }
        }

        private void beginButton_Click(object sender, EventArgs e)
        {
            SaveData(SaveDataMode.Begin);
            LoadData();
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            SaveData(SaveDataMode.End);
            LoadData();
        }

        private bool LoadData()
        {
            beginDateField.Text = "";
            endDateField.Text = "";
            startupCtrl.Text = "";
            shutdownCtrl.Text = "";
            beginDateInput.Text = "";
            endDateInput.Text = "";
            beginButton.Enabled = true;
            endButton.Enabled = true;

            string targetMonth = workDateCtrl.Value.ToString("yyyy-MM");
            string targetDate = workDateCtrl.Value.ToString("yyyy-MM-dd");
            string fileName = "WorkReport_" + targetMonth + ".xml";
            string filePath = Path.Combine(ConfigDir, fileName);
            if (!File.Exists(filePath))
            {
                LoadDataUpdateField();
                return false;
            }

            var document = LoadDocument(filePath);
            if (document == null)
            {
                LoadDataUpdateField();
                return false;
            }

            var rootFolder = document.SelectSingleNode("WorkReport/WorkDataList");
            if (rootFolder == null)
            {
                LoadDataUpdateField();
                return false;
            }

            XmlElement targetNode = FindData(document, targetDate, false);

            if (targetNode == null)
            {
                LoadDataUpdateField();
                return false;
            }

            string workDateStr = targetNode.GetAttribute("date");
            DateTime workDate = DateTime.ParseExact(workDateStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None);
            workDateCtrl.Value = workDate;
            //beginDateInput.Text = targetNode.GetAttribute("begin");
            //endDateInput.Text = targetNode.GetAttribute("end");
            beginDateField.Text = targetNode.GetAttribute("begin");
            endDateField.Text = targetNode.GetAttribute("end");
            startupCtrl.Text = targetNode.GetAttribute("startup");
            shutdownCtrl.Text = targetNode.GetAttribute("shutdown");

            beginDateInput.Text = "";
            endDateInput.Text = "";

            LoadDataUpdateField();

            return true;
        }

        private void LoadDataUpdateField()
        {
            if (workDateCtrl.Value.Date == DateTime.Today)
            {
                beginButton.Enabled = true;
                endButton.Enabled = true;
                if (beginDateField.Text == "")
                {
                    var now = DateTime.Now;
                    //var now2 = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                    var now2 = now.AddMinutes(9);
                    now2 = new DateTime(now2.Year, now2.Month, now2.Day, now2.Hour, now2.Minute / 10 * 10, 0);
                    beginDateInput.Text = now2.ToString("HH:mm");
                    //beginDateInput.Text = DateTime.Now.ToString("HH:mm");
                }
                else
                {
                    beginButton.Enabled = false;
                    if (endDateField.Text == "")
                    {
                        endDateInput.Text = DateTime.Now.ToString("HH:mm");
                    }
                    else
                    {
                        endButton.Enabled = false;
                    }
                }
            }
            else
            {
                beginButton.Enabled = false;
                endButton.Enabled = false;
            }

        }

        public enum SaveDataMode
        {
            Begin = 1,
            End,
            Startup,
            Shutdown
        }

        private void SaveData(SaveDataMode mode)
        {
            string targetMonth = workDateCtrl.Value.ToString("yyyy-MM");
            string fileName = "WorkReport_" + targetMonth + ".xml";
            string filePath = Path.Combine(ConfigDir, fileName);
            XmlDocument document = null;
            if (File.Exists(filePath))
            {
                document = LoadDocument(filePath);
            }
            else
            {
                document = CreateDocument();
            }

            string targetDate = DateTime.Today.ToString("yyyy-MM-dd");
            XmlElement targetNode = FindData(document, targetDate, true);

            if (mode == SaveDataMode.Begin)
            {
                if (string.IsNullOrEmpty(beginDateInput.Text))
                {
                    beginDateInput.Text = DateTime.Now.ToString("HH:mm");
                }
                targetNode.SetAttribute("begin", beginDateInput.Text);
            }
            else if (mode == SaveDataMode.End)
            {
                targetNode.SetAttribute("end", endDateInput.Text);
            }
            else if (mode == SaveDataMode.Shutdown)
            {
                targetNode.SetAttribute("shutdown", DateTime.Now.ToString("HH:mm"));
            }
            else if (mode == SaveDataMode.Startup)
            {
                string old = targetNode.GetAttribute("startup");
                if (string.IsNullOrEmpty(old))
                {
                    startupCtrl.Text = DateTime.Now.ToString("HH:mm");
                    targetNode.SetAttribute("startup", startupCtrl.Text);
                }
            }
            else if (mode == SaveDataMode.Shutdown)
            {
                targetNode.SetAttribute("shutdown", DateTime.Now.ToString("HH:mm"));
            }

            if (!Directory.Exists(ConfigDir))
            {
                Directory.CreateDirectory(ConfigDir);
            }
            document.Save(Path.Combine(ConfigDir, fileName));
        }

        /*
        private void SaveData_old()
        {
            string targetMonth = workDateCtrl.Value.ToString("yyyy-MM");
            string targetDate = workDateCtrl.Value.ToString("yyyy-MM-dd");
            string fileName = "WorkReport_" + targetMonth + ".xml";
            string filePath = Path.Combine(ConfigDir, fileName);
            if (!File.Exists(filePath))
            {
                return;
            }

            var document = LoadDocument(filePath);
            if (document == null)
            {
                document = CreateDocument();
            }

            XmlElement targetNode = FindData(document, targetDate, true);

            if (targetNode == null)
            {
                return;
            }

            targetNode.SetAttribute("date", workDateCtrl.Value.ToString("yyyy-MM-dd"));
            targetNode.SetAttribute("begin", beginDateCtrl.Text);
            targetNode.SetAttribute("end", endDateCtrl.Text);

            if (!Directory.Exists(ConfigDir))
            {
                Directory.CreateDirectory(ConfigDir);
            }
            document.Save(Path.Combine(ConfigDir, fileName));
        }
        */

        /*
        private void SaveLog_old(bool start)
        {
            string targetMonth = workDateCtrl.Value.ToString("yyyy-MM");
            string fileName = "WorkReport_" + targetMonth + ".xml";
            string filePath = Path.Combine(ConfigDir, fileName);
            XmlDocument document = null;
            if (File.Exists(filePath))
            {
                document = LoadDocument(filePath);
            }

            if (document == null)
            {
                document = CreateDocument();
            }

            string targetDate = DateTime.Today.ToString("yyyy-MM-dd");
            XmlElement targetNode = FindData(document, targetDate, true);

            if (start)
            {
                string old = targetNode.GetAttribute("startup");
                if (string.IsNullOrEmpty(old))
                {
                    targetNode.SetAttribute("startup", DateTime.Now.ToString("HH:mm"));
                }
            }
            else
            {
                targetNode.SetAttribute("shutdown", DateTime.Now.ToString("HH:mm"));
            }

            if (!Directory.Exists(ConfigDir))
            {
                Directory.CreateDirectory(ConfigDir);
            }
            document.Save(Path.Combine(ConfigDir, fileName));
        }
        */

        private XmlElement FindData(XmlDocument document, string targetDate, bool create)
        {
            var rootFolder = document.SelectSingleNode("WorkReport/WorkDataList");
            if (rootFolder == null)
            {
                if (create)
                {
                    throw new ApplicationException("WorkDataList tag not found.");
                }
                return null;
            }

            XmlElement targetNode = null;
            foreach (XmlElement node in rootFolder.ChildNodes)
            {
                if (node.GetAttribute("date") == targetDate)
                {
                    targetNode = node;
                }
            }
            if (targetNode == null)
            {
                if (!create)
                {
                    return null;
                }
                targetNode = document.CreateElement("WorkData");
                targetNode.SetAttribute("date", targetDate);
                rootFolder.AppendChild(targetNode);
            }

            return targetNode;
        }

        private XmlDocument CreateDocument()
        {
            var document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", null, null);
            document.AppendChild(declaration);

            XmlElement root = document.CreateElement("WorkReport");
            root.SetAttribute("version", "1.0");
            document.AppendChild(root);

            XmlElement rootFolder = document.CreateElement("WorkDataList");
            root.AppendChild(rootFolder);

            XmlElement logFolder = document.CreateElement("LogList");
            root.AppendChild(logFolder);

            return document;
        }

        private XmlDocument LoadDocument(string filePath)
        {
            var document = new XmlDocument();
            document.Load(filePath);
            var rootFolder = document.SelectSingleNode("WorkReport/WorkDataList");
            if (rootFolder == null)
            {
                throw new ApplicationException("Invalid file.");
            }

            return document;
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Visible)
                {
                    WindowState = FormWindowState.Minimized;
                }
                Show();
                WindowState = FormWindowState.Normal;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Close();
            }
        }

        private void workDateCtrl_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && Visible)
            {
                Visible = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData(SaveDataMode.Shutdown);
        }

        private void beginButtonEdit_Click(object sender, EventArgs e)
        {
            SaveData(SaveDataMode.Begin);
            LoadData();
        }

        private void endButtonEdit_Click(object sender, EventArgs e)
        {
            SaveData(SaveDataMode.End);
            LoadData();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (workDateCtrl.Value.Date == DateTime.Today)
            {
                if (beginDateField.Text != "" && endDateField.Text == "")
                {
                    var now = DateTime.Now;
                    var now2 = new DateTime(now.Year, now.Month, now.Day, now.Hour, (now.Minute) / 10 * 10, 0);
                    endDateInput.Text = now2.ToString("HH:mm");
                }
            }
        }
    }
}
