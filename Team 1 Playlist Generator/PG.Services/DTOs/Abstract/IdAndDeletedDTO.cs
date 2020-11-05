using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.DTOs.Abstract
{
    public class IdAndDeletedDTO
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
