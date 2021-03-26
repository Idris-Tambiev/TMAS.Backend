using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using Microsoft.AspNetCore.Identity;

namespace TMAS.BLL.Services
{
   public class UserService
    {
        private readonly UserRepository _user;
        private readonly UserManager<User> _userManager;
        public UserService(UserRepository repository, UserManager<User> userManager)
        {
            _user = repository;
             _userManager = userManager;
        }

        public User GetOne(User user)
        {
            return default;
        }

        public async Task<User> GetOneByEmail(User user)
        {
            
           var a= await _userManager.FindByEmailAsync(user.Email);
           return a;
        }

        public async Task<IdentityResult> Create(User createdUser)
        {

            var result = await _userManager.CreateAsync(createdUser,createdUser.Password);
            return result;
        }

        public void Update(User updatedUser)
        {

        }

        public void Delete(int id)
        {

        }

    }
}
