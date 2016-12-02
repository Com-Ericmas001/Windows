﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Com.Ericmas001.Windows.Wpf
{
    public static class WindowsUtility
    {
        public static bool IsWindows7OrHigher()
        {
            OperatingSystem osInfo = Environment.OSVersion;
            return (osInfo.Platform == PlatformID.Win32NT && osInfo.Version >= Version.Parse("6.1"));
        }

        public static string AppLocalDataPath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            string res = Path.Combine(appDataPath, appName);
            if (!Directory.Exists(res))
                Directory.CreateDirectory(res);
            return res;
        }
    }
}
