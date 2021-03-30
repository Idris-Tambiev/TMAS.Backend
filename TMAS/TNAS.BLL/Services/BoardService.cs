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

        public Board FindBoard(string name)
        {
            return _boardRepository.FindBoard(name);
        }

        public async Task<Board> Create(string title, Guid id)
        {
            Board createdBoard = new Board
            {
                Title = title,
                BoardUserId = id,
                CreatedDate = DateTime.Now,
                IsActive=true
            };
            return await _boardRepository.Create(createdBoard);
        }

        public Board Update(Board board)
        {
            return _boardRepository.Update(board);
        }

        public Board Delete(int id)
        {
            return _boardRepository.Delete(id);
        }

        public void Create(Board item)
        {
            throw new NotImplementedException();
        }
    }
}
