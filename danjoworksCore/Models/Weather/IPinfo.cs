using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace danjoworksCore.Models
{
    public class IPinfo
    {
        public string ip_address { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string continent { get; set; }
        public string continent_code { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string region { get; set; }
        public string region_code { get; set; }
        public string timezone { get; set; }
        public string owner { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string currency { get; set; }
        public List<string> languages { get; set; }
    }
}
