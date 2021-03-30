﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DB.Models.Interfaces;

namespace TMAS.DB.Models
{
    public class Card : IEntity, IAuditTabeEntity
    {
        public int Id { get; set; }
        public DateTime ExecutionPeriod { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        //varchar(5000)
        public string Text { get; set; }
        public Boolean IsDone { get; set; }

        [Required]
        public Boolean IsActive { get; set; }
        public int ColumnId { get; set; }
        public Column Column { get; set; }
    }
}
