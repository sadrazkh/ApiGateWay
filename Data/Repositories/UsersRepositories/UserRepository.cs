using Common;
using Common.Exceptions;
using Common.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts.UserContracts;
using Data.Entities.Role;
using Data.Entities.User;

namespace Data.Repositories.UsersRepositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager)
            : base(dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddRoleToUser(string name, string description, User user)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
                await _roleManager.CreateAsync(new Role(name, description));

            return await _userManager.AddToRoleAsync(user, name);
        }

        public async Task<IdentityResult> Register(User user, string password, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
                return await AddRoleToUser("User", "Role for User", user);
            else
                return result;
        }

        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            //user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
        public async Task<bool> IsExist(string phoneNumber, CancellationToken cancellationToken)
        {
            var studentExist = await _userManager.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
            if (studentExist)
            {
                return true;
            };
            return false;
        }

        public Task<int> GetUserIdByUserName(string username, CancellationToken cancellationToken)
        {
            return _userManager.Users.Where(x => x.UserName == username).Select(x => x.Id)
                  .FirstOrDefaultAsync(cancellationToken);
        }

        //#region Old Method By EbrahimiCourse
        //public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        //{
        //    var passwordHash = SecurityHelper.GetSha256Hash(password);
        //    return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        //}

        //public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
        //{
        //    //user.SecurityStamp = Guid.NewGuid();
        //    return UpdateAsync(user, cancellationToken);
        //}

        ////public override void Update(User entity, bool saveNow = true)
        ////{
        ////    entity.SecurityStamp = Guid.NewGuid();
        ////    base.Update(entity, saveNow);
        ////}

        //public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        //{
        //    user.LastLoginDate = DateTimeOffset.Now;
        //    return UpdateAsync(user, cancellationToken);
        //}

        //public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        //{
        //    var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
        //    if (exists)
        //        throw new BadRequestException("نام کاربری تکراری است");

        //    var passwordHash = SecurityHelper.GetSha256Hash(password);
        //    user.PasswordHash = passwordHash;
        //    await base.AddAsync(user, cancellationToken);
        //}

        //#endregion


    }
}
