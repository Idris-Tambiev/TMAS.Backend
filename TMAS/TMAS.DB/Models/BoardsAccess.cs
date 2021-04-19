using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.Models
{
    public class BoardsAccess
    {
        public int BoardId { get; set; }
        public Guid BoardUserId { get; set; }
        public Board Board { get; set; }
        public User User { get; set; }
    }
}
