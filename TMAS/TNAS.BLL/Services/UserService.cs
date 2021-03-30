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

namespace TMAS.BLL.Services
{
   public class UserService
    {
        private readonly UserRepository _user;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserRepository repository, UserManager<User> userManager,IMapper mapper)
        {
            _user = repository;
             _userManager = userManager;
            _mapper = mapper;
        }

        public User GetOne(User user)
        {
            return default;
        }

        public async Task<User> GetOneByEmail(User user)
        {
            
           var findedUser= await _userManager.FindByEmailAsync(user.Email);
           return findedUser;
        }

        public async Task<IdentityResult> Create(RegistrateUserDto createdUser)
        {
            var findedUser = await _userManager.FindByEmailAsync(createdUser.Email);
            if (findedUser == null)
            {
                var newUser = _mapper.Map<RegistrateUserDto, User>(createdUser);
                var result = await _userManager.CreateAsync(newUser, createdUser.Password);
                return result;
            }
            else return default;
        }

        public void Update(User updatedUser)
        {

        }

        public void Delete(int id)
        {

        }

    }
}
