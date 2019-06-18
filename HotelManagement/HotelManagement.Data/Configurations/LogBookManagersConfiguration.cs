using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.Configurations
{
    internal class LogBookManagersConfiguration : IEntityTypeConfiguration<LogbookManagers>
    {
        public void Configure(EntityTypeBuilder<LogbookManagers> builder)
        {
            builder
                .HasKey(um => new { um.ManagerId, um.LogbookId });

            builder
               .HasOne(um => um.Logbook)
               .WithMany(u => u.LogbookManagers)
               .HasForeignKey(um => um.LogbookId);

            builder
                .HasOne(um => um.Manager)
                .WithMany(u => u.LogbookManagers)
                .HasForeignKey(um => um.ManagerId);
        }
    }
}