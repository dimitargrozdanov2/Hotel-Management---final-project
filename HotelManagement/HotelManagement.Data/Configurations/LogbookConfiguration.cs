using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Data.Configurations
{
    public class LogbookConfiguration : IEntityTypeConfiguration<Logbook>
    {
        public void Configure(EntityTypeBuilder<Logbook> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
