using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces;

namespace TMAS.DAL.Repositories
{
    public class CardRepository:ICardRepository
    {
        private AppDbContext db;

        public CardRepository(AppDbContext context)
        {
            db = context;
        }
        public IEnumerable<Card> GetAll(int userId)
        {
            return db.Cards;
        }
        public Card GetOne(int id)
        {
            return db.Cards.Find(id);
        }
        public Card FindCard(string card)
        {
            return db.Cards.Find(card);
        }

        public void Create(Card card)
        {
            db.Cards.Add(card);
        }

        public void Update(Card card)
        {
            // db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Card card = db.Cards.Find(id);
            if (card != null)
                db.Cards.Remove(card);
        }
    }
}
