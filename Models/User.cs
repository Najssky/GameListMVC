using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameListMVC.Models
{
        [Table("Users")]
        public class User
        {
            [Key]
            public int ID { get; set; }

        [Required(ErrorMessage = "Wprowadź Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Wprowadź Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wprowadź Nazwisko")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Wprowadź Email")]
        public string Email { get; set; }

    }
    
}
