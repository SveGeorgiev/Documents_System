using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents_Sytem.Domain.Entities
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [StringLength(24, ErrorMessage = "The {0} must be {2} - {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yy H:mm:ss}")]
        public DateTime? DateTime { get; set; }

        public string uniqueName { get; set; }
    }
}
