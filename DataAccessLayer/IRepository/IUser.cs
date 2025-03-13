using DomainLayer.Model;


namespace DataAccessLayer.IRepository
{
    public interface IUser
    {
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Object of User</returns>
        Task<User> Create(User user, string password);

        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns>List of Users</returns>
        List<User> GetAll();

        /// <summary>
        /// Update User
        /// </summary>
        /// <returns>Ojbect of User</returns>
        Task<User?> Update(User user);

        /// <summary>
        /// Get a User
        /// </summary>
        /// <returns>Object of User</returns>
        Task<User?> Get(string id);

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="user"></param>
        Task<bool> Delete(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User?> Authenticate(string email, string password);


    }
}
