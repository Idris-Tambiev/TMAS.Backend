using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface ICardsSortService
    {
        Task MoveOnNewColumn(Card card);
        Task MoveOnOldColumn(Card card);
        Task SwitchCards(int prevPosition, Card card);
        Task<Card> ReduceAfterDeleteAsync(int id);
    }
}
