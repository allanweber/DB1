﻿namespace RH.Domain.Core.Entities
{
    public abstract class BaseEntity: IEntity
    {
        public int Id { get; private set; }
    }
}