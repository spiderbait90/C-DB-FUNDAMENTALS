using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(x => x.CreditCardId);

            builder.Property(x => x.Limit).IsRequired();

            builder.Property(x => x.MoneyOwed).IsRequired();

            builder.Property(x => x.ExpirationDate).IsRequired();

            builder.Ignore(x => x.LimitLeft);

            builder.Ignore(x => x.PaymentMethodId);

        }
    }
}
