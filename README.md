# TheOne.Logging

Logger Manager for Unity

## Installation

### Option 1: Unity Scoped Registry (Recommended)

Add the following scoped registry to your project's `Packages/manifest.json`:

```json
{
  "scopedRegistries": [
    {
      "name": "TheOne Studio",
      "url": "https://upm.the1studio.org/",
      "scopes": [
        "com.theone"
      ]
    }
  ],
  "dependencies": {
    "com.theone.logging": "1.1.0"
  }
}
```

### Option 2: Git URL

Add to Unity Package Manager:
```
https://github.com/The1Studio/TheOne.Logging.git
```

## Features

- Centralized logging system with named loggers
- Configurable log levels and filtering
- Unity Console integration
- Performance-optimized with lazy evaluation
- Caller context information (method names)
- Exception logging support
- Support for custom logger implementations
- Typed logger creation for better organization
- Integration with dependency injection frameworks

## Dependencies

- TheOne.Extensions

## Usage

### Basic Logging

```csharp
using TheOne.Logging;

public class GameManager
{
    private readonly ILogger logger;
    
    public GameManager(ILoggerManager loggerManager)
    {
        // Get a logger for this class
        logger = loggerManager.GetLogger<GameManager>();
        // Or by name: logger = loggerManager.GetLogger("GameManager");
    }
    
    public void StartGame()
    {
        logger.Info("Starting new game");
        logger.Debug("Loading player data");
        
        try
        {
            InitializeGame();
            logger.Info("Game started successfully");
        }
        catch (Exception ex)
        {
            logger.Exception(ex);
            logger.Error("Failed to start game");
        }
    }
}
```

### Advanced Logging with Lazy Evaluation

```csharp
// Expensive operations only executed when log level allows
logger.Debug(() => $"Complex calculation result: {ExpensiveCalculation()}");

// Object logging (calls ToString())
logger.Info(playerStats);
logger.Warning(gameSettings);

// Different log levels
logger.Debug("Detailed debug information");
logger.Info("General information");
logger.Warning("Something might be wrong");
logger.Error("An error occurred");
logger.Critical("Critical system failure");
```

### Logger Creation Patterns

```csharp
public class LoggingService
{
    private readonly ILoggerManager loggerManager;
    
    public LoggingService(ILoggerManager loggerManager)
    {
        this.loggerManager = loggerManager;
    }
    
    public void LogFromDifferentContexts()
    {
        // Get logger by type
        var gameLogger = loggerManager.GetLogger<GameManager>();
        
        // Get logger by object instance
        var playerLogger = loggerManager.GetLogger(player);
        
        // Get logger by name
        var systemLogger = loggerManager.GetLogger("SystemEvents");
        
        // Get default logger
        var defaultLogger = loggerManager.GetDefaultLogger();
        
        // Get all active loggers
        var allLoggers = loggerManager.GetAllLoggers();
    }
}
```

### Custom Logger Implementation

```csharp
public class FileLogger : Logger
{
    private readonly string filePath;
    
    public FileLogger(string name, LogConfig config, string filePath) : base(name, config)
    {
        this.filePath = filePath;
    }
    
    protected override void Debug(string message) => WriteToFile(message);
    protected override void Info(string message) => WriteToFile(message);
    protected override void Warning(string message) => WriteToFile(message);
    protected override void Error(string message) => WriteToFile(message);
    protected override void Critical(string message) => WriteToFile(message);
    
    protected override void Exception(Exception exception)
    {
        WriteToFile($"EXCEPTION: {exception}");
    }
    
    private void WriteToFile(string message)
    {
        File.AppendAllText(filePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}\n");
    }
}

// Custom LoggerManager implementation
public class FileLoggerManager : LoggerManager
{
    private readonly string logDirectory;
    
    public FileLoggerManager(LogLevel level, string logDirectory) : base(level)
    {
        this.logDirectory = logDirectory;
    }
    
    protected override ILogger CreateLogger(string name, LogConfig config)
    {
        var filePath = Path.Combine(logDirectory, $"{name}.log");
        return new FileLogger(name, config, filePath);
    }
}
```

## Architecture

### Folder Structure

```
TheOne.Logging/
├── Scripts/
│   ├── ILoggerManager.cs          # Logger management interface
│   ├── LoggerManager.cs           # Abstract logger manager base
│   ├── UnityLoggerManager.cs      # Unity-specific implementation
│   ├── ILogger.cs                 # Core logging interface
│   ├── Logger.cs                  # Abstract logger base class
│   ├── UnityLogger.cs             # Unity Console logger implementation
│   ├── LogConfig.cs               # Logging configuration and levels
│   └── DI/                        # Dependency injection extensions
│       ├── LoggerManagerDI.cs
│       ├── LoggerManagerVContainer.cs
│       └── LoggerManagerZenject.cs
```

