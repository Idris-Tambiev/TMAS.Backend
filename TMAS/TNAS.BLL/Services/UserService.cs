using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using Microsoft.AspNetCore.Identity;
using TMAS.DB.DTO;
using AutoMapper;
using FluentValidation;
using TMAS.BLL.Validator;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace TMAS.BLL.Services
{
   public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<RegistrateUserDto> _userValidator;
        private readonly EmailService _emailService;
        public UserService(UserRepository repository, UserManager<User> userManager,IMapper mapper,AbstractValidator<RegistrateUserDto> validator,EmailService emailService)

        {
            _userRepository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _userValidator = validator;
            _emailService = emailService;
        }
        public async Task<User> GetOneByEmail(User user)
        {
           var findedUser= await _userManager.FindByEmailAsync(user.Email);
           return findedUser;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(string searchText,Guid currentUserId,Guid creatorUserId)
        {
            var findedUsers = await _userManager.Users
                .Where(x=>x.UserName.Contains(searchText))
                .Where(x=>x.Id!= currentUserId)
                .Where(x => x.Id != creatorUserId)
                .ToListAsync();
            var mapperResult = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(findedUsers);
            return mapperResult;
        }

        public async Task<IdentityResult> Create(RegistrateUserDto createdUser)
        {
            var validationResult = _userValidator.Validate(createdUser);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                var findedUser = await _userManager.FindByEmailAsync(createdUser.Email);
                if (findedUser == null)
                {
                    var newUser = _mapper.Map<RegistrateUserDto, User>(createdUser);
                    var result = await _userManager.CreateAsync(newUser, createdUser.Password);
                    var a = _emailService.CreateEmailAsync(newUser);
                    return result;
                }
                else return default;
            }
        }
        public async Task<UserDTO> GetOneById(Guid id)
        {
            User findedUser = await _userManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<User,UserDTO>(findedUser);
            return result;
        }


        public async Task<Response> ConfirmEmailAsync(string id,string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)  return new Response
            {
                IsSuccess = false,
                Message = "User not found"
            }; ;

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user,normalToken);

            if (result.Succeeded)
            {
                return new Response { 
                    IsSuccess=true,
                    Message="Email confirmed successfully"
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Email did not confirmed"
                };
            }
        }

        public async Task<Response> ResetUserPassword(string userId, string token,string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new Response
            {
                IsSuccess = false,
                Message = "User not found"
            };

            var checkPassword = await _userManager.CheckPasswordAsync(user, newPassword);
            if (checkPassword)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "You entered old password"
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ResetPasswordAsync(user, normalToken, newPassword);

            if (result.Succeeded)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Password reseted successfully"
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Password did not reseted"
                };
            }
        }

        public async Task<User> AddPhoto(Guid userId,string photo)
        {
            var findedUser = await _userManager.FindByIdAsync(userId.ToString());
            findedUser.Photo = photo;
            await _userManager.UpdateAsync(findedUser);

            return findedUser;
        }
    }
}
