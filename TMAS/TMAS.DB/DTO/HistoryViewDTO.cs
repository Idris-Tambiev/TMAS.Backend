using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMAS.DB.DTO
{
    public class HistoryViewDTO
    {
        public DateTime CreatedDate { get; set; }
        public int ActionType { get; set; }
        public string ActionObject { get; set; }
        public int? SourceAction { get; set; }
        public int? DestinationAction { get; set; }
    }
}
