using SftpXmlTask.DTO.Models;

namespace SftpXmlTask.Service
{
    public interface IOrderProcessingService
    {
        Task<List<PurchaseOrder>> GetOrders();

        Task<bool> ProcessOrders(List<PurchaseOrder> purchaseOrders);
    }
}
