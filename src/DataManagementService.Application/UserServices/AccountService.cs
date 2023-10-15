using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces.UserInterfaces;
using DataManagementService.Domain.Identity;
using DataManagementService.Persistence.Interfaces.Idenity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Application.UserServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersistence _userPersistence;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUserPersistence userPersistence)
        {
            _userPersistence = userPersistence;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == userUpdateDto.UserName.ToLower());
                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-A01", 500, $"Unexpected error checking password: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded) {
                    var newUser = _mapper.Map<UserUpdateDto>(user);
                    return newUser;
                }

                return null;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-A02", 500, $"Unexpected error creating account: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string name)
        {
            try
            {
                var user = await _userPersistence.GetByUserNameAsync(name);
                
                if (user is null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-A03", 500, $"Unexpected error getting user by name: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var checkUserName = await _userPersistence.GetByUserNameAsync(userUpdateDto.UserName);
                if (checkUserName is null) return null;
                
                var user = _mapper.Map<User>(userUpdateDto);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersistence.Update<User>(user);

                if (await _userPersistence.SaveChangesAsync()) {
                    var updatedUser = await _userPersistence.GetByUserNameAsync(user.UserName);
                    var updatedUserMap = _mapper.Map<UserUpdateDto>(user);
                    return updatedUserMap;
                }

                return null;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-A04", 500, $"Unexpected error updating user account data: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string name)
        {
            try
            {
                return await _userManager.Users.AnyAsync(u => u.UserName.ToLower() == name.ToLower());
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-A05", 500, $"Unexpected error checking if user exists: {ex.Message}");
            }
        }
    }
}