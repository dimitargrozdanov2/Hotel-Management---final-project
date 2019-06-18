using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.Configurations
{
    internal class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.Property(a => a.Name)
               .HasMaxLength(ConfigConstants.NameLength)
               .IsRequired();
        }
    }
}