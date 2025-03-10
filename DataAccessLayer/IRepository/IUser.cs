using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IUser
    {
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Object of User</returns>
        User Create(User user);

        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns>List of Users</returns>
        List<User> GetAll();

        /// <summary>
        /// Update User
        /// </summary>
        /// <returns></returns>
        User? Update();

        /// <summary>
        /// Get a User
        /// </summary>
        /// <returns>Object of User</returns>
        User? Get();

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="user"></param>
        void Delete(User user);
       

    }
}
