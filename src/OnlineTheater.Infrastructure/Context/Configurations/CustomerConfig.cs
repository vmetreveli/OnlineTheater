using Referendum.Domain.Entities;
using Permission = Referendum.Domain.Enums.Permission;

namespace Referendum.Infrastructure.Context.Configurations;

internal sealed class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        builder.HasKey(x => new {x.RoleId, x.UserId});
    }
}