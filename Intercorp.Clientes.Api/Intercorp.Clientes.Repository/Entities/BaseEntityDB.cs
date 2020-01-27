using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Repository.Entities
{
    public class BaseEntityDB
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted { get; set; }
    }
}
