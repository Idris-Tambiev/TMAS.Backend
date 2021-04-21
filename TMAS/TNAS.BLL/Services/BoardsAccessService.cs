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
        public BoardsAccessService(BoardsAccessRepository boardsAccessRepository, IMapper mapper)
        {
            _boardsAccessRepository = boardsAccessRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<UserDTO>> GetUsers(int id,string text)
        {
            var allusers = await _boardsAccessRepository.GetUser(id, text);
            var mapperResult = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allusers);
            return mapperResult;
        }
        public  Task<Action> Delete(BoardsAccess access)
        {
            return _boardsAccessRepository.Delete(access);
        }
    }
}
