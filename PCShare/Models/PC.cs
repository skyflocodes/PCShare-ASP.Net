using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCShare.Models
{
    public class PC
    {
        public int UserId { get; set; }
        public int Id { get; set; }

        [Required]
        public String CPU { get; set; }

        public String GPU { get; set; }

        public String MOBO { get; set; }

        //parent refrence model
        public User User { get; set; }
    }
}
