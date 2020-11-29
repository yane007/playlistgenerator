namespace PG.Models.Abstract
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
