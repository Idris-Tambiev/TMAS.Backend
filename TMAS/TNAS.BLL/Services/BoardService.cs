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
        private readonly IMapper _mapper;
        public BoardService(BoardRepository repository/*,IMapper mapper*/)
        {
            _boardRepository = repository;
            _mapper = null /*mapper*/;
        }

        public IEnumerable<Board> GetAll(int userId)
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

        public async Task<CreatedBoardDto> Create(NewBoardDto board)
        {
            //var array = _mapper.Map<Question, QuestionDto>(question);
            var createdBoard = _mapper.Map<NewBoardDto, Board>(board);

            return await Task.FromResult(default(CreatedBoardDto));
            //return await _boardRepository.Create(createdBoard);
        }

        public void Update(Board board)
        {
            
        }

        public void Delete(int id)
        {
            
        }

        public void Create(Board item)
        {
            throw new NotImplementedException();
        }
    }
}
