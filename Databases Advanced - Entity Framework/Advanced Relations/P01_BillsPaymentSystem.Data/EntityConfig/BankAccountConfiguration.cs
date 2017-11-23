using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.BankAccountId);

            builder.Property(x => x.BankAccountId).IsRequired();

            builder.Property(x => x.Balance).IsRequired();

            builder.Property(x => x.BankName).HasColumnType("nvarchar(50)").IsRequired();

            builder.Property(x => x.SwiftCode).HasColumnType("varchar(20)").IsRequired();

            builder.Ignore(x => x.PaymentMethodId);
        }
    }
}
