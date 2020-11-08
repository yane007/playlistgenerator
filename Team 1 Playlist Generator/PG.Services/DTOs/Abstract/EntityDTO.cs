using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.DTOs.Abstract
{
    public abstract class EntityDTO
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
