﻿namespace Domain.Entities;

public class Project : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
