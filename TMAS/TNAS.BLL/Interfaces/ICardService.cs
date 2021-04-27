using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface ICardService:IBaseService
    {
        Task<IEnumerable<CardViewDTO>> GetAll(int columnId);
        Task<CardViewDTO> CheckCard(int cardId, bool status, Guid userId);
        Task<CardViewDTO> GetOne(int cardId);
        Task<CardViewDTO> Create(Card card, Guid userId);
        Task<IEnumerable<CardViewDTO>> FindCard(int columnId, string search);
        Task<CardViewDTO> Update(Card updatedCard, Guid userId);
        Task<CardViewDTO> Move(Card movedCard, Guid userId);
        Task<CardViewDTO> MoveOnColumns(Card movedCard,Guid userId);
        Task<CardViewDTO> Delete(int id, Guid userId);
        Task<CardViewDTO> UpdateChanges(Card updatedCard, Guid userId);
    }

}
