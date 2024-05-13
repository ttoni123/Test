using Microsoft.AspNetCore.Mvc;
using SftpXmlTask.DTO.Models;
using SftpXmlTask.Service;
using SftpXmlTask.SftpService;

namespace SftpXmlTask.Api.TaskApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderProcessingController : ControllerBase
    {
        protected readonly IOrderProcessingService service;
        protected readonly ILogger<OrderProcessingController> logger;
        protected readonly ISftpService sftpService;

        public OrderProcessingController
        (
            ILogger<OrderProcessingController> logger,
            IOrderProcessingService service,
            ISftpService sftpService
        )
        {

            this.logger = logger;
            this.service = service;
            this.sftpService = sftpService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> ImportOrders(string path)
        {
            //We could add Check if the file is already processed, but i'll just use reference number in this case
            var purchaseOrders = sftpService.GetData(path);

            return await service.ProcessOrders(purchaseOrders);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> ExportOrders()
        {
            var orders = await service.GetOrders();
            sftpService.ExportOrders(orders);

            return true;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<PurchaseOrder>> PurchaseOrders()
        {
            return await service.GetOrders();
        }

    }
}
