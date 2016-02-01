using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace md5win
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MD5Form());

            string[] args = System.Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                return;
            }

            string path = args[1];

            var md5service = MD5CryptoServiceProvider.Create();

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buf = new byte[1024 * 1024];
                int len = 0;
                //byte[] tmphash = new byte[1024];
                while (true)
                {
                    len = file.Read(buf, 0, buf.Length);
                    if (file.Position >= file.Length)
                    {
                        break;
                    }
                    md5service.TransformBlock(buf, 0, len, null, 0);
                }
                md5service.TransformFinalBlock(buf, 0, len);
            }
            var hash = md5service.Hash;

            StringBuilder hashStr = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                hashStr.AppendFormat("{0:X2}", hash[i]);
            }

            System.Diagnostics.Debug.WriteLine(hashStr);
            System.Console.WriteLine(Path.GetFileName(path));
            System.Console.WriteLine(hashStr);

            long fileSize = new FileInfo(path).Length;

            Clipboard.SetText(hashStr.ToString());
            MessageBox.Show("[File] " + Path.GetFileName(path) + "\n" + "[Size] " + string.Format("{0:#,0}", fileSize) + " BYTE\n\n" + "[Hash] " + hashStr);
        }
    }
}
