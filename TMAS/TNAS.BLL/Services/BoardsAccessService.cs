using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Repositories;
using TMAS.DAL.DTO;
using TMAS.DB.Models;
using TMAS.DB.Models.Enums;

namespace TMAS.BLL.Services
{
    public class BoardsAccessService:IBoardAccessService
    {
        private readonly IBoardAccessRepository _boardsAccessRepository;
        private readonly IMapper _mapper;
        private readonly IBoardService _boardService;
        private readonly IUserService _userService;
        private readonly IHistoryService _historyService;
        public BoardsAccessService(IBoardAccessRepository boardsAccessRepository, IMapper mapper, IBoardService boardService,IUserService userService,IHistoryService historyService)
        {
            _boardsAccessRepository = boardsAccessRepository;
            _mapper = mapper;
            _boardService = boardService;
            _userService = userService;
            _historyService = historyService;
        }

        public async Task<BoardsAccess> Create(BoardsAccess access)
        {
            var result = await _boardsAccessRepository.Create(access);
            var user = await _userService.GetOneById(access.UserId);
            var history = await _historyService.CreateHistoryObject(
                UserActions.AssignUser,
                access.UserId,
                user.Name+' '+user.LastName,
                null,
                null,
                access.BoardId
                );

            return result;
        }
        public async Task<IEnumerable<BoardViewDTO>> Get(Guid id)
        {

            var allBoards = await _boardsAccessRepository.Get(id);
            var mapperResult = _mapper.Map<IEnumerable<BoardViewDTO>>(allBoards);
            return mapperResult;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers(int boardId, string text, Guid userId)
        {
            var boards = await _boardService.GetOneById(boardId);
            Guid creatorId = boards.BoardUserId;
            IEnumerable<UserDTO> users = await _userService.GetUsers(text, userId, creatorId);
            return users;
        }

        public async Task<IEnumerable<UserDTO>> GetAssignedUsers(int boardId,string text,Guid userId)
       {
            var allusers = await _boardsAccessRepository.GetAssignedUsers(boardId, text,userId);
            var mapperResult = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allusers);
            return mapperResult;
        }

        public async Task<BoardsAccess> Delete(int boardId,Guid userId)
        {
            var user = await _userService.GetOneById(userId);
            
            var result = await _boardsAccessRepository.Delete( boardId,  userId);

            var history = await _historyService.CreateHistoryObject(
                UserActions.AssignUser,
                userId,
                user.Name + ' ' + user.LastName,
                null,
                null,
                boardId
                );

            return result;
        }
    }
}
