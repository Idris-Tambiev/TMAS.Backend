using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class ColumnService: IColumnService
    {
        ColumnRepository column;
        public ColumnService(ColumnRepository repository)
        {
            column = repository;
        }

        public IEnumerable<Column> GetAll(int userId)
        {
            return column.GetAll(userId);
        }
        public Column GetOne(int id)
        {
            return column.GetOne(id);
        }

        public void Create(Column createdColumn)
        {

        }

        public void Update(Column updatedColumn)
        {

        }

        public void Delete(int id)
        {

        }
    }
}
