using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Data.Entities.Common;

namespace Data.Entities.Role
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Role(string name) : base(name)
        {

        }

        public Role(string name, string description) : base(name)
        {
            Description = description;
        }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        #region NavigationProperties



        #endregion

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
