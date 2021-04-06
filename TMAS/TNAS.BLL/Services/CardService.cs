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
using TMAS.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace TMAS.BLL.Services
{
    public class CardService:ICardService
    {
        private readonly CardRepository _cardRepository;
        private readonly IMapper _mapper;
        private AppDbContext db;
        public CardService(CardRepository repository,IMapper mapper,AppDbContext context)
        {
            _cardRepository = repository;
            _mapper = mapper;
            db = context;
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
            Card updatedCard = await db.Cards.FirstOrDefaultAsync(x => x.Id == movedCard.Id);
            int prev = updatedCard.SortBy;
            SwitchCardsOnColumn(movedCard.SortBy, prev, movedCard);
            return updatedCard;
        }
        public async Task<Card> MoveOnColumn(Card movedCard)
        {
            Card updatedCard = db.Cards.FirstOrDefault(x => x.Id == movedCard.Id);
            MoveCards(movedCard, updatedCard.ColumnId);
            updatedCard.ColumnId = movedCard.ColumnId;
            updatedCard.SortBy = movedCard.SortBy;
            updatedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedCard;
     
        }

        public async Task<Card> Delete(int id)
        {
            return await _cardRepository.Delete(id);
        }

        private void MoveCards(Card card, int prevPosition)
        {
            var result = db.Cards.Where(x => x.ColumnId == card.ColumnId).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].SortBy >= card.SortBy)
                {
                    result[i].SortBy++;
                }

            }
            db.SaveChanges();
            var previousCards = db.Cards.Where(x => x.ColumnId == prevPosition).ToList();
            for (int i = 0; i < previousCards.Count; i++)
            {
                if (previousCards[i].SortBy >= card.SortBy)
                    previousCards[i].SortBy--;
            }
            db.SaveChanges();
        }
        private void SwitchCardsOnColumn(int curPosition, int prevPosition, Card card)
        {
            if (curPosition > prevPosition)
            {
                var result = db.Cards.Where(x => x.ColumnId == card.ColumnId).OrderBy(x => x.SortBy).Skip(prevPosition).Take(curPosition - prevPosition + 1).ToList();
                for (int i = 1; i < result.Count; i++)
                {
                    result[i].SortBy--;
                }
                result[0].SortBy = curPosition;
                result[0].UpdatedDate = DateTime.Now;
            }
            else
            {
                var result = db.Cards.Where(x => x.ColumnId == card.ColumnId).OrderBy(x => x.SortBy).Skip(curPosition).Take(prevPosition - curPosition + 1).ToList();
                for (int i = 0; i < result.Count - 1; i++)
                {
                    result[i].SortBy++;
                }
                result[result.Count - 1].SortBy = curPosition;
                result[result.Count - 1].UpdatedDate = DateTime.Now;
            }
            db.SaveChanges();
        }
    }
}
