using PG.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Genre : IdAndIsDeleted
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }

}
