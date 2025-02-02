﻿using Application.Abstract.Interfaces.Repositories;
using Application.Abstract.Messaging;
using AutoMapper;
using Domain.Abstract;
using Domain.Errors;

namespace Application.Modules.Members.GetMember;

internal sealed class GetMemberQueryHandler
    : IQueryHandler<GetMemberQuery, MemberResponse>
{
    private readonly IProjectMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetMemberQueryHandler(IProjectMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<Result<MemberResponse>> Handle(
        GetMemberQuery query,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository
            .GetAsync(query.UserId, query.ProjectId);

        if (member is null)
        {
            return Result<MemberResponse>
                .Failure(ProjectMemberErrors.NotFound);
        }

        return _mapper.Map<MemberResponse>(member);
    }
}
