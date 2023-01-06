using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class TrabajoImage
    {
        public int Id { get; set; }

        public int TrabajoId { get; set; }

        [ForeignKey("TrabajoId")]
        public virtual Trabajo Trabajo { get; set; }

        public int ImageId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
    }
}
