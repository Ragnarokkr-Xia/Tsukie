using System.Text.Json;
using Tsukie.Backend.Global;
using Tsukie.Backend.Models.Plugin;

namespace Tsukie.Backend.Utilities
{
    public class Initializer
    {
        public static async Task RestorePluginInstancesAsync(PluginUtility utility, PluginInstanceManager instanceManager)
        {
            string pluginInstanceRecordsPath =
                $"{Constants.CONFIG_FOLDER_NAME}{Path.DirectorySeparatorChar}{Constants.CONFIG_PLUGIN_INSTANCE_RECORDS_FILE_NAME}";
            if (File.Exists(pluginInstanceRecordsPath))
            {
                string pluginInstanceRecordsContent = await File.ReadAllTextAsync(pluginInstanceRecordsPath);
                List<PluginInstanceInfo> pluginInstanceRecords = JsonSerializer.Deserialize<List<PluginInstanceInfo>>(pluginInstanceRecordsContent);
                if (pluginInstanceRecords == null || pluginInstanceRecords.Count == 0)
                {
                    return;
                }
                for (int index = 0; index < pluginInstanceRecords.Count; index++)
                {
                    PluginInstanceInfo pluginInstanceRecord = pluginInstanceRecords[index];
                    try
                    {
                        PluginInfo pluginInfo = utility.FindById(pluginInstanceRecord.TypeId);
                        pluginInstanceRecord.Type = pluginInfo.Type;
                        PluginInstance instance = instanceManager.Create(pluginInstanceRecord);
                        if (pluginInstanceRecord.Status == PluginInstanceStatus.Running)
                        {
                            await instance.StartAsync();
                        }
                    }
                    catch
                    {
                        // Ignored
                    }


                }
            }
        }
    }
}
