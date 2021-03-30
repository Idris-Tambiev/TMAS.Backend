using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace TMAS.DB.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        public List<Board> Boards { get; set; }
        public List<History> Histories { get; set; }
        public List<Card> Cards { get; set; }
    }
}
