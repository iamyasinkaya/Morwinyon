namespace Morwinyon.CustomizedMiddlewares;

/// <summary>
/// This class defines options for tracking user activity.
/// </summary>
public class UserActivityTrackingOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to log user activity to the console.
    /// The default value is true.
    /// </summary>
    public bool LogToConsole { get; set; } = true;

    /// <summary>
    /// Gets or sets the path to the file where user activity will be logged.
    /// The default value is "activity_log.txt".
    /// </summary>
    public string LogFilePath { get; set; } = "activity_log.txt";
}
