using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TMAS.DAL.Repositories
{
    public class HistoryRepository:IHistoryRepository
    {
        private AppDbContext db;

        public HistoryRepository(AppDbContext context)
        {
            db = context;
        }
        public IEnumerable<History> GetAll(int userId)
        {
            return db.Histories;
        }
        public History GetOne(int id)
        {
            return db.Histories.Find(id);
        }

        public void Create(History history)
        {
            db.Histories.Add(history);
        }

        public void Update(History history)
        {
             db.Entry(history).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            History history = db.Histories.Find(id);
            if (history != null)
                db.Histories.Remove(history);
        }
    }
}
