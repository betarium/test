using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Betarium.Betapeta
{
    public partial class MemoInputForm : Form
    {
        public string MemoDir { get; set; }
        public string FileName { get; set; }
        public ProgramForm OwnerForm { get; set; }


        public MemoInputForm()
        {
            InitializeComponent();
        }

        private void MemoInputForm_Load(object sender, EventArgs e)
        {
            DeleteButton.Enabled = false;
            if (!String.IsNullOrEmpty(FileName) && File.Exists(Path.Combine(MemoDir, FileName)))
            {
                string path = Path.Combine(MemoDir, FileName);
                MemoText.Text = File.ReadAllText(path);
                Text = Path.GetFileNameWithoutExtension(FileName);
                DeleteButton.Enabled = true;
            }
            else
            {
                string newName = DateTime.Now.ToString("yyyy年MM月dd日 HH時mm分") + ".txt";
                int index = 2;
                while (File.Exists(Path.Combine(MemoDir, newName)))
                {
                    newName = DateTime.Now.ToString("yyyy年MM月dd日 HH時mm分") + " " + (index++) + ".txt";
                }
                FileName = newName;
                Text = Path.GetFileNameWithoutExtension(FileName);
            }

            MemoText.Select(0, 0);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string memo = MemoText.Text;

            if (!Directory.Exists(MemoDir))
            {
                Directory.CreateDirectory(MemoDir);
            }
            string path = Path.Combine(MemoDir, FileName);
            File.WriteAllText(path, memo);
            OwnerForm.LoadMemo();
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除しますか？", Application.ProductName, MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            //File.Delete(FileName);

            FileSystem.DeleteFile(FileName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);

            OwnerForm.LoadMemo();
            Close();
        }

    }
}
