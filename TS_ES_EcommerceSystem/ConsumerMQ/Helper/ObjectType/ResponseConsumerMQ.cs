using ElasticSearchModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerMQ.Helper.ObjectType
{
    public class ResponseConsumerMQ
    {
        public string? Action { get; set; }
        public string? ApiType { get; set; }
        public EProduct? Data { get; set; }
    }
}
