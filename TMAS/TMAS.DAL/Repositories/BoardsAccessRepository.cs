﻿using Microsoft.EntityFrameworkCore;
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


            if (searchResult.Count==0)
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
            var accesses = await db.Boards
                .Where(x => x.BoardUserId != id)
                .Include(x => x.BoardsAccesses)
                .Where(a => a.BoardsAccesses.Any(y => y.UserId == id))
                .ToListAsync();

            return accesses;     
        }

        public async Task<IEnumerable<User>> GetAssignedUsers(int id,string text, Guid userId)
        {
            var board = await db.Boards
                .Where(x => x.BoardUserId == userId)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            if (board!=null)
            {
                var accesses = await db.Users
                .Where(x => x.UserName.Contains(text))
                .Include(x => x.BoardsAccesses)
                .Where(a => a.BoardsAccesses.Any(y => y.BoardId == id))
                .ToListAsync();
                return accesses;
            }else
            {
                return default;
            }
            
        }

        public async Task<Action> Delete(BoardsAccess access)
        {
            var accesses = db.BoardsAccesses
                .Where(x => x.BoardId == access.BoardId)
                .Where(x => x.UserId == access.UserId)
                .FirstOrDefault();

            db.BoardsAccesses.Remove(accesses);
            await db.SaveChangesAsync();
            return default;
        }
    }
}
