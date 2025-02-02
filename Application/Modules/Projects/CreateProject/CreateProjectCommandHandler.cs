﻿using Application.Abstract.Interfaces;
using Application.Abstract.Interfaces.Repositories;
using Application.Abstract.Messaging;
using AutoMapper;
using Domain.Abstract;
using Domain.Entities;
using Domain.Errors;

namespace Application.Modules.Projects.CreateProject;

internal sealed class CreateProjectCommandHandler
    : ICommandHandler<CreateProjectCommand, ProjectResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public CreateProjectCommandHandler(
        IUserRepository userRepository,
        IProjectRepository projectRepository,
        IDateTimeService dateTimeService,
        IMapper mapper,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _dateTimeService = dateTimeService;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<Result<ProjectResponse>> Handle(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Result<ProjectResponse>
                .Failure(UserErrors.UserNotFound);
        }

        bool projectExists = await _projectRepository
            .ExistsByNameAsync(command.UserId, command.Project.Name);

        if (projectExists)
        {
            return Result<ProjectResponse>
                .Failure(UserErrors.ProjectAlreadyExists(
                    command.UserId, command.Project.Name));
        }

        var role = await _roleRepository.GetByNameAsync("Admin");

        if (role is null)
        {
            return Result<ProjectResponse>
                .Failure(RoleErrors.NotFound);
        }

        var project = _mapper.Map<Project>(command.Project);

        project.CreatedAt = _dateTimeService.GetCurrentTime();
        project.CreatedBy = command.UserId;

        var projectId = await _projectRepository
            .CreateAsync(project, role.Id);

        project.Id = projectId;

        return Result<ProjectResponse>
            .Success(_mapper.Map<ProjectResponse>(project));
    }
}
