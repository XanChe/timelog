using System;


namespace Timelog.Entities
{
    public abstract class Entity
    {
        public Guid UniqId { get; set; }
        public long Id { get; set; }
        public Guid UserUniqId { get; set; }
        public Entity()
        {
            UniqId = Guid.NewGuid();
        }

      
    }
}

