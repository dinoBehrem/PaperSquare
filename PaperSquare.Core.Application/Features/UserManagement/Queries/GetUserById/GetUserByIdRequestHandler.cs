using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Features.UserManagement.Dto;
using PaperSquare.Core.Application.Mapper.UserMappings;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Infrastructure.Exceptions;

namespace PaperSquare.Core.Application.Features.UserManagement.Queries.GetUserById;

public sealed class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Result<UserResponse>>
{
    private readonly IPaperSquareDbContext _context;

    public GetUserByIdRequestHandler(IPaperSquareDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Equals(request.id) && !u.IsDeleted, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundEntityException($"{typeof(User).Name} not found!", typeof(User));
        }

        return Result.Success(entity.ToUserResponse());
    }
}
