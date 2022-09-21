using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paycore.Repository.Configurations
{
    internal class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.BidPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.UserAppId).IsRequired();
            builder.Property(x => x.IsConfirm).HasDefaultValue(false);

            //Tablo ilişkileri
            builder.HasOne(x => x.Product).WithMany(x => x.Offers).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.UserApp).WithMany(x => x.Offers).HasForeignKey(x => x.UserAppId);
        }
    }
}
