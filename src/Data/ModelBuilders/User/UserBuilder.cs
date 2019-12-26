namespace Inventory.Data.ModelBuilders.User
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class UserBuilder
    {
        public static void Build(EntityTypeBuilder<Models.DataModels.UserModels.User> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .UseIdentityColumn();

            entity.HasIndex(e => e.PhoneNumber)
                .IsUnique();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.HasIndex(e => e.UserName)
                .IsUnique();

            entity.Property(e => e.Title)
                .HasMaxLength(50);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.Surname)
                .HasMaxLength(250);

            entity.Property(e => e.NationCode)
                .HasMaxLength(10);

            entity.Property(e => e.IdNumber)
                .HasMaxLength(50);

            entity.Property(e => e.HomePhoneNumber)
                .HasMaxLength(50);

            entity.Property(e => e.WorkPhoneNumber)
                .HasMaxLength(50);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50);

            entity.Property(e => e.Fax)
                .HasMaxLength(50);

            entity.Property(e => e.PostalCode)
                .HasMaxLength(50);

            entity.Property(e => e.RegisterDateTime)
                .IsRequired();

            entity.Property(e => e.LastLoginDateTime);

            entity.Property(e => e.LastUpdateDateTime);

            entity.Property(e => e.Disabled)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
