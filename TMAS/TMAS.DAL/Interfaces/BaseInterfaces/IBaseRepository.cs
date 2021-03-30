using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DAL.Interfaces.BaseInterfaces
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll(int id);
        T GetOne(int id);
        T Create(T item);
        T Update(T item);
        T Delete(int id);
    }
}
