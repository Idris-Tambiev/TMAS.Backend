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
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
