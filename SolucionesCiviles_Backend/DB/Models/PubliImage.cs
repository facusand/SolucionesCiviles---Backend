using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class PubliImage
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Descripcion { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
