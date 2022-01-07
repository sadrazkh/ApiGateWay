using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.User;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Data.Contracts.UserContracts
{
    public interface IUserRepository : IRepository<User>
    {
        //Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        // Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
        //Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
        Task<IdentityResult> Register(User user, string password, CancellationToken cancellationToken);
        Task<IdentityResult> AddRoleToUser(string name, string description, User user);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
        Task<bool> IsExist(string phoneNumber, CancellationToken cancellationToken);
        Task<int> GetUserIdByUserName(string username, CancellationToken cancellationToken);

    }
}
