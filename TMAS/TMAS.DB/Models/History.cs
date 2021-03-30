using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models.Enums;
using TMAS.DB.Models.Interfaces;

namespace TMAS.DB.Models
{
  public  class History : IEntity, IAuditTabeEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Actions ActionType { get; set; }
       // public int ActionObjectId { get; set; }
        public Guid AuthorId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
