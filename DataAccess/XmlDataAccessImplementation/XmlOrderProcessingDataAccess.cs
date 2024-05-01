using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Serilog;
using SftpXmlTask.DataAccess.XmlDataAccessImplementation;
using SftpXmlTask.DataAccess.XmlDataAccessImplementation.Models;
using SftpXmlTask.DTO.Models;
using System.Security.Cryptography;

namespace SftpXmlTask.DataAccess.TaskImplementation
{
    public class XmlOrderProcessingDataAccess : IOrderProcessingDataAccess
    {
        private readonly DatabaseContext _cscDb;
        private readonly ILogger _logger;
        private readonly int _dbTimeout;
        private readonly string _connString;

        public XmlOrderProcessingDataAccess(IConfiguration configuration)
        {
            _connString = configuration["ConnectionStrings:Database"]!;

            DbContextOptionsBuilder<DatabaseContext> optionsBuilderAccounts = new();
            optionsBuilderAccounts.UseSqlServer
            (
                _connString,
                sqlServerOptions => sqlServerOptions
                    .CommandTimeout(150)
                    .UseCompatibilityLevel(120)
            );

            optionsBuilderAccounts.EnableSensitiveDataLogging(true);

            _cscDb = new DatabaseContext(optionsBuilderAccounts.Options);

            _logger = Log.Logger.ForContext(typeof(XmlOrderProcessingDataAccess));
        }

        public async Task<List<DTO.Models.PurchaseOrder>> GetPurchaseOrders() 
        {
            _logger.Information("Get all purchase orders");

            var response = _cscDb.VPurchaseOrderInformations;

            var purchaseOrders = await DatabaseMapper.Mapper.ProjectTo<DTO.Models.PurchaseOrder>(response).ToListAsync();

            foreach (var purchaseOrder in purchaseOrders) 
            {
                var items = _cscDb.PurchaseOrderItems
                    .Where
                    (
                        g => g.PurchaseOrderId == (int)purchaseOrder.PurchaseOrderId!
                    );

                purchaseOrder.Product = DatabaseMapper.Mapper.ProjectTo<Product>(items).ToList();
            }



            return purchaseOrders;
        }

        public async Task<int> AddPurchaseOrderCustomer(int purchaseOrderId, string firstName, string lastName, string oib)
        {
            _logger.Information("add purchase order customer");

            var entity = await _cscDb.PurchaseOrderCustomers
                .AddAsync
                (
                    new PurchaseOrderCustomer
                    {
                        LastName = lastName,
                        FirstName = firstName,
                        Oib = oib,
                        PurchaseOrderId = purchaseOrderId
                    }
                );

            await _cscDb.SaveChangesAsync();

            return entity.Entity.PurchaseOrderId;
        }

        public async Task<bool> AddPurchaseOrderItems(int purchaseOrderId, List<DTO.Models.Product> products)
        {
            foreach( var product in products )
            {
                var entity = await _cscDb.PurchaseOrderItems
                    .AddAsync
                    (
                        new PurchaseOrderItem
                        {
                            PurchaseOrderId = purchaseOrderId,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            Quantity = product.Quantity
                        }
                    );

                await _cscDb.SaveChangesAsync();

            }

            return true;
        }

        public async Task<bool> IsOrderProcessed(int referenceNumber) 
        {
            _logger.Information("Check if order is already processed");

            var result = await _cscDb.PurchaseOrders
                .AnyAsync
                (
                    g => g.PurchaseOrderReferenceNumber == referenceNumber
                );

            return result;
        }

        public async Task<int> ProcessPurchaseOrder(int referenceNumber, DateTime purchasedate) 
        {
            _logger.Information("Add new purchase order");

            var entity = await _cscDb.PurchaseOrders
                .AddAsync
                (
                    new XmlDataAccessImplementation.Models.PurchaseOrder
                    {
                        PurchaseOrderReferenceNumber = referenceNumber,
                        PurchaseOrderDate = purchasedate
                    }
                );

            await _cscDb.SaveChangesAsync();

            return entity.Entity.PurchaseOrderId;
        }

    }
}
