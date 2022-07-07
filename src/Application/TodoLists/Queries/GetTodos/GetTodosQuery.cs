using AutoMapper;
using AutoMapper.QueryableExtensions;
using RedPhase.Application.Common.Interfaces;
using RedPhase.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RedPhase.Application.TodoLists.Queries.GetTodos;

public class GetTodosQuery : IRequest<TodosVm>
{
}

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = await this._context.TodoLists
                .AsNoTracking()
                .ProjectTo<TodoListDto>(this._mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}
