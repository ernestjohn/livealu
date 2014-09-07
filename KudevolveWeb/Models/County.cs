using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KudevolveWeb.Models
{
    public class County
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Governor { get; set; }
        public string Senator { get; set; }
        public Dictionary<string, Dictionary<string,string>> GeneralInformation { get; set; }
        public Dictionary<string, Dictionary<string, string>> Population { get; set; }
        public Dictionary<string, Dictionary<string, string>> Health { get; set; }
        public Dictionary<string, Dictionary<string, string>> Funding { get; set; }
        public Dictionary<string, Dictionary<string, string>> Infrastructure { get; set; }
        public Dictionary<string, Dictionary<string, string>> ServiceCoverage { get; set; }
    }
}