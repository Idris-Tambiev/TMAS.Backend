﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DAL.Interfaces.BaseInterfaces;

namespace TMAS.DAL.Interfaces
{
    public interface ICardRepository:IBaseRepository
    {
        IEnumerable<Card> GetAll(int columnId);
        Card GetOne(int id);
        Task<IEnumerable<Card>> FindCards(int id, string card);
        Task<Card> Create(Card card);
        Task<Card> Update(Card card);
        Task<Card> Delete(int id);
    }
}
