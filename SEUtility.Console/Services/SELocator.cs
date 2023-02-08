using MediatR;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Notifications;
using SEUtility.Console.Interfaces;

namespace SEUtility.Console.Services;

internal class SELocator : ISELocator
{
    private readonly ILogger<SELocator> logger;
    private readonly IMediator mediator;

    public SELocator(ILogger<SELocator> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }
    public string? GetLocation()
    {
        var drives = DriveInfo.GetDrives().Where(a => a.DriveType == DriveType.Fixed);
        foreach (var item in drives)
        {
            mediator.Publish(new ConsoleNotification($"Searching {item.Name}"));
            var files = Directory.GetFiles(item.Name, "spaceengineers.exe", new EnumerationOptions() { RecurseSubdirectories = true });
            if (files.Any())
            {
                var location = files.FirstOrDefault(a => a.Contains("bin64", StringComparison.InvariantCultureIgnoreCase));
                if (location is not null)
                {
                    mediator.Publish(new ConsoleNotification($"Space Engineers Found", ConsoleStatus.SUCCESS));
                    mediator.Publish(new ConsoleNotification($"    =>{location}", ConsoleStatus.SUCCESS));
                    return location.Replace("\\bin64\\spaceengineers.exe", "", StringComparison.InvariantCultureIgnoreCase);
                }
            }
        }
        return null;
    }

}
