﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IColumnService:IBaseService
    {
        Task<IEnumerable<Column>> GetAll(int boardId);
        Task<Column> GetOne(int id);
        Task<Column> Create(string title, int boardId);
        Task<Column> Update(Column updatedColumn);
        Task<Column> Delete(int id);
    }
}
