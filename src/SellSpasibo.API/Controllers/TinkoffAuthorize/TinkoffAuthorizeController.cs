using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SellSpasibo.API.ApiModels.TinkoffAuthorize;
using SellSpasibo.Core.Interfaces.AuthorizationService;

namespace SellSpasibo.API.Controllers.TinkoffAuthorize
{
    [Route("api/[controller]/[action]")]
    public class TinkoffAuthorizeController : ControllerBase
    {
        private readonly ILogger<TinkoffAuthorizeController> _logger;
        private readonly ITinkoffAuthorizationService _tinkoffAuthorizationService;

        public TinkoffAuthorizeController(ILogger<TinkoffAuthorizeController> logger, ITinkoffAuthorizationService tinkoffAuthorizationService)
        {
            _logger = logger;
            _tinkoffAuthorizationService = tinkoffAuthorizationService;
        }

        [HttpPost]
        public async Task<IActionResult> StartAuthorize([FromBody] StartAuthorizeAPI startAuthorizeApi)
        {
            try
            {
                await _tinkoffAuthorizationService.StartAuthorizeInAccount(startAuthorizeApi.Number);
                _logger.LogInformation("Запущено начало авторизации по логину {0}", startAuthorizeApi.Number);
                return Ok(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Не удалось запустить начало авторизации по логину {0}", startAuthorizeApi.Number);
                return BadRequest(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ContinueAuthorize([FromBody] ContinueAuthorize continueAuthorize)
        {
            try
            {
                await _tinkoffAuthorizationService.ContinueAuthorize(continueAuthorize.Number, continueAuthorize.Sms);
                _logger.LogInformation("Успешная авторизации по логину {0}", continueAuthorize.Number);
                return Ok(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Не удалось запустить начало авторизации по логину {0}", continueAuthorize.Number);
                return BadRequest(false);
            }
        }
    }
}