### Core Classes

#### `ILoggerManager`
Central manager for creating and accessing loggers:
- `GetLogger(string name)` - Get logger by name
- `GetLogger<T>()` - Get logger by type
- `GetLogger(object owner)` - Get logger from object instance
- `GetDefaultLogger()` - Get the default "TheOne" logger
- `GetAllLoggers()` - Access all created loggers

#### `ILogger`
Core logging interface with multiple signatures:
- **Basic Logging**: `Debug()`, `Info()`, `Warning()`, `Error()`, `Critical()`
- **Lazy Evaluation**: All methods support `Func<string>` for expensive operations
- **Object Logging**: Methods accept `object` parameter (calls ToString())
- **Exception Logging**: `Exception(Exception ex)` for exception handling
- **Caller Context**: Automatic method name detection using `[CallerMemberName]`

#### `LogConfig` and `LogLevel`
Configuration system with hierarchical log levels:
- `Debug` - Detailed diagnostic information
- `Info` - General informational messages
- `Warning` - Warning conditions
- `Error` - Error conditions
- `Critical` - Critical error conditions
- `Exception` - Exception logging
- `None` - Disable logging

#### `Logger` (Abstract Base)
Template method implementation providing:
- Level filtering before message processing
- Message formatting with context information
- Caller member name detection
- Template methods for concrete implementations

#### `UnityLogger` & `UnityLoggerManager`
Unity-specific implementation:
- Integrates with Unity Console
- Maps log levels to Unity's log types
- Preserves Unity's log formatting and filtering
- Tagged with "TheOne" for easy filtering

### Design Patterns

- **Factory Pattern**: LoggerManager creates loggers on demand
- **Template Method**: Logger base class with concrete implementations
- **Lazy Evaluation**: Func<string> parameters for performance optimization
- **Strategy Pattern**: Different logger implementations (Unity, File, etc.)
- **Registry Pattern**: Logger caching by name for singleton behavior

### Code Style & Conventions

- **Namespace**: All code under `TheOne.Logging` namespace
- **Null Safety**: Uses `#nullable enable` directive
- **Interfaces**: Prefixed with `I` (e.g., `ILogger`)
- **Caller Context**: Automatic method name detection
- **Performance**: Lazy evaluation for expensive log message creation
- **Extensibility**: Abstract base classes for custom implementations

### Performance Optimizations

```csharp
// Level checking prevents unnecessary string formatting
public void PerformanceExample()
{
    // Bad - always creates string even if debug is disabled
    logger.Debug($"Processing {items.Count} items: {string.Join(", ", items)}");
    
    // Good - only creates string if debug level is enabled
    logger.Debug(() => $"Processing {items.Count} items: {string.Join(", ", items)}");
}

// Logger caching prevents recreation
var logger = loggerManager.GetLogger<MyClass>(); // Creates once
var sameLogger = loggerManager.GetLogger<MyClass>(); // Returns cached instance
Debug.Assert(ReferenceEquals(logger, sameLogger)); // True
```

### Integration with DI Frameworks

#### VContainer
```csharp
using TheOne.Logging.DI;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // Register with specific log level
        builder.RegisterLoggerManager(LogLevel.Info);
        
        // Services can now inject ILoggerManager
        builder.Register<GameManager>(Lifetime.Singleton);
    }
}
```

#### Zenject
```csharp
using TheOne.Logging.DI;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LogLevel logLevel = LogLevel.Info;
    
    public override void InstallBindings()
    {
        // Register with configurable log level
        Container.BindLoggerManager(logLevel);
        
        // Services automatically get logging support
        Container.BindInterfacesTo<GameManager>().AsSingle();
    }
}
```

#### Custom DI
```csharp
using TheOne.Logging.DI;

// Register with your DI container
container.RegisterLoggerManager(LogLevel.Debug);

// Or manual registration
container.Register<ILoggerManager>(() => new UnityLoggerManager(LogLevel.Info));
```

### Advanced Usage Patterns

#### Structured Logging
```csharp
public class GameMetrics
{
    private readonly ILogger logger;
    
    public void LogPlayerAction(string action, int playerId, Dictionary<string, object> data)
    {
        logger.Info(() => $"Player {playerId} performed {action}: {JsonUtility.ToJson(data)}");
    }
}
```

