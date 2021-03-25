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
    public class HistoryService:IHistoryService
    {
        HistoryRepository history;
        public HistoryService(HistoryRepository repository)
        {
            history = repository;
        }
        public IEnumerable<History> GetAll(int userId)
        {
            return history.GetAll(userId);
        }
        public History GetOne(int userId)
        {
            return history.GetOne(userId);
        }

        public void Create(History board)
        {

        }

        public void Update(History board)
        {

        }

        public void Delete(int id)
        {

        }

    }
}
