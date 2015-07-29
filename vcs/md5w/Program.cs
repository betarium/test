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
            var bytes = File.ReadAllBytes(path);
            var md5service = MD5CryptoServiceProvider.Create();
            var hash = md5service.ComputeHash(bytes);

            StringBuilder hashStr = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                hashStr.AppendFormat("{0:X2}", hash[i]);
            }

            System.Diagnostics.Debug.WriteLine(hashStr);
            System.Console.WriteLine(hashStr);
        }
    }
}
