using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Betarium.Betapeta
{
    public partial class PasswordEditForm : Form
    {
        public class PasswordInfo
        {
            public string Account { get; set; }
            public string URL { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
        }

        public string PasswordDir { get; set; }

        public PasswordEditForm()
        {
            InitializeComponent();
        }

        public string IniPath { get; set; }

        private void PasswordEditForm_Load(object sender, EventArgs e)
        {
            IniPath = Path.Combine(PasswordDir, "password.ini");

            string selectedAccount = Win32Api.GetPrivateProfileString("common", "selected_account", null, IniPath);

            for (int i = 1; ; i++)
            {
                string accountNum = string.Format("account{0:0000}", i);
                PasswordInfo info = new PasswordInfo();
                info.Account = Win32Api.GetPrivateProfileString(accountNum, "account", null, IniPath);
                info.URL = Win32Api.GetPrivateProfileString(accountNum, "url", null, IniPath);
                info.User = Win32Api.GetPrivateProfileString(accountNum, "user", null, IniPath);
                info.Password = Win32Api.GetPrivateProfileString(accountNum, "password", null, IniPath);

                info.Account = DecodeBase64(info.Account);
                info.URL = DecodeBase64(info.URL);
                info.User = DecodeBase64(info.User);
                info.Password = DecodeBase64(info.Password);

                info.Account = info.Account ?? "";
                info.URL = info.URL ?? "";
                info.User = info.User ?? "";
                info.Password = info.Password ?? "";

                if (string.IsNullOrEmpty(info.Account))
                {
                    break;
                }
                var item = AccountList.Items.Add("");
                item.Text = info.Account;
                item.Tag = info;
                if (info.Account == selectedAccount)
                {
                    item.Selected = true;
                }
            }

        }

        protected string EncodeBase64(string value)
        {
            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        protected string DecodeBase64(string value)
        {
            if (value == null)
            {
                return value;
            }
            try
            {
                return Encoding.UTF8.GetString(System.Convert.FromBase64String(value));
            }
            catch (FormatException)
            {
                return value;
            }
        }

        private void CopyUrlButton_Click(object sender, EventArgs e)
        {
            if (AccountList.SelectedIndices.Count != 1)
            {
                return;
            }
            var item = AccountList.SelectedItems[0];
            var info = (PasswordInfo)item.Tag;
            string pass = info.URL;
            if (string.IsNullOrEmpty(pass))
            {
                Clipboard.Clear();
                return;
            }
            Clipboard.SetText(pass);
        }

        private void CopyUserButton_Click(object sender, EventArgs e)
        {
            if (AccountList.SelectedIndices.Count != 1)
            {
                return;
            }
            var item = AccountList.SelectedItems[0];
            var info = (PasswordInfo)item.Tag;

            string pass = info.User;
            if (string.IsNullOrEmpty(pass))
            {
                Clipboard.Clear();
                return;
            }
            Clipboard.SetText(pass);
        }

        private void CopyPasswordButton_Click(object sender, EventArgs e)
        {
            if (AccountList.SelectedIndices.Count != 1)
            {
                return;
            }
            var item = AccountList.SelectedItems[0];
            var info = (PasswordInfo)item.Tag;

            string pass = info.Password;
            if (string.IsNullOrEmpty(pass))
            {
                Clipboard.Clear();
                return;
            }
            Clipboard.SetText(pass);
        }

        private void AccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AccountList.SelectedIndices.Count != 1)
            {
                AccountText.Text = "";
                UrlText.Text = "";
                UserText.Text = "";
                PasswordText.Text = "";
                return;
            }
            var item = AccountList.SelectedItems[0];
            var info = (PasswordInfo)item.Tag;
            AccountText.Text = info.Account;
            UrlText.Text = info.URL;
            UserText.Text = info.User;
            PasswordText.Text = info.Password;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            PasswordInfo info = new PasswordInfo();
            ListViewItem item = null;
            int itemIndex = 0;
            if (AccountList.SelectedIndices.Count == 1)
            {
                item = AccountList.SelectedItems[0];
                info = (PasswordInfo)item.Tag;
                itemIndex = AccountList.SelectedIndices[0] + 1;
            }
            else
            {
                item = AccountList.Items.Add("");
                item.Tag = info;
                itemIndex = AccountList.Items.Count;
            }

            info.Account = AccountText.Text;
            info.URL = UrlText.Text;
            info.User = UserText.Text;
            info.Password = PasswordText.Text;
            item.Text = info.Account;

            string accountNum = string.Format("account{0:0000}", itemIndex);
            //string path = Path.Combine(PasswordDir, "password.ini");
            if (File.Exists(IniPath))
            {
                File.Copy(IniPath, IniPath + ".bak", true);
            }
            Win32Api.WritePrivateProfileString(accountNum, "account", EncodeBase64(info.Account), IniPath);
            Win32Api.WritePrivateProfileString(accountNum, "url", EncodeBase64(info.URL), IniPath);
            Win32Api.WritePrivateProfileString(accountNum, "user", EncodeBase64(info.User), IniPath);
            Win32Api.WritePrivateProfileString(accountNum, "password", EncodeBase64(info.Password), IniPath);
        }

        private void PasswordEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string accountName = null;
            if (AccountList.SelectedIndices.Count == 1)
            {
                var item = AccountList.SelectedItems[0];
                var info = (PasswordInfo)item.Tag;
                accountName = info.Account;
            }
            Win32Api.WritePrivateProfileString("common", "selected_account", accountName, IniPath);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (AccountList.SelectedIndices.Count != 1)
            {
                return;
            }
            var item = AccountList.SelectedItems[0];
            var info = (PasswordInfo)item.Tag;

            string url = info.URL;
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            try
            {
                Process.Start(url);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "エラー：プログラムの起動に失敗しました");
            }
        }

    }
}
