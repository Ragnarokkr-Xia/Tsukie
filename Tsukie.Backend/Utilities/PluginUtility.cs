using System.Reflection;
using Tsukie.Backend.Models.Exceptions;
using Tsukie.Backend.Models.Plugin;
using Tsukie.Integration.Interfaces;
using Tsukie.Integration.Models;

namespace Tsukie.Backend.Utilities
{
    public class PluginUtility
    {
        public PluginUtility(ILogger<PluginUtility> logger)
        {
            Logger = logger;
        }

        public string BaseFolder { get; set; } = string.Empty;
        public ILogger Logger { get; set; }

        public IEnumerable<PluginInfo> Cache { get; set; }

        public void Reload()
        {
            ListPluginInfo(true);
        }
        public IEnumerable<PluginInfo> ListPluginInfo(bool reload = false)
        {
            if (reload || Cache == null)
            {
                Cache = Discover();
            }
            return Cache;
        }
        private IEnumerable<PluginInfo> Discover()
        {
            List<PluginInfo> results = new List<PluginInfo>();
            if (!Directory.Exists(BaseFolder))
            {
                return results;
            }
            IEnumerable<string> subFolders = Directory.EnumerateDirectories(BaseFolder);
            foreach (string subFolder in subFolders)
            {
                IEnumerable<string> filesInSubFolder = Directory.EnumerateFiles(subFolder, "*.dll");
                foreach (string file in filesInSubFolder)
                {
                    try
                    {
                        Assembly pluginAssembly = Assembly.LoadFrom(file);
                        foreach (var type in pluginAssembly.ExportedTypes)
                        {
                            bool criteria =
                                type.IsSubclassOf(typeof(Plugin)) &&
                                type.FindInterfaces((t, c) => t == c as Type, typeof(IStartStop)).Length > 0 &&
                                type.FindInterfaces((t, c) => t == c as Type, typeof(IDisposable)).Length > 0;
                            if (criteria)
                            {
                                PluginInfo result = new PluginInfo()
                                {
                                    Id = GetPluginId(type),
                                    Path = file,
                                    Type = type
                                };
                                results.Add(result);
                            }
                        }
                    }
                    catch
                    {
                        Logger?.LogDebug($"file: {file} is not an Tsukie plugin, skipped loading.");
                    }
                    
                }
            }

            return results;
        }

        public PluginInfo FindById(string typeId)
        {
            IEnumerable<PluginInfo> pluginInfoList = ListPluginInfo();
            PluginInfo result = pluginInfoList.FirstOrDefault(t => t.Id.Equals(typeId, StringComparison.InvariantCultureIgnoreCase));
            if (result == null)
            {
                throw new PluginTypeNotFoundException();
            }

            return result;
        }
        public string GetPluginId(Type t) => (string)t.GetProperty(nameof(PluginInformation.PluginId), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string GetPluginName(Type t) => (string)t.GetProperty(nameof(PluginInformation.PluginName), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string GetPluginDescription(Type t) => (string)t.GetProperty(nameof(PluginInformation.PluginDescription), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string GetPluginVersion(Type t) => (string)t.GetProperty(nameof(PluginInformation.PluginVersion), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string GetPluginAuthorName(Type t) => (string)t.GetProperty(nameof(PluginInformation.PluginAuthorName), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string GetPluginWebPageUrl(Type t) => (string)t.GetProperty(nameof(PluginInformation.PluginWebPageUrl), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public bool? GetPluginNewVersionPublished(Type t) => (bool?)t.GetProperty(nameof(PluginInformation.PluginNewVersionPublished), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string GetPluginNewVersionWebPageUrl(Type t) => (string)t.GetProperty(nameof(PluginInformation.PluginNewVersionWebPageUrl), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
    }
}
