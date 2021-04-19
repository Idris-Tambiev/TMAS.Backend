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

namespace TMAS.BLL.Services
{
   public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<RegistrateUserDto> _userValidator;
        private readonly EmailService _emailService;
        //private readonly IMailService _mailService;
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


        public async Task<UserManagerResponse> ConfirmEmailAsync(string id,string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)  return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "User not found"
            }; ;

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user,normalToken);

            if (result.Succeeded)
            {
                return new UserManagerResponse { 
                    IsSuccess=true,
                    Message="Email confirmed successfully"
                };
            }
            else
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Email did not confirmed"
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
