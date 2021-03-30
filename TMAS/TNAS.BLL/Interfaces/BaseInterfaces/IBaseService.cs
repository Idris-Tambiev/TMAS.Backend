using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.BLL.Interfaces.BaseInterfaces
{
    public interface IBaseService<T>
    {
        IEnumerable<T> GetAll(Guid id);
        T GetOne(int id);
        //T Create();
        T Update(T item);
        T Delete(int id);
    }
}
