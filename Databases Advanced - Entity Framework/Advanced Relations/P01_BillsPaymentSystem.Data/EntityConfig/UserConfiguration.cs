using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.FirstName).HasColumnType("nvarchar(50)").IsRequired();

            builder.Property(x => x.LastName).HasColumnType("nvarchar(50)").IsRequired();

            builder.Property(x => x.Email).HasColumnType("varchar(80)").IsRequired();

            builder.Property(x => x.Password).HasColumnType("varchar(25)").IsRequired();

            builder.HasMany(x => x.PaymentMethods)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
