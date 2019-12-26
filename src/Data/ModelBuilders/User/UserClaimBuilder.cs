namespace Inventory.Data.ModelBuilders.User
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class UserClaimBuilder
    {
        public static void Build(EntityTypeBuilder<Models.DataModels.UserModels.UserClaim> entity)
        {
            entity.HasKey(e => new { e.Id, e.UserId });
            entity.Property(e => e.Id)
                .UseIdentityColumn();

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
