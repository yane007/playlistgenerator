using PG.Models.Abstract;

namespace PG.Models
{
    public class Genre : IdAndIsDeleted
    {
        public string Name { get; set; }
    }

}
