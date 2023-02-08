using MediatR;
using SEUtility.Common.Notifications;
using SEUtility.Console.Interfaces;

namespace SEUtility.Console.Services;

internal class ConsoleNotificationHandler : INotificationHandler<ConsoleNotification>
{
    private readonly IConsoleWriter console;

    public ConsoleNotificationHandler(IConsoleWriter console)
    {
        this.console = console;
    }
    public Task Handle(ConsoleNotification notification, CancellationToken cancellationToken)
    {
        switch (notification.Status)
        {
            case ConsoleStatus.WARNING:
                console.Warning(notification.Message);
                break;
            case ConsoleStatus.ERROR:
                console.Error(notification.Message);
                break;
            case ConsoleStatus.SUCCESS:
                console.Success(notification.Message);
                break;
            case ConsoleStatus.INFORMATION:
            default:
                console.Output(notification.Message);
                break;
        }
        return Task.CompletedTask;
    }
}
