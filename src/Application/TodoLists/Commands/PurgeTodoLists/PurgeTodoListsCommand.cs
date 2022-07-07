using RedPhase.Application.Common.Interfaces;
using RedPhase.Application.Common.Security;
using MediatR;

namespace RedPhase.Application.TodoLists.Commands.PurgeTodoLists;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public class PurgeTodoListsCommand : IRequest
{
}

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoListsCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        this._context.TodoLists.RemoveRange(this._context.TodoLists);

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
