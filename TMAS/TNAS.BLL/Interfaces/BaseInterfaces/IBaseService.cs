using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.BLL.Interfaces.BaseInterfaces
{
    public interface IBaseService<T>
    {
        Task<T> GetOne(int id);
        Task<T> Update(T item);
        Task<T> Delete(int id);
    }
}
