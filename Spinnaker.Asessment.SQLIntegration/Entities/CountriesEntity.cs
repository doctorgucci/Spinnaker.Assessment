using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinnaker.Asessment.SQLIntegration.Entities
{
    public partial class CountriesEntity
    {
        public int Id { get; set; }
        public required string Iso { get; set; }
        public required string Name { get; set; }
        public required string FriendlyName { get; set; }
        public required string Iso3 { get; set; }
        public int NumCode { get; set; }
        public int PhoneCode { get; set; }

        public ICollection<CustomerEntity> Customers { get; set; }
    }
}
