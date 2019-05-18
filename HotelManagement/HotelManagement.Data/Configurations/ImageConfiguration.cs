using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HotelManagement.DataModels;

namespace HotelManagement.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(80)
                .IsRequired();
        }
    }
}
