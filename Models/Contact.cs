using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameListMVC.Models
{   
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMessage { get; set; }
        public string ContactSubject { get; set; }
        
    }
}