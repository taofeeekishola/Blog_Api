using DataAccessLayer.IRepository;
using DomainLayer.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    class UserRepository : IUser
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Create(User user)
        {
            IdentityResult result =  await _userManager.CreateAsync(user, user.PasswordHash);

            if (!result.Succeeded)
            {
                return null;
            }

            return user;
        }

        public  async Task<bool> Delete(User user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<User?> Get(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public List<User> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task<User?> Update(User user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return null;
            }

            return user;
        }
    }
   
}
