namespace Inventory.Data.ModelBuilders
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class ProductBuilder
    {
        public static void Build(EntityTypeBuilder<Models.DataModels.Product> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .UseIdentityColumn();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
