using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Globalization;
using System.Net;

namespace wol
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                System.Console.Error.WriteLine("Invalid parameter.");
                System.Environment.Exit(-1);
                return;
            }

            var packet = new List<string>();
            packet.AddRange(Enumerable.Repeat("FF", 16));

            var mac = args[0].Split('-');
            for (int i = 0; i < 16; i++)
            {
                packet.AddRange(mac);
            }
            var data = Array.ConvertAll(packet.ToArray(), s => byte.Parse(s, NumberStyles.AllowHexSpecifier));

            new UdpClient(0).Send(data, data.Length, new IPEndPoint(0xFFFFFFFF, 9));
        }
    }
}
