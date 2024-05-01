using AutoMapper;
using SftpXmlTask.DataAccess.XmlDataAccessImplementation.Models;
using SftpXmlTask.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SftpXmlTask.DataAccess.XmlDataAccessImplementation
{
    public class PurchaseOrderDataProfile : Profile
    {
        public PurchaseOrderDataProfile()
        {
            CreateMap<VPurchaseOrderInformation, DTO.Models.PurchaseOrder>()
                .ForMember
                (
                    dest => dest.ReferenceNumber,
                    map => map.MapFrom(src => src.PurchaseOrderReferenceNumber)
                )
                .ForMember
                (
                    dest => dest.PurchaseDate,
                    map => map.MapFrom(src => src.PurchaseOrderDate)
                )
                .ForMember
                (
                    dest => dest.Customer,
                    map => map.MapFrom(src => new DTO.Models.Customer
                    {
                        FirstName = src.FirstName,
                        LastName = src.LastName,
                        OIB = src.Oib
                    })
                );


            CreateMap<PurchaseOrderItem, DTO.Models.Product>()
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
