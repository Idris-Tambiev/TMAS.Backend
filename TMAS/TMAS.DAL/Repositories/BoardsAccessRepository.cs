using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.DAL.Repositories
{
    public class BoardsAccessRepository
    {
        private readonly AppDbContext db;
 
        public BoardsAccessRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task<BoardsAccess> Create(BoardsAccess access)
        {
            var searchResult = await db.BoardsAccesses
                .Where(x => x.BoardId == access.BoardId)
                .Where(x => x.UserId == access.UserId)
                .ToListAsync();
            if (searchResult==null)
            {
                await db.BoardsAccesses.AddAsync(access);
                await db.SaveChangesAsync();
                return access;
            }
            else
            {
                return default;
            }
        }

        public async Task<IEnumerable<Board>> Get(Guid id)
        {
            var acceses = await db.Boards
               .Include(x=>x.BoardsAccesses
                    .Where(x => x.UserId == id))
               .Where(x=>x.BoardUserId!=id)
               .ToListAsync();


            return acceses;     
        }
    }
}
