using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using AutoMapper;
using TMAS.DAL.DTO;
using TMAS.DB.Context;
using Microsoft.EntityFrameworkCore;
using TMAS.DAL.Interfaces;
using TMAS.DB.Models.Enums;
using TMAS.DAL.DTO.View;
using TMAS.DAL.DTO.Created;
using FluentValidation;

namespace TMAS.BLL.Services
{
    public class CardService:ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IHistoryService _historyService;
        private readonly IColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        private readonly ICardsSortService _sortService;
        private readonly AbstractValidator<CardContentDTO> _cardContentValidator;
        private readonly AbstractValidator<CardCreatedDTO> _cardValidator;

        public CardService(ICardRepository repository,
            IMapper mapper,AppDbContext context,
            ICardsSortService cardsMoveService, 
            IHistoryService historyService, 
            IColumnRepository columnRepository,
            AbstractValidator<CardContentDTO> cardContentValidator,
            AbstractValidator<CardCreatedDTO> cardValidator
            )
        {
            _cardRepository = repository;
            _mapper = mapper;
            _historyService = historyService;
            _sortService = cardsMoveService;
            _columnRepository = columnRepository;
            _cardContentValidator = cardContentValidator;
            _cardValidator = cardValidator;
        }
        public async Task<IEnumerable<CardViewDTO>> GetAll(int columnId)
        {
            var allCards = await _cardRepository.GetAll(columnId);
            var mapperResult = _mapper.Map<IEnumerable<Card>,IEnumerable<CardViewDTO>>(allCards);
            return mapperResult;
        }
        public async Task<CardViewDTO> CheckCard(int cardId, bool status, Guid userId)
        {

            Card card =await _cardRepository.GetOne(cardId);
            Column column = await _columnRepository.GetOne(card.ColumnId);
            var result = await _cardRepository.CheckCard(cardId, status);
            UserActions action;

            if (status) action = UserActions.CheckedCard;
            else action = UserActions.UncheckedCard;

            var history = await _historyService.CreateHistoryObject(
                action,
                userId,
                card.Title,
                null,
                null,
                column.BoardId
                );

            var mapperResult = _mapper.Map<Card, CardViewDTO>(result);
            return mapperResult;
          
        }

        public async Task<CardFullDTO> GetOne(int cardId)
        {
            var card = await _cardRepository.GetOne(cardId);
            var mapperResult = _mapper.Map<Card, CardFullDTO>(card);
            return mapperResult;
        }

        public async Task<CardViewDTO> Create(CardCreatedDTO card, Guid userId)
        {
            var validationResult = _cardValidator.Validate(card);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                Column column = await _columnRepository.GetOne(card.ColumnId);
                var mapperCard = _mapper.Map<CardCreatedDTO, Card>(card);
                var dbCard = await _cardRepository.Create(mapperCard);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.CreateCard,
                    userId,
                    card.Title,
                    null,
                    null,
                    column.BoardId
                    );

