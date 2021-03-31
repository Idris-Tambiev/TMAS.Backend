using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.BLL.Interfaces.BaseInterfaces
{
    public interface ICreateWithGuid<T>
    {
        Task<T> Create(T item,Guid id);
    }
}
