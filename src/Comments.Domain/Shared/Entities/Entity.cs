﻿namespace Comments.Domain.Shared.Entities
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
