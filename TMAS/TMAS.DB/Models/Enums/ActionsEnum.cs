using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.Models.Enums
{
        public enum Actions:byte
        {
            AddCard=0,
            EditCard=1,
            DeleteCard=2,
            AddColumn=3,
            EditColumn=4,
            DeleteColumn=5,
            CheckCardDone=6,
            CheckCardUndone=7,
            MoveCard=8,
            AddBoard=9,
            DeleteBoard=10,
            EditBoard=11
        }
}
