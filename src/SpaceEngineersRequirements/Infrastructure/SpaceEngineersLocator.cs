using FluentResults;
using Microsoft.Extensions.Logging;
using SpaceEngineers.Application.Interfaces;

namespace SpaceEngineersRequirements.Infrastructure;
internal class SpaceEngineersLocator(ILogger<SpaceEngineersLocator> logger) : ISpaceEngineersLocator
{
    public readonly ILogger<SpaceEngineersLocator> _logger = logger;

    private const string SPACE_ENGINEERS_EXE_NAME = "spaceengineers.exe";
    private const string SPACE_ENGINEERS_BIN_FOLDER = "bin64";
    public Result<string> GetLocation()
    {
        var drives = DriveInfo.GetDrives().Where(a => a.DriveType == DriveType.Fixed);
        foreach (var item in drives)
        {
            _logger.LogDebug("Searching {drive}", item.Name);
            var files = Directory.GetFiles(item.Name, SPACE_ENGINEERS_EXE_NAME, new EnumerationOptions() { RecurseSubdirectories = true });
            _logger.LogDebug("Found {fileCount} files on {drive}", files.Length, item.Name);
            if (files.Length != 0)
            {
                var location = files.FirstOrDefault(a => a.Contains(SPACE_ENGINEERS_BIN_FOLDER, StringComparison.InvariantCultureIgnoreCase));
                if (location is not null)
                {
                    return location.Replace($"\\{SPACE_ENGINEERS_BIN_FOLDER}\\{SPACE_ENGINEERS_EXE_NAME}", "", StringComparison.InvariantCultureIgnoreCase);
                }
            }
        }

        return Result.Fail("Space Engineers Not Found");
    }
}
