using System.Diagnostics;
using Sora.Interfaces;
using Tsukie.Integration.Models;

namespace Tsukie.Backend.Models
{
    public class PluginInstance:IDisposable
    {
        private bool _disposed;

        public PluginInstanceInfo? Info { get; set; } = new PluginInstanceInfo();

        public Plugin? HostPlugin { get; set; }

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

                // 释放未托管的资源(未托管的对象)并重写终结器
                // 将大型字段设置为 null
                Info = null;
                _disposed = true;
            }
        }

        // // 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~PluginInstance()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
