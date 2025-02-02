﻿using Application.Abstract;

namespace Application.Modules.Projects;

public record ProjectResponse(
    Guid Id,
    string Name,
    string? Description,
    Guid CreatedBy,
    DateTime CreatedAt,
    Guid? UpdatedBy,
    DateTime? UpdatedAt
    )
    : AuditableResponse(
        CreatedBy, CreatedAt, UpdatedBy, UpdatedAt);
