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
    public class CardService:ICardService
    {
        private readonly CardRepository _cardRepository;
        private readonly IMapper _mapper;
        public CardService(CardRepository repository,IMapper mapper)
        {
            _cardRepository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CardViewDTO>> GetAll(int columnId)
        {
            var allCards = await _cardRepository.GetAll(columnId);
            var result = _mapper.Map<IEnumerable<Card>,IEnumerable<CardViewDTO>>(allCards);
            return result;
        }
        public async Task<Card> CheckCard(int cardId,Boolean status)
        {
            return await _cardRepository.CheckCard(cardId,status);
        }

        public async Task<Card> GetOne(int cardId)
        {
            return await _cardRepository.GetOne(cardId);
        }

        public async Task<Card> Create(Card card)
        {
            var newCard = new Card
            {
                Title=card.Title,
                Text = card.Text,
                ColumnId = card.ColumnId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDone=false,
                SortBy=card.SortBy
            };
            return await _cardRepository.Create(newCard);
        }

        public async Task<IEnumerable<Card>> FindCard(int boardId, string search)
        {
            return await _cardRepository.FindCards(boardId,search);
        }
        public async Task<Card> Update(Card updatedCard)
        {
            return await _cardRepository.Update(updatedCard);
        }
        public async Task<Card> Move(Card movedCard)
        {
            return await _cardRepository.Move(movedCard);
        }
        public async Task<Card> MoveOnColumn(Card movedCard)
        {
            return await _cardRepository.MoveOnColumn(movedCard);
        }

        public async Task<Card> Delete(int id)
        {
            return await _cardRepository.Delete(id);
        }

    }
}
