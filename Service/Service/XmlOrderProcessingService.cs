using Serilog;
using SftpXmlTask.DataAccess;
using SftpXmlTask.DTO.Models;

namespace SftpXmlTask.Service
{
    public class XmlOrderProcessingService : IOrderProcessingService
    {

        private readonly IOrderProcessingDataAccess _dataAccess;
        private readonly ILogger _logger;

        public XmlOrderProcessingService(IOrderProcessingDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _logger = Log.Logger.ForContext(typeof(XmlOrderProcessingService));
        }

        public async Task<List<PurchaseOrder>> GetOrders() 
        {
            return await _dataAccess.GetPurchaseOrders();
        }

        public async Task<bool> ProcessOrders(List<PurchaseOrder> purchaseOrders)
        {
            _logger.Information("Processing purchase orders");

            bool isOrderProcessed = false;
            foreach (var purchaseOrder in purchaseOrders) 
            {
                _logger.Information("Process order " + purchaseOrder.ReferenceNumber);

                isOrderProcessed = await _dataAccess.IsOrderProcessed(purchaseOrder.ReferenceNumber);

                if(isOrderProcessed) 
                {
                    _logger.Information("Order " + purchaseOrder.ReferenceNumber + " already processed. Skipping");
                    continue;
                }

                var purcahseOrderId = await _dataAccess.ProcessPurchaseOrder(purchaseOrder.ReferenceNumber, purchaseOrder.PurchaseDate);

                if (purcahseOrderId == 0)
                {
                    _logger.Error("Failed to process order "  + purchaseOrder.ReferenceNumber);
                    continue;
                }

                await _dataAccess.AddPurchaseOrderCustomer(purcahseOrderId, purchaseOrder.Customer.FirstName, purchaseOrder.Customer.LastName, purchaseOrder.Customer.OIB);

                await _dataAccess.AddPurchaseOrderItems(purcahseOrderId, purchaseOrder.Product);

                _logger.Information("Processed order " + purchaseOrder.ReferenceNumber);
            }

            return true;
        }

    }
}
