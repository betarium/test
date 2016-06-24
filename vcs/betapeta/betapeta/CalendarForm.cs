using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace Betarium.Betapeta
{
    public partial class CalendarForm : Form
    {
        const string WEEK = "SUNMONTUEWEDTHUFRISAT";
        const string WEEK2 = "SMTWTFS";
        const string INI_FILE = "Calendar.ini";

        public DateTime Today { get; set; }
        public DateTime Current { get; set; }
        public Rectangle WorkingArea { get; set; }

        [DllImport("USER32.DLL")]
        protected static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("USER32.DLL")]
        protected static extern IntPtr FindWindowEx(IntPtr parent, IntPtr child, string className, string windowName);

        [DllImport("USER32.DLL")]
        protected static extern IntPtr SetParent(IntPtr child, IntPtr parent);

        [DllImport("USER32.DLL")]
        protected static extern IntPtr GetDesktopWindow();

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, UInt32 bRevert);

        [DllImport("USER32.DLL")]
        private static extern bool AppendMenu(IntPtr hMenu, UInt32 uFlags, UInt32 uIDNewItem, string lpNewItem);

        protected Label todayLabel = new Label();
        protected Label[] cellLabeles = new Label[49];
        protected ToolTip[] cellToolTip = new ToolTip[42];

        public CalendarForm()
        {
            InitializeComponent();
        }

        /*
        void SetParentDesktop()
        {
            IntPtr desktopWindow = FindWindow(null, "Program Manager");

            if (System.Environment.OSVersion.Version.Major >= 6)
            {
                IntPtr tmpChildWmd = IntPtr.Zero;
                while (true)
                {
                    tmpChildWmd = FindWindowEx(IntPtr.Zero, tmpChildWmd, "WorkerW", null);
                    if (tmpChildWmd == IntPtr.Zero)
                    {
                        break;
                    }
                    IntPtr findWnd = FindWindowEx(tmpChildWmd, IntPtr.Zero, "SHELLDLL_DefView", null);
                    if (findWnd != null)
                    {
                        desktopWindow = findWnd;
                        break;
                    }
                }
            }

            SetParent(Handle, desktopWindow);
        }
        */

        private void MakeCalendar()
        {
            int headHeight = 14;
            int weekHeight = 12;
            int cellWidth = 18;
            int cellHeight = 12;

            ClientSize = new Size((cellWidth - 1) * 7 + 1, headHeight + weekHeight + (cellHeight - 1) * 6 + 1);

            //Top = Screen.PrimaryScreen.WorkingArea.Height - Height;
            //Left = Screen.PrimaryScreen.WorkingArea.Width - Width;

            if (WorkingArea.Width == 0)
            {
                WorkingArea = Screen.PrimaryScreen.WorkingArea;
            }
            Top = WorkingArea.Bottom - Height;
            Left = WorkingArea.Right - Width;
            Top = WorkingArea.Bottom - Height;
            Left = WorkingArea.Right - Width;

            todayLabel.Parent = this;
            todayLabel.Width = ClientSize.Width;
            todayLabel.Height = headHeight;
            todayLabel.Font = new Font(this.Font.FontFamily, 12.0f, FontStyle.Bold, GraphicsUnit.Pixel);
            todayLabel.TextAlign = ContentAlignment.MiddleCenter;
            todayLabel.Visible = true;

            for (int index = 0; index < 49; index++)
            {
                Label label = new Label();
                cellLabeles[index] = label;
                label.Parent = this;
                label.AutoSize = false;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.Width = cellWidth;
                label.Height = cellHeight;

                label.Text = index.ToString();
                label.Left = index % 7 * (cellWidth - 1);

                if (index < 7)
                {
                    label.Top = headHeight;
                    label.Height = weekHeight;
                    label.BackColor = Color.Gray;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                }
                else if (index >= 7)
                {
                    label.Top = (index / 7 - 1) * (cellHeight - 1) + weekHeight + headHeight;
                    label.BackColor = Color.White;
                    label.TextAlign = ContentAlignment.MiddleRight;
                }
                label.Show();
            }

            Font weekFont = new Font(this.Font.FontFamily, 10.0f, FontStyle.Bold, GraphicsUnit.Pixel);
            for (int index = 0; index < 7; index++)
            {
                Label label = cellLabeles[index];
                label.Font = weekFont;
                //label.Text = WEEK.Substring(index * 3, 3);
                label.Text = WEEK2.Substring(index, 1);
                if (index == 0)
                {
                    label.ForeColor = Color.Red;
                }
                else if (index == 6)
                {
                    label.ForeColor = Color.Blue;
                }
                label.BackColor = Color.LightGray;
            }

            Font dayFont = new Font(this.Font.FontFamily, 10.0f, FontStyle.Regular, GraphicsUnit.Pixel);
            for (int index = 0; index < 42; index++)
            {
                Label label = cellLabeles[index + 7];
                label.Font = dayFont;

                //cellToolTip[index] = new ToolTip();
                //cellToolTip[index].SetToolTip(label, null);

                if (index % 7 == 0)
                {
                    label.ForeColor = Color.Red;
                }
                else if (index % 7 == 6)
                {
                    label.ForeColor = Color.Blue;
                }
                else
                {
                    label.ForeColor = Color.Black;
                }
                //label.DoubleClick += Day_DoubleClick;
            }
        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            Visible = false;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;

            Today = DateTime.Today;
            Current = Today;

            //SetParentDesktop();

            if (Environment.GetCommandLineArgs().Length >= 2)
            {
                string param = Environment.GetCommandLineArgs()[1];
                if (param == "/update")
                {
                    DownloadHoliday();
                }
            }


            LoadHoliday();

            IntPtr menu = GetSystemMenu(Handle, 0);
            AppendMenu(menu, 0, 0x00000800, null);
            AppendMenu(menu, 0, 0, "翌月");
            AppendMenu(menu, 0, 1, "当月");
            AppendMenu(menu, 0, 2, "前月");

            MakeCalendar();

            updateTimer.Interval = 10000;
            updateTimer.Start();

            Update(Current);

            Visible = true;
        }

        Dictionary<string, bool> holydayTable = new Dictionary<string, bool>();
        private void LoadHoliday()
        {
            //string iniPath = Path.Combine(Application.StartupPath, Application.ProductName + ".ini");
            string iniPath = Path.Combine(Application.StartupPath, INI_FILE);
            for (int i = 1; ; i++)
            {
                string holiday = Win32Api.GetPrivateProfileString("HOLIDAY", i.ToString(), null, iniPath);
                if (string.IsNullOrEmpty(holiday))
                {
                    break;
                }
                holydayTable.Add(holiday, true);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            if (m.Msg == WM_SYSCOMMAND)
            {
                switch ((uint)m.WParam)
                {
                    case 0:
                        Current = Current.AddMonths(1);
                        Update(Current);
                        break;
                    case 1:
                        Current = Today;
                        Update(Current);
                        break;
                    case 2:
                        Current = Current.AddMonths(-1);
                        Update(Current);
                        break;
                }
            }
            base.WndProc(ref m);
        }

        protected void Update(DateTime target)
        {
            DateTime today = Today;

            Text = String.Format("{0}/{1}", target.Year, target.Month);
            //if (target.Year == Today.Year && target.Month == Today.Month)
            //{
            //    Text = String.Format("{0}/{1}/{2}({3})", today.Year, today.Month, today.Day, WEEK.Substring((int)today.DayOfWeek * 3, 3));
            //}
            if (target.Year == Today.Year && target.Month == Today.Month)
            {
                todayLabel.Text = String.Format("{0}/{1}/{2}({3})", today.Year, today.Month, today.Day, WEEK.Substring((int)today.DayOfWeek * 3, 3));
            }
            else
            {
                todayLabel.Text = "";
            }

            DateTime beginDate = new DateTime(target.Year, target.Month, 1);
            int startWeeek = (int)beginDate.DayOfWeek;
            beginDate = beginDate.AddDays(-(int)beginDate.DayOfWeek);
            for (int index = 0; index < 42; index++)
            {
                Label label = cellLabeles[index + 7];

                DateTime day = beginDate.AddDays(index);
                string holydayCheck = day.ToString("yyyy/MM/dd");
                label.Name = day.ToString("yyyyMMdd");
                label.Text = day.Day.ToString();
                label.Tag = day;

                if (index % 7 == 0)
                {
                    label.ForeColor = Color.Red;
                }
                else if (holydayTable.ContainsKey(holydayCheck))
                {
                    label.ForeColor = Color.Red;
                }
                else if (index % 7 == 6)
                {
                    label.ForeColor = Color.Blue;
                }
                else
                {
                    label.ForeColor = Color.Black;
                }
                if (day == today)
                {
                    label.BackColor = Color.LightGreen;
                }
                else if (day.Month != target.Month)
                {
                    label.BackColor = Color.DarkGray;
                }
                else
                {
                    label.BackColor = Color.White;
                }
            }

            int nextMonth = -1;
            for (int index = 0; index < 42; index++)
            {
                Label label = cellLabeles[index + 7];
                ToolTip toolTip = cellToolTip[index];

                DateTime day = beginDate.AddDays(index);
                label.Text = day.Day.ToString();

                if (day.Month != target.Month && (day.Year > target.Year || (day.Year == target.Year && day.Month > target.Month)))
                {
                    if (nextMonth == -1)
                    {
                        nextMonth = index;
                    }
                    if (index / 7 > nextMonth / 7)
                    {
                        label.Text = "";
                    }
                }

                //if (scheduleList.ContainsKey(day.ToString("yyyy-MM-dd")))
                //{
                //    string item = scheduleList[day.ToString("yyyy-MM-dd")];

                //    label.Text = label.Text + "\n" + item.Replace("\n", " / ");
                //    toolTip.ShowAlways = true;
                //    toolTip.SetToolTip(label, day.ToString("yyyy/MM/dd") + "\n" + item);
                //}
                //else
                //{
                //    toolTip.SetToolTip(label, null);
                //    //toolTip.Hide(label);
                //}

            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Today == Today)
            {
                return;
            }

            if (Current.Year == Today.Year && Current.Month == Today.Month)
            {
                Today = DateTime.Today;
                Current = Today;
                Update(Current);
            }

            Today = DateTime.Today;

        }

        protected void DownloadHoliday()
        {
            //string dataStart = Today.AddMonths(-1).ToString("yyyy-MM-dd");
            //string dataEnd = Today.AddMonths(12).ToString("yyyy-MM-dd");
            //string dataStart = "2000-01-01";
            //string dataEnd = "2020-12-31";
            string dataStart = new DateTime(Today.Year, 1, 1).AddYears(-1).ToString("yyyy-MM-dd");
            string dataEnd = new DateTime(Today.Year, 12, 31).AddYears(2).ToString("yyyy-MM-dd");
            string url = "http://www.google.com/calendar/feeds/outid3el0qkcrsuf89fltf7a4qbacgt9@import.calendar.google.com/public/full-noattendees?orderby=startTime&sortorder=ascend&start-min=" + dataStart + "T00:00:00&start-max=" + dataEnd + "T23:59:59&max-results=99999";

            List<string> holydatList = new List<string>();
            try
            {
                using (XmlReader reader = new XmlTextReader(url))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(reader);
                    doc.Save(Path.Combine(Application.StartupPath, "Calendar.xml"));
                    //System.Diagnostics.Debug.WriteLine(doc.w.ToString());

                    XmlNamespaceManager names = new XmlNamespaceManager(doc.NameTable);
                    names.AddNamespace("ns", "http://www.w3.org/2005/Atom");
                    names.AddNamespace("gd", "http://schemas.google.com/g/2005");
                    XmlNodeList nodeList = doc.SelectNodes("ns:feed/ns:entry", names);
                    //scheduleList.Clear();
                    foreach (XmlNode node in nodeList)
                    {
                        XmlNode item = node.SelectSingleNode("ns:title", names);
                        XmlNode item2 = node.SelectSingleNode("gd:when", names);
                        XmlNodeList item3 = node.SelectNodes("gd:when", names);

                        string title = item.InnerText;
                        string start = item2.Attributes["startTime"].Value;

                        start = start.Replace("-", "/");
                        title = title.Split('/')[0];
                        title = title.Trim();

                        //System.Diagnostics.Debug.WriteLine(start + "=" + title);
                        //holydatList.Add(start + "=" + title);
                        foreach (XmlNode item4 in item3)
                        {
                            start = item4.Attributes["startTime"].Value;
                            start = start.Replace("-", "/");
                            title = title.Split('/')[0];
                            title = title.Trim();

                            //System.Diagnostics.Debug.WriteLine(start + "=" + title);
                            holydatList.Add(start + "=" + title);

                        }
                        //if (scheduleList.ContainsKey(start))
                        //{
                        //    scheduleList[start] = scheduleList[start] + "\n" + title;
                        //}
                        //else
                        //{
                        //    scheduleList.Add(start, title);
                        //}
                    }
                    holydatList.Sort();
                    string holidayData = "[HOLIDAY]\r\n";
                    for (int i = 0; i < holydatList.Count; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(holydatList[i]);
                        holidayData += (i + 1).ToString() + "=" + holydatList[i].Split('=')[0] + "\r\n";
                    }
                    File.WriteAllText(Path.Combine(Application.StartupPath, "Calendar.ini"), holidayData);
                }
            }
            catch (Exception ex)
            {
                updateTimer.Stop();
                MessageBox.Show(this, "データの取得に失敗しました。" + ex.Message);
            }
        }

    }
}
