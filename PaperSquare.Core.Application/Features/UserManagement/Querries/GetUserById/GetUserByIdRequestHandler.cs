using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Domain.Entities.Identity;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Querries.GetUserById;

public sealed class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Result<UserDto>>
{
    private readonly PaperSquareDbContext _context;
    private readonly IMapper _mapper;
    public GetUserByIdRequestHandler(IMapper mapper, PaperSquareDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Result<UserDto>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundEntityException($"{typeof(User).Name} not found!", typeof(User));
        }

        var mappedEntity = _mapper.Map<UserDto>(entity);

        return Result.Success(mappedEntity);
    }
}
