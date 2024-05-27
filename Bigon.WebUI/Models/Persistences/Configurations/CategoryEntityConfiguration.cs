using Bigon.WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.WebUI.Models.Persistences.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(x => x.ParentId)
                .HasPrincipalKey(x => x.Id);

            builder.ConfigureAsAuditable();

            builder.ToTable("Categories");
        }
    }
}
