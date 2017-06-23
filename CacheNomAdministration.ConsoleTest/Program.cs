using CacheNomAdministration.SoapServices.SoapProxy;
using CacheNomAdministration.SoapServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheNomAdministration.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 500;
            ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
            client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));

            var configuration = client.GetConfiguration();

            foreach (var tag in configuration.OrderBy(x=>x.TotalizerTag).ThenBy(x=>x.ControlPointID))
            {
                Console.WriteLine($"{nameof(tag.TotalizerTag), 15}: {tag.TotalizerTag, 30} | {nameof(tag.ControlPointID), 20}: {tag.ControlPointID, 20} | {nameof(tag.ControlerCode), 20}: {tag.ControlerCode, 20} | {nameof(tag.ControlPointName), 20}: {tag.ControlPointName, 20} ");
            }
        }
    }
}
