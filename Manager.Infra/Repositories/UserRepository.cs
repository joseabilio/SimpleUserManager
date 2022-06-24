using Manager.Infra.Interfaces;
using Manager.Infra.Context;
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Manager.Infra.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        private readonly ManagerContext managerContext;
        public UserRepository(ManagerContext context):base(context)
        {
            managerContext = context; 
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await managerContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).ToListAsync();
            
            return user.FirstOrDefault();
        }
        public async Task<List<User>> SearchByEmail(string email)
        {
            var allUsers = await managerContext.Users.Where(x => x.Email.ToLower().Contains(email.ToLower()))
                                                     .ToListAsync();
            return allUsers;

        }
        public async Task<List<User>> SearchByName(string name)
        {
             var allUsers = await managerContext.Users.Where(x => x.Name.ToLower().Contains(name.ToLower()))
                                               .AsNoTracking()
                                               .ToListAsync();
            return allUsers;  
        }
    }
}