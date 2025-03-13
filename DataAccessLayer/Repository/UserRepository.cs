using DataAccessLayer.IRepository;
using DomainLayer.Model;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IUser
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //funciton to create a user
        public async Task<User> Create(User user, string password)
        {
            // Create the user with the provided password
            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return null; // Return null if user creation fails
            }

            return user; // Return the created user
        }


        //function to delete a user
        public  async Task<bool> Delete(User user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }


        //function to get a user
        public async Task<User?> Get(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        //funciton to get all users
        public List<User> GetAll()
        {
            return _userManager.Users.ToList();
        }

        //function to update a user
        public async Task<User?> Update(User user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return null;
            }

            return user;
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            return isPasswordValid ? user : null;
        }
    }
   
}
