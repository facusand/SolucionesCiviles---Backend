using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string role { get; set; }
    }
}
