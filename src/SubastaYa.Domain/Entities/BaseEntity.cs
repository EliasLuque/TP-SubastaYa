namespace SubastaYa.Domain.Entities
{ 
    public enum EntityState
    {
        ACTIVE = 1,
        INACTIVE = 0
    }

    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public EntityState State { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}