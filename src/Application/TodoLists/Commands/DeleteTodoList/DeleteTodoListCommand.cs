using RedPhase.Application.Common.Exceptions;
using RedPhase.Application.Common.Interfaces;
using RedPhase.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RedPhase.Application.TodoLists.Commands.DeleteTodoList;

public class DeleteTodoListCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await this._context.TodoLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        this._context.TodoLists.Remove(entity);

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
