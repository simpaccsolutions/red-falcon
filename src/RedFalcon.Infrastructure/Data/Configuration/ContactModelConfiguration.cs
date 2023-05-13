using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Infrastructure.Data.Configuration
{
    public class ContactModelConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Firstname).HasMaxLength(100).IsRequired(true);
            builder.Property(o => o.Lastname).HasMaxLength(100).IsRequired(false);
            builder.Property(pr => pr.BirthDate).HasColumnType("datetime").IsRequired(false);
            builder.Property(o => o.Email).HasMaxLength(100).IsRequired(false);
            builder.Property(o => o.Phone).HasMaxLength(50).IsRequired(false);

            builder.Property(cc => cc.CreatedBy).HasMaxLength(450);
            builder.Property(cc => cc.DateCreated).HasColumnType("datetime");
            builder.Property(cc => cc.UpdatedBy).HasMaxLength(450);
            builder.Property(cc => cc.DateUpdated).HasColumnType("datetime");
        }
    }
}
