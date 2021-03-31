using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface ICardService:IBaseService<Card>,IGetAllByInt<Card>,ICreateWithoutGuid<Card>
    {
        // Card FindCard(string card);
        Task<Card> Create(Card createdCard);
    }
}
