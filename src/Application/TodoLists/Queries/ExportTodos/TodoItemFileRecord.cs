using RedPhase.Application.Common.Mappings;
using RedPhase.Domain.Entities;

namespace RedPhase.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
