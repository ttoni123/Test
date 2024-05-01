using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SftpXmlTask.DataAccess.XmlDataAccessImplementation
{
    public static class DatabaseMapper
    {
        public static IMapper Mapper
        {
            get;
        }

        static DatabaseMapper()
        {
            var configuration = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.AddMaps(Assembly.GetExecutingAssembly());
                }
            );

            Mapper = configuration.CreateMapper();
        }
    }
}
