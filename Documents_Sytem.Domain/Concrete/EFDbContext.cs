using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents_Sytem.Domain.Entities;

namespace Documents_Sytem.Domain.Concrete
{
    public class EFDbContext :DbContext
    {
        public EFDbContext()
            : base("name=DefaultConnection")
        {

        }

        public IDbSet<Document> Documents { get; set; }
    }
}
