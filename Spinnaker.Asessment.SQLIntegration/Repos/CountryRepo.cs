using Microsoft.EntityFrameworkCore;
using Spinnaker.Asessment.SQLIntegration.Base;
using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinnaker.Asessment.SQLIntegration.Repos
{
    public class CountryRepo: BaseRepo, ICountryRepo
    {
        public CountryRepo(CustomerDbContext context) : base(context) { }

        public async Task<List<Country>> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();

            return countries.Select(item => new Country
            {
                Id = item.Id,
                Iso = item.Iso,
                Name = item.Name,
                FriendlyName = item.FriendlyName,
                NumCode = item.NumCode,
                PhoneCode = item.PhoneCode

            }).ToList();
        }
    }
}
