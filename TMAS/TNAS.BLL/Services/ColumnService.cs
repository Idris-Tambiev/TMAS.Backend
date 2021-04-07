using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.BLL.Interfaces.BaseInterfaces;
using AutoMapper;
using TMAS.DB.DTO;

namespace TMAS.BLL.Services
{
    public class ColumnService: IColumnService
    {
        private readonly ColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        public ColumnService(ColumnRepository repository,IMapper mapper)
        {
            _columnRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ColumnViewDTO>> GetAll(int boardId)
        {
            var columns= _columnRepository.GetAll(boardId);
            var mapperResult = _mapper.Map<IEnumerable<Column>,IEnumerable<ColumnViewDTO>>(columns);
            return mapperResult;
        }
        public async Task<Column> GetOne(int columnId)
        {
            return await _columnRepository.GetOne(columnId);
        }

        public async Task<Column> Create(string title, int boardId)
        {
            var newColumn = new Column
            {
                Title = title,
                BoardId = boardId,
                CreatedDate =DateTime.Now,
                IsActive=true
        };
            return await _columnRepository.Create(newColumn);
        }

        public async Task<Column> Update(Column updatedColumn)
        {
            return _columnRepository.Update(updatedColumn);
        }


        public async Task<Column> Delete(int id)
        {
            return _columnRepository.Delete(id);
        }
    }
}
