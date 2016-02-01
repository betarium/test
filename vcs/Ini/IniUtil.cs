using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ini
{
    public static class IniUtil
    {
        [DllImport("KERNEL32.DLL")]
        private static extern uint
            GetPrivateProfileStringA(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpReturnedString,
            uint nSize,
            string lpFileName);

        [DllImport("KERNEL32.DLL")]
        private static extern uint
            GetPrivateProfileIntA(
            string lpAppName,
            string lpKeyName,
            int nDefault,
            string lpFileName);

        [DllImport("KERNEL32.DLL")]
        private static extern uint
            WritePrivateProfileStringA(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        public static uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, out string lpReturnedString, int nSize, string lpFileName)
        {
            StringBuilder buf = new StringBuilder(nSize);
            uint result = GetPrivateProfileStringA(lpAppName, lpKeyName, lpDefault, buf, (uint)buf.Capacity, lpFileName);
            lpReturnedString = buf.ToString();
            return result;
        }

        public static uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName)
        {
            uint result = GetPrivateProfileIntA(lpAppName, lpKeyName, nDefault, lpFileName);
            return result;
        }

        public static uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName)
        {
            uint result = WritePrivateProfileStringA(lpAppName, lpKeyName, lpString, lpFileName);
            return result;
        }
    }
}
