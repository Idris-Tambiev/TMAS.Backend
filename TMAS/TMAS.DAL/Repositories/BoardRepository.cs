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
    public class BoardRepository : IBoardRepository
    {
        AppDbContext db;

        public BoardRepository(AppDbContext context)
        {
            db = context;
        }
        public IEnumerable<Board> GetAll(int userId)
        {
            
            return db.Boards;
        }
        public Board GetOne(int id)
        {
            return db.Boards.Find(id);
        }

        public Board FindBoard(string name)
        {
            return db.Boards.Find(name);
        }

        public void Create(Board board)
        {
            db.Boards.Add(board);
        }

        public void Update(Board board)
        {
           // db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Board board = db.Boards.Find(id);
            if (board != null)
                db.Boards.Remove(board);
        }

    }
}
