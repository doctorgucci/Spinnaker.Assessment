using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spinnaker.Asessment.SQLIntegration.Entities;
using Spinnaker.Asessment.SQLIntegration.Extensions;

namespace Spinnaker.Asessment.SQLIntegration.Mappings
{
    public class CustomerEntityMap : EntityTypeConfiguration<CustomerEntity>
    {
        public override void Map(EntityTypeBuilder<CustomerEntity> builder)
        {

            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Property(x => x.Name);
            builder.Property(x => x.Surname);
            builder.Property(x => x.IdentityNumber);
            builder.Property(x => x.Email);
            builder.Property(x => x.PhoneNumber);
            builder.Property(x => x.CountriesId);

            builder.HasOne(x => x.Countries).WithMany(x => x.Customers).HasForeignKey(x => x.CountriesId);
        }
    }
}
