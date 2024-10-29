using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsApi.Data.Model.ModelConfiguration
{
    public class WithdrawConfiguration : IEntityTypeConfiguration<Withdraw>
    {
        public void Configure(EntityTypeBuilder<Withdraw> builder)
        {
            builder.HasKey(m => m.WithdrawID);
            builder.HasOne(m => m.User)
                .WithMany(m => m.Withdraw)
                .HasForeignKey(m => m.UserIdCard);

            //builder.Property(m => m.UserIdCard).HasMaxLength(13);
            builder.Property(m => m.WithdrawDate).HasDefaultValueSql("getdate()");
        }
    }
}