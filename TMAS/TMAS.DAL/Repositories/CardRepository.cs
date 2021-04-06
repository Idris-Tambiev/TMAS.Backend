using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace TMAS.DAL.Repositories
{
    public class CardRepository:ICardRepository
    {
        private AppDbContext db;

        public CardRepository(AppDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Card>> GetAll(int columnId)
        {
            return await db.Cards
                .Where(x => x.ColumnId == columnId)
                .Where(x => x.IsActive == true)
                .OrderBy(d=>d.SortBy)
                .ToListAsync();
        }

        public async Task<Card> GetOne(int cardId)
        {
            return await db.Cards.FirstOrDefaultAsync(i => i.Id == cardId);
        }
        public async Task<Card> CheckCard(int cardId,Boolean status)
        {
          var result=  await db.Cards.FirstOrDefaultAsync(i => i.Id == cardId);
            result.IsDone = status;
            result.UpdatedDate = DateTime.Now;
            db.SaveChanges();
              return result;
        }
        public async Task<IEnumerable<Card>> FindCards(int boardId, string search)
        {
            var findedCards =await db.Boards
                .Where(x => x.Id == boardId)
                .SelectMany(s => 
                s.Columns.SelectMany(
                    b=>b.Cards.Where(x=>
                    x.Title.Contains(search)))
                ).ToListAsync();
            return findedCards;
        }


        public async Task<Card> Create(Card card)
        {
            db.Cards.Add(card);
            await db.SaveChangesAsync();
            return card;
        }

        public async Task<Card> Update(Card card)
        {
            Card updatedCard = db.Cards.FirstOrDefault(x => x.Id == card.Id);
            updatedCard.Title = card.Title;
            updatedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedCard;
        }
        public async Task<Card> Move(Card card)
        {
            Card oldCard = db.Cards.Where(x => x.ColumnId == card.ColumnId).FirstOrDefault(x => x.SortBy == card.SortBy);
            Card updatedCard = db.Cards.FirstOrDefault(x => x.Id == card.Id);
            oldCard.SortBy = updatedCard.SortBy;
            oldCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            updatedCard.SortBy = card.SortBy;
            updatedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedCard;
        }

        public async Task<Card> Delete(int id)
        {
            Card deletedCard = db.Cards.FirstOrDefault(x => x.Id == id);
            deletedCard.IsActive = false;
            deletedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return deletedCard;
        }

        public async Task<Card> MoveOnColumn(Card card)
        {
            Card updatedCard = db.Cards.FirstOrDefault(x => x.Id == card.Id);
            MoveCards(card, updatedCard.ColumnId);
            updatedCard.ColumnId = card.ColumnId;
            updatedCard.SortBy = card.SortBy;
            updatedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedCard;
        }

        private void MoveCards(Card card,int prevPosition)
        {
            var result = db.Cards.Where(x => x.ColumnId == card.ColumnId).ToList();
            for(int i = 0; i < result.Count; i++)
            {
                if (result[i].SortBy >= card.SortBy)
                {
                    result[i].SortBy++;
                }
                    
               
            }
            db.SaveChanges();
            //уменьшить предыдущие
            var previousCards = db.Cards.Where(x => x.ColumnId == prevPosition).ToList();
            for (int i = 0; i < previousCards.Count; i++)
            {
                if (previousCards[i].SortBy >= card.SortBy)
                    previousCards[i].SortBy--;
            }
            db.SaveChanges();
        }
    }
}
