﻿using DomainLayer.Model;
using DomainLayer.UserDto;


namespace BusinessLogicLayer.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns>The created User object or null if creation failed</returns>
        Task<User?> CreateUser(CreateUserDto userDto);

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>List of all Users</returns>
        List<User> GetAllUsers();

        /// <summary>
        /// Get a single User by ID
        /// </summary>
        /// <param name="id">The user ID</param>
        /// <returns>User object or null if not found</returns>
        Task<User?> GetUser(string id);

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user">The user with updated information</param>
        /// <param name="message">Output message about the operation result</param>
        /// <returns>The updated User object or null if update failed</returns>
        Task<User?> UpdateUser(User user);

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id">The user ID to delete</param>
        /// <returns>Boolean true if deleted successfully, otherwise false</returns>
        Task<bool> DeleteUser(string id);

        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <param name="loginDto">The login credentials provided by the user.</param>
        /// <returns>A task that resolves to the authenticated user if successful, otherwise null.</returns>
        Task<User?> AuthenticateUser(LoginUserDto loginDto);

    }
}