using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ini
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Help();
                return;
            }

            string firstArg = args[0].ToLower();
            if (firstArg == "/h" || firstArg == "/help" || firstArg == "/?")
            {
                Help();
                return;
            }

            if (firstArg == "/set")
            {
                if (args.Length != 5)
                {
                    ConsoleWriteErrorLine("無効なパラメータです。");
                    Environment.Exit(-1);
                    return;
                }

                var result = IniUtil.WritePrivateProfileString(args[2], args[3], args[4], args[1]);
                if (result == 0)
                {
                    ConsoleWriteErrorLine("書き込みエラーが発生しました。");
                    Environment.Exit(-2);
                    return;
                }
            }
            else
            {
                if (args.Length != 3)
                {
                    ConsoleWriteErrorLine("無効なパラメータです。");
                    Environment.Exit(-1);
                    return;
                }

                string val = null;
                var result = IniUtil.GetPrivateProfileString(args[1], args[2], null, out val, 1024, args[0]);
                if (result == 0)
                {
                    ConsoleWriteErrorLine("読み込みエラーが発生しました。");
                    Environment.Exit(-2);
                    return;
                }
                ConsoleWriteLine(val);
            }
        }

        static void Help()
        {
            ConsoleWriteLine("INIファイルの読み書きを行います。");
            ConsoleWriteLine("");
            ConsoleWriteLine("ini ファイル名 セクション名 キー名");
            ConsoleWriteLine("ini /set ファイル名 セクション名 キー名 値");
        }

        static void ConsoleWriteLine(string message)
        {
            System.Console.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(message);
        }

        static void ConsoleWriteErrorLine(string message)
        {
            System.Console.Error.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(message);
        }

    }
}
