using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SellSpasibo.API.ApiModels.Admin;
using SellSpasibo.Core.Interfaces;

namespace SellSpasibo.API.Controllers.Admin
{
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly IPayerAccountManager _accountManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IPayerAccountManager accountManager, ILogger<AdminController> logger)
        {
            _accountManager = accountManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTinkoffAccount([FromBody] AddTinkoffAccountAPI tinkoffAccountInfo)
        {
            try
            {
                await _accountManager.AddTinkoffAccount(tinkoffAccountInfo.Number, tinkoffAccountInfo.Password,
                tinkoffAccountInfo.AccountId);
                _logger.LogInformation("Платёжный аккаунт с логином {0} успешно был добавлен", tinkoffAccountInfo.Number);
                return Ok(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Платёжный аккаунт с логином {0} не был добавлен", tinkoffAccountInfo.Number);
                return BadRequest(false);
            }
        }
    }
}