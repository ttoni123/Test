using SftpXmlTask.DTO.Models;

namespace SftpXmlTask.SftpService
{
    public interface ISftpService
    {
        List<PurchaseOrder> GetData();

        bool ExportOrders(List<PurchaseOrder> orders);
    }
}
