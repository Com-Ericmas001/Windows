using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Windows.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory.Models
{
    public class FirstCategorySettings
    {
        private static string SettingsPath => Path.Combine(WindowsUtil.AppLocalDataPath(), nameof(FirstCategorySettings) + ".txt");
        public IList<string> SomeTextHistory { get; set; }
        public bool SomeBool { get; set; }
        public static FirstCategorySettings LoadSettings()
        {
            var res = new FirstCategorySettings
            {
                SomeBool = true
            };

            try
            {
                res = JsonConvert.DeserializeObject<FirstCategorySettings>(File.ReadAllText(SettingsPath));
            }
            catch
            {
                // do nothing
            }
            res.InitList(x => x.SomeTextHistory, "Basic Text");
            return res;
        }
        private void InitList(Expression<Func<FirstCategorySettings, IList<string>>> property, params string[] defaults)
        {
            var expr = (MemberExpression)property.Body;
            var prop = (PropertyInfo)expr.Member;

            var current = (IList<string>)prop.GetValue(this);
            if (current == null || !current.Any())
                prop.SetValue(this, new List<string>(defaults), null);
            else
                prop.SetValue(this, current.Distinct().ToList(), null);
        }
        public void Save()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(this, Formatting.Indented, settings));
        }
    }
}
