﻿using Domain.Abstract;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public Guid UserId { get; set; }
    public string? Description { get; set; }
}
