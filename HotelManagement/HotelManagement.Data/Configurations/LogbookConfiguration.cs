using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.Configurations
{
    internal class LogbookConfiguration : IEntityTypeConfiguration<Logbook>
    {
        public void Configure(EntityTypeBuilder<Logbook> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(ConfigConstants.NameLength)
                .IsRequired();
        }
    }
}