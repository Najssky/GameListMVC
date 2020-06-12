using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameListMVC.Models
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string gameTitle { get; set; }
        public string gameGenre { get; set; }
        public string gameYear { get; set; }

    }
}
