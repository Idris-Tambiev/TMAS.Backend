using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using AutoMapper;
using TMAS.DB.DTO;

namespace TMAS.BLL.Services
{
    public class HistoryService:IHistoryService
    {
        private readonly HistoryRepository _historyRepository;
        private readonly IMapper _mapper;
        public HistoryService(HistoryRepository repository,IMapper mapper)
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

        public async Task<History> Create(History history,Guid userId)
        {
            History newHistory = new History
            {
                ActionType = history.ActionType,
                AuthorId = userId,
                CreatedDate = DateTime.Now,
                ActionObject = history.ActionObject,
                SourceAction= history.SourceAction,
                DestinationAction=history.DestinationAction,
                BoardId=history.BoardId

            };
            return  await _historyRepository.Create(newHistory);
        }

    }
}
