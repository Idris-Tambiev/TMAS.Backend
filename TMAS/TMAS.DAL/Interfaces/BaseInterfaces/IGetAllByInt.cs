﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DAL.Interfaces.BaseInterfaces
{
    public interface IGetAllByInt<T>
    {
        Task<IEnumerable<T>> GetAll(int id);
    }
}
