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
    public class HistoryService//:IHistoryService
    {
        private readonly HistoryRepository _historyRepository;
        public HistoryService(HistoryRepository repository)
        {
            _historyRepository = repository;
        }
        public IEnumerable<History> GetAll(Guid userId)
        {
            return _historyRepository.GetAll(userId);
        }
        public History GetOne(int id)
        {
            //return _cardRepository.GetOne(id);
            return default;
        }

        public async Task<History> Create(int actionId,string actionObject,Guid userId)
        {
            var newHistory = new History
            {
                ActionType = (DB.Models.Enums.Actions)actionId,
                AuthorId = userId,
                CreatedDate = DateTime.Now,
                ActionObject = actionObject

            };
            return  _historyRepository.Create(newHistory);
        }

        public void Update(History board)
        {

        }

        public void Delete(int id)
        {

        }

    }
}
