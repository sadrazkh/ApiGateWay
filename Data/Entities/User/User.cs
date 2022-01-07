using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Data.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.User
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public User()
        {
            IsDeleted = false;
        }

        public string FullName { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

    }


    //Fluent Api Configuration
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

        }
    }

    public enum Gender
    {
        Men,
        Women
    }
}
