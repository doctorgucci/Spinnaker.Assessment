using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinnaker.Assessment.DomainModels
{
    public class Country
    {
        public int Id { get; set; }
        public string? Iso { get; set; }
        public string? Name { get; set; }
        public string? FriendlyName { get; set; }
        public string? Iso3 { get; set; }
        public int NumCode { get; set; }
        public int PhoneCode { get; set; }
    }
}
