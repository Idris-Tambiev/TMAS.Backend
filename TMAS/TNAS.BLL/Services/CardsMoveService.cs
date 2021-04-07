using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class CardsMoveService
    {
        private AppDbContext db;
        public CardsMoveService(AppDbContext context)
        {
            db = context;
        }
        public void MoveOnNewColumn(Card card)
        {
            int currentPosition = card.SortBy;
            var result = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x=>x.SortBy)
                .Skip(currentPosition)
                .ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].SortBy++;
            }
            db.SaveChanges();
        }

        public void MoveOnOldColumn(Card card)
        {
            int prevPosition = card.SortBy;
            var previousCards = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x => x.SortBy)
                .Skip(prevPosition+1)
                .ToList();
            for (int i = 0; i < previousCards.Count; i++)
            {
                previousCards[i].SortBy--;
            }
            db.SaveChanges();
        }

        public void SwitchCards(int prevPosition, Card card)
        {
            int currentPosition = card.SortBy;
            if (currentPosition < prevPosition)
            {
                var result = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x => x.SortBy)
                .Skip(currentPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].SortBy < prevPosition)
                    {
                        result[i].SortBy++;
                    }
                }
            }
            else
            {
                var result = db.Cards
                .Where(x => x.ColumnId == card.ColumnId)
                .OrderBy(x => x.SortBy)
                .Skip(prevPosition+1)
                .Take(currentPosition - prevPosition)
                .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].SortBy--;
                }
            }
            db.SaveChanges();
        }
    }
}
