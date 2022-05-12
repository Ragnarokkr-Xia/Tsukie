using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Tsukie.Integration.Models.Configuration
{
    public class PluginConfiguration
    {
        public PluginConfiguration(string configPath)
        {
            ConfigurationManager = GetConfigurationManagerInstance(configPath);
        }

        private ConfigurationManager GetConfigurationManagerInstance(string configPath)
        {
            ConfigurationManager instance = new ConfigurationManager();
            string? assemblyDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrEmpty(assemblyDirectoryName))
            {
                instance.SetBasePath(assemblyDirectoryName);
            }
            instance.AddJsonFile(configPath);
            return instance;
        }

        public IEnumerable<string>? AccessDescriptors =>
            ConfigurationManager.Get<PluginInternalConfiguration>().AccessDescriptors;
        public ConfigurationManager ConfigurationManager { get; }
    }
}
