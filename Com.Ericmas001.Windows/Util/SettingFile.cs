using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Windows.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Com.Ericmas001.Windows.Util
{
    public static class SettingFile<T> where T:new() 
    {
        public static string FullPath => Path.Combine(WindowsUtil.AppLocalDataPath(), typeof(T).Name + ".txt");

        public static T Load()
        {
            var res = new T();

            try
            {
                res = JsonConvert.DeserializeObject<T>(File.ReadAllText(FullPath));
            }
            catch
            {
                // do nothing
            }

            var listProps = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(IList<string>)).ToArray();
            foreach (var listProp in listProps)
            {
                var att = listProp.GetCustomAttribute(typeof(DefaultValuesAttribute)) as DefaultValuesAttribute;
                var defaults = att?.Defaults ?? new string[0];

                if(!(listProp.GetValue(res) is IList<string> list) || !list.Any())
                    listProp.SetValue(res,new List<string>(defaults));
                else
                    listProp.SetValue(res, new List<string>(list.Distinct()));
            }
            return res;
        }

        public static void Save(T obj)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
            File.WriteAllText(FullPath, JsonConvert.SerializeObject(obj, Formatting.Indented, settings));
        }
    }
}
