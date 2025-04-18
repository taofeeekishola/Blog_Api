﻿using AutoMapper;
using BusinessLogicLayer.IServices;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;
using DomainLayer.UserDto;


namespace BusinessLogicLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        IMapper _imapper;

        public UserService(IUnitOfWork unitOfWork, IMapper imapper)
        {
            _unitOfWork = unitOfWork;
            _imapper = imapper;
        }

        //function to create a user
        public async Task<User?> CreateUser(CreateUserDto userDto)
        {
            // Validate input
            if (string.IsNullOrEmpty(userDto.FirstName) || // Use PascalCase: FirstName
                string.IsNullOrEmpty(userDto.LastName) ||  // Use PascalCase: LastName
                string.IsNullOrEmpty(userDto.Email) ||
                string.IsNullOrEmpty(userDto.Password))
            {
                return null;
            }

            // Map DTO to User entity
            var user = _imapper.Map<User>(userDto);
            user.UserName = userDto.Email; // Set UserName to Email (required by Identity)

            // Create the user
            var createdUser = await _unitOfWork.userRepository.Create(user, userDto.Password);

            return createdUser;
        }

        //function to get all users
        public List<User> GetAllUsers()
        {
            return _unitOfWork.userRepository.GetAll();
        }

        //function to get a single user
        public async Task<User?> GetUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return await _unitOfWork.userRepository.Get(id);
        }

        //function to update a user
        public async Task<User?> UpdateUser(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Id))
            {
                return null;
            }

            var existingUser = await _unitOfWork.userRepository.Get(user.Id);
            if (existingUser == null)
            {
                return null;
            }

            // Update only non-password fields
            existingUser.FirstName = user.FirstName;
            existingUser.FirstName = user.FirstName;
            existingUser.Email = user.Email;

            await _unitOfWork.userRepository.Update(existingUser);
            return existingUser;
        }

        //function to delete a user
        public async Task<bool> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            var user = await _unitOfWork.userRepository.Get(id);
            if (user == null)
            {
                return false;
            }

            await _unitOfWork.userRepository.Delete(user);

            return true;
        }

        //function to login a user
        public async Task<User?> AuthenticateUser(LoginUserDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return null;
            }

            return await _unitOfWork.userRepository.Authenticate(loginDto.Email, loginDto.Password);
        }
    }
}