﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Repositories
{
    public class ColumnRepository:IColumnRepository
    {
        private AppDbContext db;

        public ColumnRepository(AppDbContext context)
        {
            db =context;
        }

        public IEnumerable<Column> GetAll(int boardId)
        {
            return db.Columns.Where(x => x.BoardId == boardId);
        }
        public Column GetOne(int id)
        {
            return db.Columns.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Column> Create(Column column)
        {
            db.Columns.Add(column);
            await db.SaveChangesAsync();
            return column;
        }

        public Column Update(Column column)
        {
            Column updatedColumn = db.Columns.FirstOrDefault(x => x.Id == column.Id);
            updatedColumn.Title = column.Title;
            updatedColumn.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedColumn;
        }

        public Column Delete(int id)
        {
            Column deletedColumn = db.Columns.FirstOrDefault(x => x.Id == id);
            if (deletedColumn != null)
            {
                deletedColumn.IsActive = false;
                deletedColumn.UpdatedDate = DateTime.Now;
                db.SaveChanges();
                return deletedColumn;
            }
            else
            {
                return default;
            }
            
            
        }

    }
}
