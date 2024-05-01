using System.Xml.Serialization;

namespace SftpXmlTask.DTO.Models
{
    public class Customer
    {
        public string FirstName
        {
            get; set;
        } = null!;

        public string LastName
        {
            get; set;
        } = null!;

        public string OIB
        {
            get; set;
        } = null!;
    }
}
