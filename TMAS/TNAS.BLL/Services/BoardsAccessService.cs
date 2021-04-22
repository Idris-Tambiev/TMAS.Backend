using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.DB.DTO;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class BoardsAccessService
    {
        private readonly BoardsAccessRepository _boardsAccessRepository;
        private readonly IMapper _mapper;
        private readonly BoardService _boardService;
        private readonly UserService _userService;
        public BoardsAccessService(BoardsAccessRepository boardsAccessRepository, IMapper mapper, BoardService boardService,UserService userService)
        {
            _boardsAccessRepository = boardsAccessRepository;
            _mapper = mapper;
            _boardService = boardService;
            _userService = userService;
        }

        public async Task<BoardsAccess> Create(BoardsAccess access)
        {
            return await _boardsAccessRepository.Create(access);
        }
        public async Task<IEnumerable<BoardViewDTO>> Get(Guid id)
        {

            var allBoards = await _boardsAccessRepository.Get(id);
            var mapperResult = _mapper.Map<IEnumerable<Board>, IEnumerable<BoardViewDTO>>(allBoards);
            return mapperResult;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers(int boardId, string text, Guid userId)
        {
            Board boards = await _boardService.GetOneById(boardId);
            Guid creatorId = boards.BoardUserId;
            var users = await _userService.GetUsers(text, userId, creatorId);
            return users;
        }

        public async Task<IEnumerable<UserDTO>> GetAssignedUsers(int boardId,string text,Guid userId)
       {
            var allusers = await _boardsAccessRepository.GetAssignedUsers(boardId, text,userId);
            var mapperResult = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allusers);
            return mapperResult;
        }

        public  Task<Action> Delete(BoardsAccess access)
        {
            return _boardsAccessRepository.Delete(access);
        }
    }
}
