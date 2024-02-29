using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spinnaker.Asessment.SQLIntegration.Entities;
using Spinnaker.Asessment.SQLIntegration.Extensions;

namespace Spinnaker.Asessment.SQLIntegration.Mappings
{
    public class CountriesEntityMap : EntityTypeConfiguration<CountriesEntity>
    {
        public override void Map(EntityTypeBuilder<CountriesEntity> builder)
        {

            builder.ToTable("Countries");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Iso);
            builder.Property(x => x.Name);
            builder.Property(x => x.FriendlyName);
            builder.Property(x => x.Iso3);
            builder.Property(x => x.NumCode);
            builder.Property(x => x.PhoneCode);

        }
    }
}
