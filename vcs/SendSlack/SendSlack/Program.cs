using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SendSlack
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                System.Console.WriteLine("Usage:\r\nSendSlack url username message\r\n");
                Environment.Exit(1);
                return;
            }

            string url = args[0];
            string username = args[1];
            string text = args[2];

            SendSlackMessage(url, username, text);
        }

        [DataContract]
        class SlackMessage
        {
            [DataMember]
            public string username { get; set; }

            [DataMember]
            public string text { get; set; }
        }

        private static void SendSlackMessage(string url, string username, string text)
        {
            var message = new SlackMessage();
            message.username = username;
            message.text = text;

            MemoryStream stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(typeof(SlackMessage));
            serializer.WriteObject(stream, message);
            stream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            var client = new WebClient();
            byte[] result = client.UploadData(url, buffer);

            string resultText = Encoding.UTF8.GetString(result);
            System.Console.WriteLine(resultText);
        }
    }
}
