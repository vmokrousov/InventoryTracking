using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using InventoryTracking.Entities;
using InventoryTracking.Service.Dto;
using InventoryTracking.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracking.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public IInventoryService _service { get; }
        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// http://localhost:8081/inventory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var results = await _service.GetList();

            return Ok(results);
        }

        // GET http://localhost:8081/inventory/Apples,
        [HttpGet("{itemname}")]
        public async Task<IActionResult> Get(string itemname)
        {
            var results = await _service.GetByName(itemname);
            return Ok(results);
        }

        /// <summary>
        /// POST http://localhost:8081/inventory,
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{itemname}")]
        public async Task<IActionResult> Update([FromBody] List<Inventory> request)
        {
            await _service.Save(request);
            return Ok();
        }

        /// <summary>
        /// PUT http://localhost:8081/inventory/Apples,
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{itemname}")]
        public async Task<IActionResult> Update([FromBody] Inventory request)
        {
            await _service.UpdateInventory(request);
            return Ok();
        }

        [HttpDelete(), Route("{itemname}")]
        public async Task<IActionResult> Delete(string itemname)
        {
            await _service.DeleteInventory(itemname);
            return Ok();
        }

        [HttpDelete(), Route("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<string> itemnames)
        {
            await _service.BulkDeleteInventory(itemnames);
            return Ok();
        }

        /// <summary>
        /// POST http://localhost:8081/inventory/search,
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(List<Inventory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromBody] InventoryFilter request)
        {
            var results  = await _service.Search(request);
            return Ok(results);
        }
    }
}
