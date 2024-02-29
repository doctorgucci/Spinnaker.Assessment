using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinnaker.Asessment.SQLIntegration.Entities
{
    public partial class CustomerEntity
    {     

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber
        {
            get; set;
        }

        
        public int CountriesId { get; set; }

        public CountriesEntity Countries { get; set; }
    }
}
