﻿using System;
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
    public class BoardRepository //: //IBoardRepository
    {
        AppDbContext db;

        public BoardRepository(AppDbContext context)
        {
            db = context;
        }
        public IEnumerable<Board> GetAll(Guid userId)
        {
            return db.Boards.Where(x=>x.BoardUserId==userId);
        }
        public Board GetOne(int id)
        {
            return db.Boards.Find(id);
        }

        public IEnumerable<Board> FindBoard(Guid id,string search)
        {
            return db.Boards.Where(p => p.Title.Contains(search) && p.BoardUserId==id).ToList();
        }

        public async Task<Board> Create(Board board)
        {
           db.Boards.Add(board);
           await db.SaveChangesAsync();
           return board;
        }

        public Board Update(Board board)
        {
            Board updatedBoard = db.Boards.FirstOrDefault(x => x.Id == board.Id);
            updatedBoard.Title = board.Title;
            updatedBoard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedBoard;
        }

        public Board Delete(int id)
        {
            Board deletedBoard = db.Boards.FirstOrDefault(x => x.Id == id);
            deletedBoard.IsActive = false;
            deletedBoard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return deletedBoard;
        }

    }
}
