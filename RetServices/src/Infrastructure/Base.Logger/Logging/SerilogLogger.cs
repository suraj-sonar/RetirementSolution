using Base.Application.Logging;
using Microsoft.Extensions.Logging;
namespace Base.logging.Logging;

public class SerilogLogger<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public SerilogLogger(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message) => _logger.LogInformation(message);
    public void LogWarning(string message) => _logger.LogWarning(message);
    public void LogError(Exception ex, string message) => _logger.LogError(ex, message);
    public void LogDebug(string message) => _logger.LogDebug(message);
}
