﻿using Microsoft.Win32;
using System.Text;

namespace Client.Client
{
    internal class WindowsHelper
    {
        public static string GetUsername()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        public static int GetScreenCount()
        {
            return Screen.AllScreens.Length;
        }

        public static void WriteRegistryKey(string szPath, RegistryValueKind regType, string szKeyname, string szKeyValue, bool bCreateNew = false)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(szPath, true);

            if (registryKey == null)
            {
                if (!bCreateNew)
                {
                    registryKey.Close();
                    return;
                }

                registryKey = Registry.CurrentUser.CreateSubKey(szPath);
            }

            switch(regType)
            {
                case RegistryValueKind.DWord:
                    registryKey.SetValue(szKeyname, uint.Parse(szKeyValue), regType);
                    break;
                default:
                    registryKey.SetValue(szKeyname, szKeyValue, regType);
                    break;
            }
            registryKey.Close();
        }

        public static string ReadRegistryKey(string szPath, string szKeyname)
        {

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(szPath, true);

            if (registryKey == null)
            {
                return "<ERROR-NOTFOUND>";
            }

            string value = registryKey.GetValue(szKeyname).ToString();
            registryKey.Close();

            if (value == null)
                return "<ERROR-NOTFOUND>";

            return value;
        }

    }
}
