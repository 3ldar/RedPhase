﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using RedPhase.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RedPhase.Application.TodoLists.Queries.ExportTodos;

public class ExportTodosQuery : IRequest<ExportTodosVm>
{
    public int ListId { get; set; }
}

public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _fileBuilder;

    public ExportTodosQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
    {
        this._context = context;
        this._mapper = mapper;
        this._fileBuilder = fileBuilder;
    }

    public async Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
    {
        var records = await this._context.TodoItems
                .Where(t => t.ListId == request.ListId)
                .ProjectTo<TodoItemRecord>(this._mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        var vm = new ExportTodosVm(
            "TodoItems.csv",
            "text/csv",
            this._fileBuilder.BuildTodoItemsFile(records));

        return vm;
    }
}
