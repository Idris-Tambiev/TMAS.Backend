﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.BLL.Interfaces.BaseInterfaces
{
    public interface ICreateWithoutGuid<T>
    {
        Task<T> Create(T item);
    }
}
