using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using System.Diagnostics;

namespace SQLServerBackupManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            backupFolderText.Text = Properties.Settings.Default.BackupDirectory;

            List<string> databaseList = new List<string>();
            using (DbConnectionExtension conn = new DbConnectionExtension())
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT @@SERVERNAME";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ServerName.Text = "ServerName: " + reader.GetString(0);
                        }
                    }
                }

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM sys.databases ORDER BY name";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string databaseName = reader.GetString(0);
                            databaseList.Add(databaseName);
                        }
                    }
                }
            }

            foreach (var item in databaseList)
            {
                databaseListView.Items.Add(item);
            }
            databaseListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            databaseListView.Columns[0].Width = databaseListView.Columns[0].Width - 10;
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            string backupDir = backupFolderText.Text;
            if (!Directory.Exists(backupDir))
            {
                MessageBox.Show("Backup Folder not exists.");
                return;
            }

            try
            {
                UseWaitCursor = true;
                using (DbConnectionExtension conn = new DbConnectionExtension())
                {
                    foreach (ListViewItem item in databaseListView.CheckedItems)
                    {
                        string sql = "BACKUP DATABASE {0} TO DISK = '{1}'";
                        string fileName = string.Format("{0}-{1}.bak", item.Text, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                        string filePath = Path.Combine(backupDir, fileName);
                        sql = string.Format(sql, item.Text, filePath);
                        using (var command = conn.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandTimeout = 60000;
                            command.ExecuteNonQuery();
                        }

                        if (EventLog.SourceExists("Application"))
                        {
                            EventLog.WriteEntry("Application", "SQL Server Backup:" + filePath, EventLogEntryType.Information, 0, 0);
                        }
                    }
                }
                UseWaitCursor = false;
                MessageBox.Show("Backup complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backup Error.\n" + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }
    }
}
