using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Interfaces
{
    public interface IBoardRepository:IBaseRepository<Board>,IGetAllByGuid<Board>
    {
        Task<IEnumerable<Board>> FindBoard(Guid id,string name);
    }

}