#### Contextual Loggers
```csharp
public class GameSession
{
    private readonly ILogger sessionLogger;
    
    public GameSession(ILoggerManager loggerManager, string sessionId)
    {
        // Create context-specific logger
        sessionLogger = loggerManager.GetLogger($"GameSession_{sessionId}");
    }
    
    public void StartSession()
    {
        sessionLogger.Info("Session started"); // Automatically includes session ID in logs
    }
}
```

#### Conditional Logging
```csharp
public class DebugService
{
    private readonly ILogger logger;
    
    public void ConditionalLogging()
    {
        #if DEVELOPMENT_BUILD || UNITY_EDITOR
        logger.Debug("Development-only debug information");
        #endif
        
        // Runtime conditional logging
        if (Application.isEditor)
        {
            logger.Debug("Editor-only debugging");
        }
    }
}
```

## Performance Considerations

- Loggers are cached by name to prevent recreation overhead
- Level filtering occurs before message formatting
- Lazy evaluation with `Func<string>` prevents expensive operations when disabled
- Caller member name detection uses compile-time attributes (zero runtime cost)
- Unity integration preserves native console performance
- String formatting only occurs when log level permits output

## Best Practices

1. **Logger Naming**: Use consistent naming patterns (class names, feature areas)
2. **Log Levels**: Use appropriate levels - Debug for diagnostics, Info for flow, Warning/Error for issues
3. **Lazy Evaluation**: Use `Func<string>` for expensive log message generation
4. **Context**: Let automatic caller context provide method names
5. **Exception Handling**: Always log exceptions with full stack traces
6. **Performance**: Avoid logging in hot paths unless necessary
7. **Configuration**: Set appropriate log levels for different build configurations
8. **Structured Data**: Consider JSON or structured formats for complex data
9. **Testing**: Mock ILoggerManager for unit tests to verify logging behavior

---

# API Reference

Namespace: `TheOne.Logging`

## ILogger

**Properties:**
- `string Name { get; }`
- `LogLevel LogLevel { get; set; }`

**Extension Methods (via LoggerExtensions):**
- `Debug(string message, [CallerMemberName] string context = "")` - `[Conditional("THEONE_LOGGING_DEBUG")]`
- `Info(string message, [CallerMemberName] string context = "")` - `[Conditional("THEONE_LOGGING_INFO")]`
- `Warning(string message, [CallerMemberName] string context = "")` - `[Conditional("THEONE_LOGGING_WARNING")]`
- `Error(string message, [CallerMemberName] string context = "")` - `[Conditional("THEONE_LOGGING_ERROR")]`
- `Critical(string message, [CallerMemberName] string context = "")` - `[Conditional("THEONE_LOGGING_CRITICAL")]`
- `Exception(Exception exception, [CallerMemberName] string context = "")` - `[Conditional("THEONE_LOGGING_EXCEPTION")]`

All methods are conditionally compiled and auto-detect caller member name.

## LogLevel Enum
`Debug(0)` < `Info(1)` < `Warning(2)` < `Error(3)` < `Critical(4)` < `Exception(5)` < `None(6)`

## ILoggerManager
- `ILogger GetLogger(string name)`
- `ILogger GetLogger(Type ownerType)` - delegates to `GetLogger(ownerType.Name)`
- `ILogger GetLogger(object owner)` - delegates to `GetLogger(owner.GetType())`
- `ILogger GetLogger<T>()` - delegates to `GetLogger(typeof(T))`
- `ILogger GetDefaultLogger()` - delegates to `GetLogger(nameof(TheOne))`
- `IEnumerable<ILogger> GetAllLoggers()`

## Classes
- `LoggerManager` (abstract) - `protected LoggerManager(LogLevel logLevel)`, abstract `CreateLogger(string name, LogLevel logLevel)`
- `UnityLogger` (sealed) - implements ILogger, formats: `[LogLevel] [Name] [Context] Message`
- `UnityLoggerManager` (sealed) - `[Preserve] public UnityLoggerManager(LogLevel logLevel)`, creates UnityLogger instances

## DI Registration
- VContainer: `builder.RegisterLoggerManager()` - registers with default LogLevel.Info
- Zenject: `Container.BindLoggerManager()` - binds ILogger per-object via `GetLogger(ctx.ObjectType)`
- Custom DI: `container.AddLoggerManager()` - registers with default LogLevel.Info

## Conditional Symbols
`THEONE_LOGGING_DEBUG`, `THEONE_LOGGING_INFO`, `THEONE_LOGGING_WARNING`, `THEONE_LOGGING_ERROR`, `THEONE_LOGGING_CRITICAL`, `THEONE_LOGGING_EXCEPTION` - control which log levels are compiled in