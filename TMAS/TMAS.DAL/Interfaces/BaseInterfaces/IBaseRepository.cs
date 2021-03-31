using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DAL.Interfaces.BaseInterfaces
{
    public interface IBaseRepository<T>
    {
        //IEnumerable<T> GetAll(int id);
        Task<T> GetOne(int id);
        Task<T> Create(T item);
        Task<T> Update(T item);
        Task<T> Delete(int id);
    }
}
