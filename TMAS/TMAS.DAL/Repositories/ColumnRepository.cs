using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;

namespace TMAS.DAL.Repositories
{
    public class ColumnRepository:IColumnRepository
    {
        private AppDbContext db;

        public ColumnRepository(AppDbContext context)
        {
            db =context;
        }
        public IEnumerable<Column> GetAll(int userId)
        {
            return db.Columns;
        }
        public Column GetOne(int id)
        {
            return db.Columns.Find(id);
        }


        public void Create(Column column)
        {
            db.Columns.Add(column);
        }

        public void Update(Column column)
        {
            // db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Column column = db.Columns.Find(id);
            if (column != null)
                db.Columns.Remove(column);
        }
    }
}
