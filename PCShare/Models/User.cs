using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCShare.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public String Username { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        //child object refrence
        public List<PC> PCs { get; set; }
    }
}