using MediatR;

namespace SEUtility.Common.Notifications;

public enum ConsoleStatus
{
    INFORMATION,
    WARNING,
    ERROR,
    SUCCESS
}
public class ConsoleNotification : INotification
{
    public string Message { get; private set; }
    public ConsoleStatus Status { get; private set; }

    public ConsoleNotification(string message, ConsoleStatus status)
    {
        Message = message;
        Status = status;
    }

    public ConsoleNotification(string message) : this(message, ConsoleStatus.INFORMATION) { }
}
