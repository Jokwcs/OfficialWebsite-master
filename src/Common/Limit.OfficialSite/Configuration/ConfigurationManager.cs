using System.IO;
using Microsoft.Extensions.Configuration;

namespace Limit.OfficialSite.Configuration
{
    public class ConfigurationManager
    {
        private static readonly object Locker = new object();

        private static ConfigurationManager _instance;

        private IConfigurationRoot Config { get; }

        private ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config = builder.Build();
        }

        private static ConfigurationManager GetInstance()
        {
            if (_instance != null)
                return _instance;

            lock (Locker)
            {
                _instance ??= new ConfigurationManager();
            }

            return _instance;
        }

        public static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }

        public static T GetEntity<T>(string name)
        {
            return GetInstance().Config.GetValue<T>(name);
        }
    }
}