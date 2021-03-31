using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Repositories
{
    public class CardRepository//:ICardRepository
    {
        private AppDbContext db;

        public CardRepository(AppDbContext context)
        {
            db = context;
        }
        public IEnumerable<Card> GetAll(int columnId)
        {
            return db.Cards.Where(x => x.ColumnId == columnId);
        }
        public Card GetOne(int id)
        {
            return db.Cards.FirstOrDefault(i => i.Id == id);
        }
        public IEnumerable<Card> FindCards(Guid id, string search)
        {
            return db.Cards.Where(p => p.Title.Contains(search)).ToList();
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
            updatedCard.Text = card.Text;
            updatedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return updatedCard;
        }

        public Card Delete(int id)
        {
            Card deletedCard = db.Cards.FirstOrDefault(x => x.Id == id);
            deletedCard.IsActive = false;
            deletedCard.UpdatedDate = DateTime.Now;
            db.SaveChanges();
            return deletedCard;
        }
    }
}
