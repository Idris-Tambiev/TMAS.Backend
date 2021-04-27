using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IBoardAccessService:IBaseService
    {
        Task<BoardsAccess> Create(BoardsAccess access);
        Task<IEnumerable<BoardViewDTO>> Get(Guid id);
        Task<IEnumerable<UserDTO>> GetAllUsers(int boardId, string text, Guid userId);
        Task<IEnumerable<UserDTO>> GetAssignedUsers(int boardId, string text, Guid userId);
        Task<BoardsAccess> Delete(int boardId,Guid userId);
    }
}
