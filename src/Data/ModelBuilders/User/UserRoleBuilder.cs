namespace Inventory.Data.ModelBuilders.User
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class UserRoleBuilder
    {
        public static void Build(EntityTypeBuilder<Models.DataModels.UserModels.UserRole> entity)
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .UseIdentityColumn();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
