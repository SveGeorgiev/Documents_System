using Documents_Sytem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents_System.Models
{
    public class DocumentViewModel
    {
        public IEnumerable<Document> Documents { get; set; }

        //public int Id { get; set; }

        //public string Name { get; set; }

        //public string Description { get; set; }

        //public string Category { get; set; }

        //public DateTime? DateTime { get; set; }

        //public string uniqueName { get; set; }
    }
}