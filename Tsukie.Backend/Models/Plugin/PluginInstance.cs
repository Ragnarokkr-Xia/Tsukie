using Sora.Interfaces;

namespace Tsukie.Backend.Models.Plugin
{
    public class PluginInstance:IDisposable
    {
        private bool _disposed;

        public PluginInstanceInfo? Info { get; set; } = new PluginInstanceInfo();

        public Integration.Models.Plugin? HostPlugin { get; set; }

        public ISoraService? SoraService { get; set; }

        public void Stop()
        {
            SoraService?.StopService();
        }

        public void Start()
        {
            SoraService?.StartService();
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

                Info = null;
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
