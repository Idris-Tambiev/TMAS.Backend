﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models.Interfaces;

namespace TMAS.DB.Models
{
    public class Column : IEntity, IAuditTabeEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public int OrderBy { get; set; }
        public Boolean IsActive { get; set; }
        public List<Card> Cards { get; set; }
        
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
