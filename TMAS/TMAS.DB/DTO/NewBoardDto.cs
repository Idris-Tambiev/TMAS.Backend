using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.DTO
{
    public class NewBoardDto
    {
        public string Title { get; set; }
        public Guid UserBoardId {get;set;}
    }
}
