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
    public class CardService:ICardService
    {
        private readonly CardRepository _cardRepository;
        public CardService(CardRepository repository)
        {
            _cardRepository = repository;
        }
        public async Task<IEnumerable<Card>> GetAll(int columnId)
        {
            return await _cardRepository.GetAll(columnId);
        }

        public async Task<Card> GetOne(int cardId)
        {
            return await _cardRepository.GetOne(cardId);
        }

        public async Task<Card> Create(string title,string text,int columnId)
        {
            var newColumn = new Card
            {
                Title=title,
                Text = text,
                ColumnId = columnId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDone=false
            };
            return await _cardRepository.Create(newColumn);
        }

        public async Task<IEnumerable<Card>> FindCard(int boardId, string search)
        {
            return await _cardRepository.FindCards(boardId,search);
        }
        public async Task<Card> Update(Card updatedCard)
        {
            return await _cardRepository.Update(updatedCard);
        }

        public async Task<Card> Delete(int id)
        {
            return await _cardRepository.Delete(id);
        }

    }
}
