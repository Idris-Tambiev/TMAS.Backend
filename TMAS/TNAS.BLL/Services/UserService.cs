using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
   public class UserService
    {
        UserRepository user;
        public UserService(UserRepository repository)
        {
            user = repository;
        }

        public User Create(User createdUser)
        {
            return createdUser;
        }

        public void Update(User updatedUser)
        {

        }

        public void Delete(int id)
        {

        }

    }
}
