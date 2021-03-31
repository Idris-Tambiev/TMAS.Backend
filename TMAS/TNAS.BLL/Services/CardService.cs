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
    public class CardService//:ICardService
    {
        private readonly CardRepository _cardRepository;
        public CardService(CardRepository repository)
        {
            _cardRepository = repository;
        }
        public IEnumerable<Card> GetAll(int columnId)
        {
            return _cardRepository.GetAll(columnId);
        }

        public Card GetOne(int id)
        {
            return _cardRepository.GetOne(id);
        }

        public async Task<Card> Create(Card createdCard)
        {
            var newColumn = new Card
            {
                Title=createdCard.Title,
                Text = createdCard.Text,
                ColumnId = createdCard.ColumnId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDone=false
            };
            return await _cardRepository.Create(newColumn);
        }

        public IEnumerable<Card> FindCard(Guid userId, string search)
        {
            return _cardRepository.FindCards(userId, search);
        }
        public async Task<Card> Update(Card updatedCard)
        {
            return await _cardRepository.Update(updatedCard);
        }

        public Card Delete(int id)
        {
            return _cardRepository.Delete(id);
        }

    }
}
