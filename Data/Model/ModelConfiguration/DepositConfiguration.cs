using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsApi.Data.Model.ModelConfiguration
{
    public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> builder)
        {
            builder.HasKey(m => m.DepositID);
            builder.HasOne(m => m.User)
                .WithMany(m => m.Deposit)
                .HasForeignKey(m => m.UserIdCard);
            
            //builder.Property(m => m.UserIdCard).HasMaxLength(13);
            builder.Property(m => m.DepositDate).HasDefaultValueSql("getdate()");
            
        }
    }
}