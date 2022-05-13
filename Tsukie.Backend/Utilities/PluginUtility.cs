using System.Reflection;
using Tsukie.Backend.Models;
using Tsukie.Backend.Models.Plugin;
using Tsukie.Integration.Models;

namespace Tsukie.Backend.Utilities
{
    public class PluginUtility
    {
        public PluginUtility(ILogger<PluginUtility>? logger)
        {
            Logger = logger;
        }

        public string? BaseFolder { get; set; } = string.Empty;
        public ILogger? Logger { get; set; }

        public IEnumerable<PluginInfo>? Cache { get; set; }

        public IEnumerable<PluginInfo> ListPluginInfo(bool reload = false)
        {
            if (reload || Cache == null)
            {
                Cache = Discover();
            }
            return Cache;
        }
        public IEnumerable<PluginInfo> Discover()
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
                            if(type.IsSubclassOf(typeof(Plugin)))
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

        public string? GetPluginId(Type t) => (string?)t.GetProperty(nameof(Plugin.PluginId), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string? GetPluginName(Type t) => (string?)t.GetProperty(nameof(Plugin.PluginName), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string? GetPluginDescription(Type t) => (string?)t.GetProperty(nameof(Plugin.PluginDescription), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string? GetPluginVersion(Type t) => (string?)t.GetProperty(nameof(Plugin.PluginVersion), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string? GetPluginAuthorName(Type t) => (string?)t.GetProperty(nameof(Plugin.PluginAuthorName), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string? GetPluginWebPageUrl(Type t) => (string?)t.GetProperty(nameof(Plugin.PluginWebPageUrl), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public bool? GetPluginNewVersionPublished(Type t) => (bool?)t.GetProperty(nameof(Plugin.PluginNewVersionPublished), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
        public string? GetPluginNewVersionWebPageUrl(Type t) => (string?)t.GetProperty(nameof(Plugin.PluginNewVersionWebPageUrl), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
    }
}
