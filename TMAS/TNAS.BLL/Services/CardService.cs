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
        CardRepository card;
        public CardService(CardRepository repository)
        {
            card = repository;
        }
        public IEnumerable<Card> GetAll(int userId)
        {
            return card.GetAll(userId);
        }
        public Card GetOne(int id)
        {
            return card.GetOne(id);
        }

        public Card FindCard(string name)
        {
            return card.FindCard(name);
        }

        public void Create(Card createdCard)
        {

        }

        public void Update(Card updatedCard)
        {

        }

        public void Delete(int id)
        {

        }

    }
}
