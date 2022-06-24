using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .UseIdentityColumn()
                   .HasColumnType("BIGINT");
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(80)
                   .HasColumnName("NAME")
                   .HasColumnType("VARCHAR(80)");
            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(300)
                   .HasColumnName("EMAIL")
                   .HasColumnType("VARCHAR(300)");
            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("PASSWORD")
                   .HasColumnType("VARCHAR(100)");      
        }
    }
}