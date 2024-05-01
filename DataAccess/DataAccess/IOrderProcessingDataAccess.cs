using SftpXmlTask.DTO.Models;

namespace SftpXmlTask.DataAccess
{
    public interface IOrderProcessingDataAccess
    {
        Task<List<DTO.Models.PurchaseOrder>> GetPurchaseOrders();

        Task<bool> IsOrderProcessed(int referenceNumber);

        Task<int> ProcessPurchaseOrder(int referenceNumber, DateTime purchasedate);

        Task<int> AddPurchaseOrderCustomer(int purchaseOrderId, string firstName, string lastName, string oib);

        Task<bool> AddPurchaseOrderItems(int purchaseOrderId, List<Product> products);
    }
}
