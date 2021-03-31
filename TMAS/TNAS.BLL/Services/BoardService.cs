using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.DB.DTO;
using AutoMapper;
using TMAS.BLL.Interfaces.BaseInterfaces;

namespace TMAS.BLL.Services
{
    public class BoardService : IBoardService
    {
      private readonly  BoardRepository _boardRepository;
        public BoardService(BoardRepository repository)
        {
            _boardRepository = repository;
        }

        public IEnumerable<Board> GetAll(Guid userId)
        {
            return _boardRepository.GetAll(userId);
        }
        public Board GetOne(int id)
        {
            return _boardRepository.GetOne(id);
        }

        public IEnumerable<Board> FindBoard(Guid userId,string search)
        {
            return _boardRepository.FindBoard(userId,search);
        }

        public async Task<Board> Create(Board board, Guid id)
        {
            Board createdBoard = new Board
            {
                Title = board.Title,
                BoardUserId = id,
                CreatedDate = DateTime.Now,
                IsActive=board.IsActive
            };

            return await _boardRepository.Create(createdBoard);
        }

        public async Task<Board> Update(Board board)
        {
            return await _boardRepository.Update(board);
        }

        public Board Delete(int id)
        {
            return _boardRepository.Delete(id);
        }



    }
}
