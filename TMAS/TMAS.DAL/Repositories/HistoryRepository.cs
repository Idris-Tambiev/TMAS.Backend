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
        public async Task<IEnumerable<History>> GetAll(int boardId)
        {
            return await db.Histories.Where(x => x.BoardId == boardId).ToListAsync();
        }

        public async Task<History> GetOne(int id)
        {
            return await db.Histories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<History> Create(History history)
        {
            db.Histories.Add(history);
            db.SaveChanges();
            return history;
        }

    }
}
