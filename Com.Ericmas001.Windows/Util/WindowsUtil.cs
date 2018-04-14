using System;
using System.IO;
using System.Reflection;
using System.Security.Principal;

namespace Com.Ericmas001.Windows.Util
{
    public class WindowsUtil
    {
        public static string GetWindowsUsername()
        {
            string user = WindowsIdentity.GetCurrent().Name;
            user = user.Substring(user.LastIndexOf('\\') + 1);
            return user.ToUpper();
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
