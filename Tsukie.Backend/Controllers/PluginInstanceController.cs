using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tsukie.Backend.Models.Exceptions;
using Tsukie.Backend.Models.Plugin;
using Tsukie.Backend.Models.Requests;
using Tsukie.Backend.Models.Responses;
using Tsukie.Backend.Utilities;

namespace Tsukie.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginInstanceController : ControllerBase
    {
        private PluginUtility PluginUtility { get; }
        private PluginInstanceManager InstanceManager { get; }

        private ILogger<PluginInstanceController> Logger { get; }

        public PluginInstanceController(PluginInstanceManager instanceManager,PluginUtility pluginUtility, ILogger<PluginInstanceController> logger)
        {
            InstanceManager = instanceManager;
            PluginUtility = pluginUtility;
            Logger = logger;
        }
        [Route("")]
        [HttpPost]
        public IActionResult Create([FromBody] PluginInstanceCreateRequest request)
        {
            ResponseBase response = new ResponseBase();
            PluginInstanceInfo info = new PluginInstanceInfo()
            {
                TypeId = request.TypeId,
                Type = PluginUtility.FindById(request.TypeId).Type,
                Name = request.Name,
                CqServerAddress = request.CqServerAddress,
                CqServerPort = request.CqServerPort
            };
            PluginInstance instance = InstanceManager.Create(info);
            response.Result = instance.Id;
            return Ok(response);
        }
        [Route("")]
        [HttpGet]
        public IActionResult List()
        {
            ResponseBase response = new ResponseBase();
            IEnumerable<PluginInstanceInfo> result = InstanceManager.PluginInstanceList.Select(t => (PluginInstanceInfo) t);
            response.Result = result;
            return Ok(response);
        }

        [Route("{instanceId}")]
        [HttpGet]
        public IActionResult Read(string instanceId)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                PluginInstanceInfo result = InstanceManager.Find(instanceId);
                response.Result = result;
                
            }
            catch (PluginTypeNotFoundException ex)
            {
                response.FillByException(ex);
                return NotFound(response);
            }
            return Ok(response);
        }

        [Route("{instanceId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string instanceId)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                await InstanceManager.DeleteAsync(instanceId);

            }
            catch (PluginTypeNotFoundException ex)
            {
                response.FillByException(ex);
                return NotFound(response);
            }
            return Ok(response);
        }

        [Route("{instanceId}/stop")]
        [HttpGet]
        public async Task<IActionResult> Stop(string instanceId)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                await InstanceManager.StopAsync(instanceId);
            }
            catch (PluginTypeNotFoundException ex)
            {
                response.FillByException(ex);
                return NotFound(response);
            }
            return Ok(response);
        }

        [Route("{instanceId}/start")]
        [HttpGet]
        public async Task<IActionResult> Start(string instanceId)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                await InstanceManager.StartAsync(instanceId);
                
            }
            catch (PluginTypeNotFoundException ex)
            {
                response.FillByException(ex);
                return NotFound(response);
            }
            return Ok(response);

        }
    }
}
