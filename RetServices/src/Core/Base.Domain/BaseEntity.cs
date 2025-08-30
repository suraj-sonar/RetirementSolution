namespace Base.Domain
{
    public class BaseEntity
    {
        public int id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
