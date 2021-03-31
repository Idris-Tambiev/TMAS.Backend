using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.BLL.Interfaces.BaseInterfaces;

namespace TMAS.BLL.Services
{
    public class ColumnService: IColumnService
    {
        private readonly ColumnRepository _columnRepository;
        public ColumnService(ColumnRepository repository)
        {
            _columnRepository = repository;
        }

        public async Task<IEnumerable<Column>> GetAll(int boardId)
        {
            return await _columnRepository.GetAll(boardId);
        }
        public async Task<Column> GetOne(int id)
        {
            return await _columnRepository.GetOne(id);
        }

        public async Task<Column> Create(Column createdColumn)
        {
            var newColumn = new Column
            {
                Title = createdColumn.Title,
                BoardId = createdColumn.BoardId,
                CreatedDate =DateTime.Now,
                IsActive=true
        };
            return await _columnRepository.Create(newColumn);
        }

        public async Task<Column> Update(Column updatedColumn)
        {
            return await _columnRepository.Update(updatedColumn);
        }


        public async Task<Column> Delete(int id)
        {
            return await _columnRepository.Delete(id);
        }
    }
}
