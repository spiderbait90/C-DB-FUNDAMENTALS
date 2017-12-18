using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Configurations
{
    public class UserConfig:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.LastName).IsRequired();

            builder.HasMany(x => x.ProductsBought)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId);

            builder.HasMany(x => x.ProductsSold)
                .WithOne(x => x.Buyer)
                .HasForeignKey(x => x.BuyerId);
        }
    }
}
