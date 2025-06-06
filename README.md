📦 MiniEvents
A lightweight, event publisher for .NET. Publish events with ease and automatically wire up event handlers via dependency injection.

🚀 Features:

🔁 Publish/Subscribe pattern for decoupling event logic

⚙️ Automatic registration of event handlers via reflection

🧪 Testable and composable

🪶 Minimal, clean, no external dependencies

📄 Usage
1. Define an Event
public class UserRegisteredEvent : IEvent { 
    public string Email { get; set; } 
}

2. Create a Handler
public class SendWelcomeEmailHandler : IEventHandler<UserRegisteredEvent> {
    public Task HandleAsync(UserRegisteredEvent e, CancellationToken ct) {
        Console.WriteLine($"Sending welcome email to {e.Email}");
        return Task.CompletedTask;
    }
}

3. Register Services
In Program.cs:
builder.Services.AddMiniEvents(Assembly.GetExecutingAssembly());

4. Publish Event
public class UserService {
    private readonly IEventPublisher _eventPublisher;

    public UserService(IEventPublisher eventPublisher) {
        _eventPublisher = eventPublisher;
    }

    public void RegisterUser(string email) {
        // Logic to save user...
        _eventPublisher.Publish(new UserRegisteredEvent { Email = email });
    }
}
🔧 Setup
To use AddMiniEvents, make sure to pass in the assembly or assemblies where your IEventHandler<> implementations are located:

services.AddMiniEvents(typeof(SendWelcomeEmailHandler).Assembly);

✅ Benefits
-Encourages separation of concerns
-Eliminates tight coupling between components
-Easy to test and reason about

📂 Folder Structure (Example)
YourProject/
├── Events/
│   └── UserRegisteredEvent.cs
├── Handlers/
│   └── SendWelcomeEmailHandler.cs
├── Services/
│   └── UserService.cs

💬 Contributing
Pull requests are welcome! If you find a bug or have a feature request, please open an issue.
