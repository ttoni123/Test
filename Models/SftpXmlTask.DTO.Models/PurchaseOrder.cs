using AutoMapper;
using System;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SftpXmlTask.DTO.Models
{

    public class PurchaseOrder
    {
        public int? PurchaseOrderId 
        {
            get; set;
        }

        public Customer Customer
        {
            get; set;
        } = null!;

        public int ReferenceNumber 
        {
            get; set;
        }

        public List<Product> Product
        {
            get; set;
        } = null!;

        public DateTime PurchaseDate 
        {
            get; set;
        }
    }
}
