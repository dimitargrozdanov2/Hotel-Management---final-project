using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Data.Configurations
{
    internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(ConfigConstants.NameLength);

            builder.Property(a => a.Comment)
                .HasMaxLength(ConfigConstants.CommentLength)
                .IsRequired();
        }
    }
}
