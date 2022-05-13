using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tsukie.Backend.Models;
using Tsukie.Backend.Models.Exceptions;
using Tsukie.Backend.Models.Plugin;
using Tsukie.Backend.Models.Responses;
using Tsukie.Backend.Utilities;
using Tsukie.Integration.Models;

namespace Tsukie.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private PluginUtility PluginUtility { get; }
        private ILogger<PluginController> Logger { get; }

        public PluginController(ILogger<PluginController> logger, PluginUtility pluginUtility)
        {
            PluginUtility = pluginUtility;
            Logger = logger;
        }

        [Route("reload")]
        [HttpGet]
        public IActionResult Reload()
        {
            ResponseBase response = new ResponseBase();
            try
            {
                PluginUtility.ListPluginInfo(true);
            }
            catch (Exception ex)
            {
                response.FillByException(new GeneralException(string.Empty, ex));
                return Ok(response);
            }
            
            return Ok();
        }

        [Route("")]
        [HttpGet]
        public IActionResult ListPlugins()
        {
            ResponseBase response = new ResponseBase();
            IEnumerable<PluginInfo> pluginInfoList = PluginUtility.ListPluginInfo();
            IEnumerable<object> result = pluginInfoList.Select(t =>
            {
                string? pluginId = t.Id;
                string? pluginName = PluginUtility.GetPluginName(t.Type);
                string? pluginVersion = PluginUtility.GetPluginVersion(t.Type);
                string? pluginAuthorName = PluginUtility.GetPluginAuthorName(t.Type);
                return new
                {
                    Id = pluginId,
                    Name = pluginName,
                    Version = pluginVersion,
                    AuthorName = pluginAuthorName,
                };
            });
            response.Result = result;
            return Ok(response);
        }

        [Route("{pluginId}")]
        [HttpGet]
        public IActionResult Read(string pluginId)
        {
            ResponseBase response = new ResponseBase();
            IEnumerable<PluginInfo> pluginInfoList = PluginUtility.ListPluginInfo();
            PluginInfo? targetPluginInfo = pluginInfoList.FirstOrDefault(t =>
                t.Id.Equals(pluginId.Trim(), StringComparison.InvariantCultureIgnoreCase));
            if (targetPluginInfo == null)
            {
                return NotFound();
            }

            object result = new
            {
                Id = targetPluginInfo.Id,
                Type = targetPluginInfo.Type.FullName,
                Path = targetPluginInfo.Path,
                Name = PluginUtility.GetPluginName(targetPluginInfo.Type),
                Version = PluginUtility.GetPluginVersion(targetPluginInfo.Type),
                AuthorName = PluginUtility.GetPluginAuthorName(targetPluginInfo.Type),
                WebPageUrl = PluginUtility.GetPluginWebPageUrl(targetPluginInfo.Type),
                Description = PluginUtility.GetPluginDescription(targetPluginInfo.Type),
                NewVersionPublished = PluginUtility.GetPluginNewVersionPublished(targetPluginInfo.Type),
                NewVersionWebPageUrl =PluginUtility.GetPluginNewVersionWebPageUrl(targetPluginInfo.Type)
            };
            response.Result = result;
            return Ok(response);
        }
    }
}
