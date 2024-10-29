using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsApi.Data.Model.ModelConfiguration
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(m => m.TransferID);

            builder.HasOne(m => m.User)
                .WithMany(m => m.Transfer)
                .HasForeignKey(m => m.UserIdCard);

            //builder.Property(m => m.UserIdCard).HasMaxLength(13);
            builder.Property(m => m.TransferDate).HasDefaultValueSql("getdate()");
        }
    }
}