using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace base64
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            using (TextReader reader = new StreamReader(path, Encoding.Default))
            {
                string data = reader.ReadToEnd();
                byte[] val = Convert.FromBase64String(data);
                using (FileStream stream = new FileStream(path + ".bin", FileMode.Create))
                {
                    stream.Write(val, 0, val.Length);
                }
            }
        }
    }
}
