using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanKiosk_Admin.APIServices
{
    public class ServiceRequest
    {
        public string BaseAddress { get; set; }
        public IDictionary<string, string> RequestParameters { get; set; }
        public Protocols HttpProtocol { get; set; }
        public string Body { get; set; }
    }

    public enum Protocols
    {
        HTTP_POST,
        HTTP_GET,
        HTTP_PUT,
        HTTP_DELETE
    }
}
