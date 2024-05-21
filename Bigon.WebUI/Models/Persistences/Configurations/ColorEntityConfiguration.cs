using Bigon.WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.WebUI.Models.Persistences.Configurations
{
    public class ColorEntityConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int");
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(x => x.HexCode).HasColumnType("varchar").HasMaxLength(7).IsRequired();

            builder.ConfigureAsAuditable();

            builder.ToTable("Colors");
        }
    }
}
