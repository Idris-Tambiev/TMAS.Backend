using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
namespace TMAS.BLL.Services
{
    public class BoardService:IBoardService
    {
        BoardRepository board;
        public BoardService(BoardRepository repository)
        {
            board = repository;
        }

        public IEnumerable<Board> GetAll(int userId)
        {
            return board.GetAll(userId);
        }
        public Board GetOne(int id)
        {
            return board.GetOne(id);
        }

        public Board FindBoard(string name)
        {
            return board.FindBoard(name);
        }

        public void Create(Board board)
        {
           
        }

        public void Update(Board board)
        {
            
        }

        public void Delete(int id)
        {
            
        }

    }
}
