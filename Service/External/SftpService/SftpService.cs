using Renci.SshNet;
using Serilog;
using SftpXmlTask.DTO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace SftpXmlTask.SftpService
{
    public class SftpService : ISftpService
    {

        private readonly ILogger _logger;
        private readonly SftpConfiguration _sftpConfiguration;

        public SftpService(SftpConfiguration configuration)
        {
            _sftpConfiguration = configuration;
            _logger = Log.Logger.ForContext(typeof(SftpService));
        }

        public bool ExportOrders(List<PurchaseOrder> orders) 
        {

            var result = SftpMapper.Mapper.ProjectTo<CUSTOMER>(orders.AsQueryable());

            var customer = result.ToList();

            foreach(var item in customer) 
            {
                item.ORDERS.ITEM = SftpMapper.Mapper.ProjectTo<ITEM>(orders.First().Product.AsQueryable()).ToList();

                if (!orders.Any()) 
                {
                    break;
                }
            }

            ORDERFILE file = new ORDERFILE() { CUSTOMER = customer };

            XmlSerializer ser = new XmlSerializer(typeof(ORDERFILE));


            var xml = "";
            var filePath = "C:\\Users\\tobr\\Documents\\" + "Orders" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml";
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    ser.Serialize(writer, file);
                    xml = sww.ToString(); // Your XML
                    System.IO.File.WriteAllText(filePath, xml);

                    writer.Dispose();
                }
                sww.Dispose();
            }
            var fileStream = new FileStream(filePath, FileMode.Open);
            using (var sftp = new SftpClient(_sftpConfiguration.Host, _sftpConfiguration.UserName, _sftpConfiguration.Password))
            {
                sftp.Connect();

                if (!sftp.IsConnected)
                {
                    throw new Exception("Failed to connect to sftp");
                }
                sftp.BufferSize = 1024;

                sftp.UploadFile(fileStream, "/" + "Orders" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml");

                sftp.Dispose();
            }

            return true;
        }

        public List<PurchaseOrder> GetData(string path)
        {

            XmlDocument doc = new XmlDocument();
            using (var sftp = new SftpClient(_sftpConfiguration.Host, _sftpConfiguration.UserName, _sftpConfiguration.Password))
            {
                sftp.Connect();

                if (!sftp.IsConnected) 
                {
                    throw new Exception("Failed to connect to sftp");
                }

                doc.Load(sftp.OpenRead(path ?? _sftpConfiguration.FilePath));

                sftp.Dispose();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(ORDERFILE));

            using (StringReader reader = new StringReader(doc.OuterXml)) 
            {
                var xml = serializer.Deserialize(reader);

                if (xml == null) 
                {
                    throw new Exception("Failed to deserialize xml"); //I'll throw regular exception to avoid creating custom errors and middleware
                }

                var parsedXml = (ORDERFILE)xml!;

                var result = SftpMapper.Mapper.ProjectTo<PurchaseOrder>(parsedXml.CUSTOMER.AsQueryable());

                reader.Close();

                return result.ToList();

            }
        }
    }
}
