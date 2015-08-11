using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace md5w
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            string path = args[0];

            var md5service = MD5CryptoServiceProvider.Create();

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buf = new byte[1024];
                int len = 0;
                byte[] tmphash = new byte[1024];
                while (true)
                {
                    len = file.Read(buf, 0, buf.Length);
                    if (file.Position >= file.Length)
                    {
                        break;
                    }
                    md5service.TransformBlock(buf, 0, len, tmphash, 0);
                }
                md5service.TransformFinalBlock(buf, 0, len);
            }
            var hash = md5service.Hash;

            /*
            var bytes = File.ReadAllBytes(path);
            var hash = md5service.ComputeHash(buf);
            */

            StringBuilder hashStr = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                hashStr.AppendFormat("{0:X2}", hash[i]);
            }

            System.Diagnostics.Debug.WriteLine(hashStr);
            System.Console.WriteLine(Path.GetFileName(path));
            System.Console.WriteLine(hashStr);
        }
    }
}
