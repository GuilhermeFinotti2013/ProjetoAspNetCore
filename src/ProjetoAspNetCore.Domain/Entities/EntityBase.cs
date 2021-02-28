using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoAspNetCore.Domain.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public EntityBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
