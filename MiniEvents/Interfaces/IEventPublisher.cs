using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEvents.Interfaces {
    public interface IEventPublisher {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
