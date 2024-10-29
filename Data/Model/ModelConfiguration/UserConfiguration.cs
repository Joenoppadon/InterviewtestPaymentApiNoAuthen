using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PaymentsApi.Data.Model.ModelConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.IdCard);
            //builder.Property(m => m.IdCard).HasMaxLength(13);
            //builder.Property(m => m.FullName).HasColumnType("nvarchar(100)");
            builder.Property(m => m.CreateDate).HasDefaultValueSql("getdate()"); //set datetime same db server
            builder.Property(m => m.IdCard).ValueGeneratedNever();

        }
    }
}