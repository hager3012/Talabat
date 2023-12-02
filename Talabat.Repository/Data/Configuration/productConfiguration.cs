using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configuration
{
    internal class productConfiguration : IEntityTypeConfiguration<product>
    {
        public void Configure(EntityTypeBuilder<product> builder)
        {
            builder.Property(P => P.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P =>P.PictureUrl).IsRequired();
            builder.Property(P =>P.Price).IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.HasOne(P => P.ProductBrand).WithMany()
                .HasForeignKey(P => P.BrandId);
            builder.HasOne(P => P.ProductCategory).WithMany()
                .HasForeignKey(P =>P.CategoryId);
        }
    }
}
