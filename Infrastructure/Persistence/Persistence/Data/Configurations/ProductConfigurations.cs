using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.HasOne(navigationExpression:P=>P.ProductBrand)
                  .WithMany() //
                  .HasForeignKey(foreignKeyExpression:P=>P.BrandId);

            builder.HasOne(navigationExpression: P => P.ProductType)
                   .WithMany()
                   .HasForeignKey(foreignKeyExpression: P => P.TypeId);

            builder.Property(P => P.Price)
                .HasColumnType(typeName: "decimal(10,2)");
        }
    }
}
