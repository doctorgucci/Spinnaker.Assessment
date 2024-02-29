using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.In;
using Spinnaker.Assessment.Interfaces.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinnaker.Assessment.Domain
{
    public class CountryService: ICountryService
    {
        private readonly ICountryRepo _countryRepo;

        public CountryService(ICountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _countryRepo.GetCountries();
        }
    }
}
