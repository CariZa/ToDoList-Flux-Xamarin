using System;
namespace ToDoListFlux.Stores
{
    public class StoreEventArgs: EventArgs
    {
        public string EventType { get; set; }
         
        public StoreEventArgs(string eventType)
        {
            EventType = eventType;
        }
    }
}
