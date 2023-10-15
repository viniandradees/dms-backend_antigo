using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace DataManagementService.Application.Interfaces.UserInterfaces
{
    public interface IAccountService
    {
        Task<bool> UserExists(string name);
        Task<UserUpdateDto> GetUserByUserNameAsync(string name);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}