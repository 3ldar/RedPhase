using RedPhase.Application.Common.Exceptions;
using RedPhase.Application.Common.Interfaces;
using RedPhase.Domain.Entities;
using MediatR;

namespace RedPhase.Application.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await this._context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.Title = request.Title;
        entity.Done = request.Done;

        await this._context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
