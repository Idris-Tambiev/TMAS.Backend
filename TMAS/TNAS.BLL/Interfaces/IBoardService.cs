using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IBoardService:IBaseService<Board>
    {
        Task<Board> Create(string title, Guid id);
        IEnumerable<Board> FindBoard(Guid userId, string search);
    }
}
