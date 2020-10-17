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

        //will update with proper email type in the future when we cover functionality in class
        public String Email { get; set; }

        //will update with proper password type in the future when we cover functionality in class
        public String Password { get; set; }

        //child object refrence
        public List<PC> PCs { get; set; }
    }
}