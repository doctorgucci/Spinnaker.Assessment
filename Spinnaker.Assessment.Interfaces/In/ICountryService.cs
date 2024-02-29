using Spinnaker.Assessment.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinnaker.Assessment.Interfaces.In
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries();
    }
}
