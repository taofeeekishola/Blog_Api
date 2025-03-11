using BusinessLogicLayer.IServices;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> CreateUser(User user)
        {
            if (string.IsNullOrEmpty(user.firstName) || string.IsNullOrEmpty(user.lastName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return null;
            }

            var createdUser = await _unitOfWork.userRepository.Create(user);
            
            return createdUser;
        }

        public List<User> GetAllUsers()
        {
            return _unitOfWork.userRepository.GetAll();
        }

        public async Task<User?> GetUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return await _unitOfWork.userRepository.Get(id);
        }

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
            existingUser.firstName = user.firstName;
            existingUser.lastName = user.lastName;
            existingUser.Email = user.Email;

            await _unitOfWork.userRepository.Update(existingUser);
            return existingUser;
        }

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
    }
}