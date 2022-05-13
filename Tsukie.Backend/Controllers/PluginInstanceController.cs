using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tsukie.Backend.Models.Plugin;

namespace Tsukie.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginInstanceController : ControllerBase
    {
        private PluginInstanceManager InstanceManager { get; }

        private ILogger<PluginInstanceController> Logger { get; }

        public PluginInstanceController(PluginInstanceManager instanceManager, ILogger<PluginInstanceController> logger)
        {
            InstanceManager = instanceManager;
            Logger = logger;
        }
        [Route("")]
        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }
        [Route("")]
        [HttpGet]
        public IActionResult List()
        {
            return Ok();
        }

        [Route("{instanceId}")]
        [HttpGet]
        public IActionResult Read(string instanceId)
        {
            return Ok();
        }

        [Route("{instanceId}")]
        [HttpDelete]
        public IActionResult Delete(string instanceId)
        {
            return Ok();
        }

        [Route("{instanceId}/stop")]
        [HttpGet]
        public IActionResult Stop(string instanceId)
        {
            return Ok();
        }

        [Route("{instanceId}/start")]
        [HttpGet]
        public IActionResult Start(string instanceId)
        {
            return Ok();
        }
    }
}
