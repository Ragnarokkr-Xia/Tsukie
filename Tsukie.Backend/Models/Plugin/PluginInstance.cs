using Sora.Interfaces;

namespace Tsukie.Backend.Models.Plugin
{
    public class PluginInstance:PluginInstanceInfo,IDisposable
    {
        private bool _disposed;
        public Integration.Models.Plugin? HostPlugin { get; set; }
        public ISoraService? SoraService { get; set; }
        public PluginInstanceStatus Status { get; private set; } = PluginInstanceStatus.Stopped;
        public async Task StopAsync()
        {
            if (Status == PluginInstanceStatus.Running && SoraService != null)
            {
                await SoraService.StopService();
                Status = PluginInstanceStatus.Stopped;
            }
            
        }

        public async Task StartAsync()
        {
            if (Status == PluginInstanceStatus.Stopped && SoraService != null)
            {
                await SoraService.StartService();
                Status = PluginInstanceStatus.Running;
            }
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    SoraService?.Dispose();
                    HostPlugin?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
