using MediatR;

namespace Restaurant.Order.Application.OutBox;

public class OutboxMessage<E> : INotification
{

    public E Content { get; private set; }
    
    public Guid Id { get; set; }
    
    public string Type { get; set; }
    
    public DateTime Created { get; set; }
    
    public bool Processed { get; set; }
    
    public DateTime? ProcessedOn { get; set; }

    public OutboxMessage(E content)
    {
        Id = Guid.NewGuid();
        Created = DateTime.Now;
        Processed = false;
        Content = content;
        Type = content.GetType().Name;
    }

    public void MarkAsProcessed()
    {
        ProcessedOn = DateTime.Now;
        Processed = true;
    }

    private OutboxMessage(){
        
    }

    
}

