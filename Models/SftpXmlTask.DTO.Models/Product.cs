using System.Xml.Serialization;

namespace SftpXmlTask.DTO.Models
{
    public class Product
    {
        public string ProductName
        {
            get; set;
        } = null!;

        public int Quantity 
        {
            get; set;
        }

        public decimal Price 
        {
            get; set;
        }
    }
}
