using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(a => a.Name)
               .HasMaxLength(ConfigConstants.NameLength)
               .IsRequired();
        }
    }
}
