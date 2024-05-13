using SftpXmlTask.DTO.Models;

namespace SftpXmlTask.SftpService
{
    public interface ISftpService
    {
        List<PurchaseOrder> GetData(string path);

        bool ExportOrders(List<PurchaseOrder> orders);
    }
}
