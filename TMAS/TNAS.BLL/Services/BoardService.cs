using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.DAL.DTO;
using AutoMapper;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.Interfaces;

namespace TMAS.BLL.Services
{
    public class BoardService : IBoardService
    {
      private readonly  IBoardRepository _boardRepository;
      private readonly IMapper _mapper;
        public BoardService(IBoardRepository repository,IMapper mapper)
        {
            _boardRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BoardViewDTO>> GetAll(Guid userId)
        {
            var boards = await _boardRepository.GetAll(userId);
            var mapperResult = _mapper.Map<IEnumerable<BoardViewDTO>>(boards);
            return mapperResult;
        }
        //get boards for Front
        public async Task<BoardViewDTO> GetOne(int boardId)
        {
            var board = await _boardRepository.GetOne(boardId);
            var mapperResult = _mapper.Map<BoardViewDTO>(board);
            return mapperResult;
        }

        //get boards for boardsAccess
        public async Task<Board> GetOneById(int boardId)
        {
            var board = await _boardRepository.GetOne(boardId);
            return board;
        }

        public async Task<IEnumerable<BoardViewDTO>> FindBoard(Guid userId,string search)
        {
            var boards = await _boardRepository.FindBoard(userId, search);
            var mapperResult = _mapper.Map<IEnumerable<BoardViewDTO>>(boards);
            return mapperResult;
        }

        public async Task<BoardViewDTO> Create(string title , Guid id)
        {
            Board createdBoard = new Board
            {
                Title = title,
                BoardUserId = id,
                CreatedDate = DateTime.Now,
                IsActive=true
            };
            var result = await _boardRepository.Create(createdBoard);
            var mapperResult = _mapper.Map<BoardViewDTO>(result);
            return mapperResult;
        }

        public async Task<BoardViewDTO> Update(Board board)
        {
            var result = await _boardRepository.Update(board);
            var mapperResult = _mapper.Map<BoardViewDTO>(result);
            return mapperResult;
        }

        public async Task<BoardViewDTO> Delete(int id)
        {
            var result = await _boardRepository.Delete(id);
            var mapperResult = _mapper.Map<BoardViewDTO>(result);
            return mapperResult;
        }
    }
}
