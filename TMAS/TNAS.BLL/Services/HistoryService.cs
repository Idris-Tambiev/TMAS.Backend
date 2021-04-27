using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using AutoMapper;
using TMAS.DAL.DTO;
using TMAS.DAL.Interfaces;
using TMAS.DB.Models.Enums;

namespace TMAS.BLL.Services
{
    public class HistoryService:IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;
        public HistoryService(IHistoryRepository repository,IMapper mapper)
        {
            _historyRepository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<HistoryViewDTO>> GetAll(int boardId)
        {
            var allHistories= await _historyRepository.GetAll(boardId);
            var mapperResult = _mapper.Map<IEnumerable<History>,IEnumerable<HistoryViewDTO>>(allHistories);
            return mapperResult;
        }
        public async Task<HistoryViewDTO> CreateHistoryObject(UserActions actionType, Guid userId,string actionObject, int? sourceAction,int? destinationAction,int boardId)
        {
            History newHistory = new History
            {
                ActionType = (DB.Models.Enums.UserActions)actionType,
                AuthorId = userId,
                CreatedDate = DateTime.Now,
                ActionObject = actionObject,
                SourceAction = sourceAction,
                DestinationAction = destinationAction,
                BoardId = boardId
            };

            var history=await Create(newHistory);
            return history;
        }

        public async Task<HistoryViewDTO> Create(History history)
        {
            var createResult= await _historyRepository.Create(history);
            var mapperResult = _mapper.Map<History, HistoryViewDTO>(createResult);
            return mapperResult;
        }

    }
}
