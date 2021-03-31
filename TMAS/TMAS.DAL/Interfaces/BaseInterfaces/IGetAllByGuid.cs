using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DAL.Interfaces.BaseInterfaces
{
    public interface IGetAllByGuid<T>
    {
        Task<IEnumerable<T>> GetAll(Guid id);
    }
}
