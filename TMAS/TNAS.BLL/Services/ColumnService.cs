﻿using System;
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
using TMAS.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace TMAS.BLL.Services
{
    public class ColumnService: IColumnService
    {
        private readonly ColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext db;
        public ColumnService(ColumnRepository repository,IMapper mapper,AppDbContext context)
        {
            _columnRepository = repository;
            _mapper = mapper;
            db = context;
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

        public async Task<Column> Move(Column movedColumn)
        {
            Column updatedColumn = await db.Columns.FirstOrDefaultAsync(x => x.Id == movedColumn.Id);

            SwitchColumns(updatedColumn.SortBy, movedColumn);

            updatedColumn.SortBy = movedColumn.SortBy;
            updatedColumn.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return movedColumn;
        }

        public void SwitchColumns(int prevPosition, Column column)
        {
            int currentPosition = column.SortBy;
            if (currentPosition < prevPosition)
            {
                var result = db.Columns
                .Where(x => x.BoardId == column.BoardId)
                .OrderBy(x => x.SortBy)
                .Skip(currentPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].SortBy < prevPosition)
                    {
                        result[i].SortBy++;
                    }
                }
            }
            else
            {
                var result = db.Columns
                .Where(x => x.BoardId == column.BoardId)
                .OrderBy(x => x.SortBy)
                .Skip(prevPosition + 1)
                .Take(currentPosition - prevPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].SortBy--;
                }
            }
            db.SaveChanges();
        }
    }
}