                var mapperResult = _mapper.Map<Card, CardViewDTO>(dbCard);
                return mapperResult;
            }
        }

        public async Task<IEnumerable<CardViewDTO>> FindCard(int columnId, string search)
        {
            var result= await _cardRepository.FindCards(columnId, search);
            var mapperResult = _mapper.Map<IEnumerable<Card>, IEnumerable<CardViewDTO>>(result);
            return mapperResult;
        }

        public async Task<CardViewDTO> UpdateTitle(int id, string title, Guid userId)
        {
            if(id != null && title != null)
            {
                Card card = await _cardRepository.GetOne(id);
                Column column = await _columnRepository.GetOne(card.ColumnId);
                card.UpdatedDate = DateTime.Now;
                card.Title = title;
                var result = await _cardRepository.Update(card);

                var history = await _historyService.CreateHistoryObject(
                    UserActions.UpdateCard,
                    userId,
                    title,
                    null,
                    null,
                    column.BoardId
                    );

                var mapperResult = _mapper.Map<Card, CardViewDTO>(result);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty id or title");
            }
            
        }

        public async Task<CardViewDTO> UpdateContent(CardContentDTO updatedCard, Guid userId)
        {
            var validationResult = _cardContentValidator.Validate(updatedCard);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                Card card = await _cardRepository.GetOne(updatedCard.Id);
                Column column = await _columnRepository.GetOne(card.ColumnId);
                var oldCard = await _cardRepository.GetOne(updatedCard.Id);
                card.UpdatedDate = DateTime.Now;

                if (oldCard.Text == null && updatedCard.Text != null)
                {
                    card.Text = updatedCard.Text;
                    var historyObject = await _historyService.CreateHistoryObject(
                    UserActions.AddedDescription,
                    userId,
                    card.Title,
                    null,
                    null,
                    column.BoardId
                    );
                }
                if (oldCard.Text != null && updatedCard.Text != oldCard.Text)
                {
                    card.Text = updatedCard.Text;
                    var historyObject = await _historyService.CreateHistoryObject(
                    UserActions.EditedDescription,
                    userId,
                    card.Title,
                    null,
                    null,
                    column.BoardId
                    );
                }
                if (oldCard.ExecutionPeriod != updatedCard.ExecutionPeriod)
                {
                    card.ExecutionPeriod = updatedCard.ExecutionPeriod;
                    var historyObject = await _historyService.CreateHistoryObject(
                    UserActions.ChangeExecutionPeriod,
                    userId,
                    card.Title,
                    null,
                    null,
                    column.BoardId
                    );
                }

                var result = await _cardRepository.Update(card);
                var mapperResult = _mapper.Map<Card, CardViewDTO>(result);
                return mapperResult;
            }
        }

        public async Task<CardViewDTO> Move(CardViewDTO movedCard, Guid userId)
        {

            Card updatedCard = await _cardRepository.GetOne(movedCard.Id);
            Column column = await _columnRepository.GetOne(updatedCard.ColumnId);

            await _sortService.SwitchCards(updatedCard.SortBy,movedCard);
            updatedCard.SortBy = movedCard.SortBy;
            updatedCard.UpdatedDate = DateTime.Now;

            var updateResult=await _cardRepository.Update(updatedCard);

            var historyObject =await _historyService.CreateHistoryObject(
                UserActions.MoveCard,
                userId,
                updatedCard.Title,
                null,
                movedCard.ColumnId,
                column.BoardId
                );

            var mapperResult = _mapper.Map<Card, CardViewDTO>(updateResult);
            return mapperResult;

        }

        public async Task<CardViewDTO> MoveOnColumns(CardViewDTO movedCard, Guid userId)
        {

            Card updateCard = await _cardRepository.GetOne(movedCard.Id);
            await _sortService.MoveOnNewColumn(movedCard);
            await _sortService.MoveOnOldColumn(updateCard);

            int oldColumn = updateCard.ColumnId;
            Column column = await _columnRepository.GetOne(updateCard.ColumnId);
            updateCard.ColumnId = movedCard.ColumnId;
            updateCard.SortBy = movedCard.SortBy;
            updateCard.UpdatedDate = DateTime.Now;

            var updateResult=await _cardRepository.Update(updateCard);


            var historyObject =await _historyService.CreateHistoryObject(
                UserActions.MoveCardOnOtherColumn,
                userId,
                updateCard.Title,
                oldColumn,
                movedCard.ColumnId,
                column.BoardId
                );

            var mapperResult = _mapper.Map<Card, CardViewDTO>(updateResult);
            return mapperResult;
        }
        public async Task<CardViewDTO> Delete(int id, Guid userId)
        {
            Card card = await _cardRepository.GetOne(id);
            Column column = await _columnRepository.GetOne(card.ColumnId);
            var reduceResult =await _sortService.ReduceAfterDeleteAsync(id);
            var result = await _cardRepository.Delete(id);
            var history = await _historyService.CreateHistoryObject(
                UserActions.DeleteCard,
                userId,
                card.Title,
                null,
                null,
                column.BoardId
                );
            var mapperResult = _mapper.Map<Card, CardViewDTO>(result);
            return mapperResult;
        }

    }
}
