using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Interfaces
{
    public interface IHistoryRepository:IBaseRepository<History>,IGetAllByGuid<History>
    {

    }
}
