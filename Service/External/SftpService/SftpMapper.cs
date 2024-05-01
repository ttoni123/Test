using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SftpXmlTask.SftpService
{
    public static class SftpMapper
    {
        public static IMapper Mapper
        {
            get;
        }

        static SftpMapper()
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

