﻿namespace RedPhase.Domain.Events;

public class TodoItemCompletedEvent : DomainEvent
{
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
