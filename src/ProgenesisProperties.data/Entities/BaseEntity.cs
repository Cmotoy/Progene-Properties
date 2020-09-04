using System;
namespace ProgenesisProperties.Entities
{
    public abstract class BaseEntity
    {
        public string Id {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime ModifiedAt {get; set;}
        public DateTime deletedAt {get;set;}
        public bool IsDeleted {get;set;}
        
    }
}