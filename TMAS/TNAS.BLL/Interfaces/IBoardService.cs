using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IBoardService:IBaseService
    {
        Task<IEnumerable<Board>> GetAll(Guid userId);
        Task<Board> GetOne(int id);
        Task<Board> Create(string title, Guid id);
        Task<Board> Update(Board board);
        Task<IEnumerable<Board>> FindBoard(Guid userId, string search);
        Task<Board> Delete(int id);
    }
}
