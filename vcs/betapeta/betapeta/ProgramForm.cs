using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;

namespace Betarium.Betapeta
{
    public partial class ProgramForm : Form
    {
        public ProgramForm()
        {
            InitializeComponent();

            Visible = false;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.Top = -9999;
            this.Left = -9999;
            //Opacity = 0.0;
        }

        [DllImport("USER32.DLL")]
        protected static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("USER32.DLL")]
        protected static extern IntPtr SetParent(IntPtr child, IntPtr parent);

        [DllImport("USER32.DLL")]
        protected static extern IntPtr FindWindowEx(IntPtr parent, IntPtr child, string className, string windowName);

        [DllImport("USER32.DLL")]
        protected static extern IntPtr GetDesktopWindow();

        public string BaseDir { get; set; }
        public string MemoDir { get; set; }

        private void ProgramForm_Load(object sender, EventArgs e)
        {
            MoveWindowDesktop(this);
#if DEBUG
            trayIcon.Text = trayIcon.Text + " [DEBUG]";
#endif
            trayIcon.Visible = true;

            BaseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            BaseDir = Path.Combine(BaseDir, "betapeta");
            MemoDir = Path.Combine(BaseDir, "memo");
            if (!Directory.Exists(MemoDir))
            {
                Directory.CreateDirectory(MemoDir);
            }
            LoadMemo();
            CalendarForm calendar = new CalendarForm();
            calendar.WorkingArea = GetWorkingArea();
            calendar.Show();
            MoveWindowDesktop(calendar);
            Visible = false;
        }

        private void MoveWindowDesktop(Form form)
        {
            IntPtr desktopWindow = FindWindow("Progman", "Program Manager");
            if (FindWindowEx(desktopWindow, IntPtr.Zero, "SHELLDLL_DefView", null) == IntPtr.Zero)
            {
                //IntPtr tmpChildWmd = IntPtr.Zero;
                //while (true)
                //{
                //    tmpChildWmd = FindWindowEx(IntPtr.Zero, tmpChildWmd, "SysListView32", null);
                //    if (tmpChildWmd == IntPtr.Zero)
                //    {
                //        break;
                //    }
                //    IntPtr findWnd = FindWindowEx(tmpChildWmd, IntPtr.Zero, "SHELLDLL_DefView", null);
                //    if (findWnd != IntPtr.Zero)
                //    {
                //        findWnd = FindWindowEx(IntPtr.Zero, findWnd, "WorkerW", null);
                //        desktopWindow = findWnd;
                //        break;
                //    }
                //}
                IntPtr tmpChildWmd = IntPtr.Zero;
                while (true)
                {
                    tmpChildWmd = FindWindowEx(IntPtr.Zero, tmpChildWmd, "WorkerW", null);
                    if (tmpChildWmd == IntPtr.Zero)
                    {
                        break;
                    }
                    IntPtr findWnd = FindWindowEx(tmpChildWmd, IntPtr.Zero, "SHELLDLL_DefView", null);
                    if (findWnd != IntPtr.Zero)
                    {
                        desktopWindow = tmpChildWmd;
                        break;

                        //IntPtr findWnd2 = FindWindowEx(findWnd, IntPtr.Zero, "SysListView32", null);
                        //if (findWnd2 != IntPtr.Zero)
                        //{
                        //    desktopWindow = findWnd2;
                        //    break;
                        //}
                        //break;
                    }
                }
            }

            SetParent(form.Handle, desktopWindow);

        }

        private List<MemoForm> memoFormList = new List<MemoForm>();

        public Rectangle GetWorkingArea()
        {
            int screenMode = Properties.Settings.Default.TargetScreenMode;
            int screenNum = Properties.Settings.Default.TargetScreenNum;
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            if (screenMode == 0)
            {
                workingArea = Screen.PrimaryScreen.WorkingArea;
            }
            else if (screenMode == 1)
            {
                workingArea = Screen.AllScreens[0].WorkingArea;
            }
            else if (screenMode == 2)
            {
                workingArea = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea;
            }
            else if (screenMode == 3 && screenNum >= 0 && screenNum < Screen.AllScreens.Length)
            {
                workingArea = Screen.AllScreens[screenNum].WorkingArea;
            }

            bool screenReviseIgnore = Properties.Settings.Default.ScreenReviseIgnoreFlag;
            if (!screenReviseIgnore)
            {
                Rectangle allArea = new Rectangle();
                foreach (var scr in Screen.AllScreens)
                {
                    allArea = Rectangle.Union(allArea, scr.Bounds);
                }
                workingArea.X -= allArea.Left;
                workingArea.Y -= allArea.Top;
            }

            return workingArea;
        }

        public void LoadMemo()
        {
            foreach (var item in memoFormList)
            {
                if (item is MemoForm)
                {
                    item.Close();
                }
            }
            memoFormList.Clear();

            string[] pathList = Directory.GetFiles(MemoDir, "*.txt");

            List<string> tmplst = new List<string>(pathList);
            tmplst.Sort();
            pathList = tmplst.ToArray();

            var workingArea = GetWorkingArea();

            int cnt = 0;
            foreach (var path in pathList)
            {
                MemoForm memoForm = new MemoForm();
                memoForm.StartPosition = FormStartPosition.Manual;
                memoForm.Top = workingArea.Top + memoForm.Height * (cnt++);
                memoForm.Left = workingArea.Right - memoForm.Width;
                memoForm.Text = Path.GetFileNameWithoutExtension(path);
                memoForm.MemoText.Text = File.ReadAllText(path);
                memoForm.OwnerForm = this;
                memoForm.FileName = path;
                MoveWindowDesktop(memoForm);
                memoForm.Show();
                memoFormList.Add(memoForm);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                EditMemo(null);
            }
        }

        public void EditMemo(string fileName)
        {
            MemoInputForm memoForm = new MemoInputForm();
            memoForm.MemoDir = MemoDir;
            memoForm.OwnerForm = this;
            memoForm.Text = fileName;
            if (!string.IsNullOrEmpty(fileName))
            {
                string path = Path.Combine(MemoDir, fileName);
                memoForm.MemoText.Text = File.ReadAllText(path);
                memoForm.FileName = fileName;
            }
            memoForm.ShowDialog(this);
        }

        private void openMemoFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MemoDir);
        }

        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordEditForm form = new PasswordEditForm();
            form.PasswordDir = BaseDir;
            form.Show(this);
        }

    }
}
