using RedPhase.Application.TodoLists.Queries.ExportTodos;

namespace RedPhase.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
