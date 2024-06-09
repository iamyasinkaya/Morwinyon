using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using System.Data;
using System.Data.SqlClient;

namespace Morwinyon.Logging;

public class DatabaseLogger : ILogger
{
    private readonly string _categoryName;
    private readonly DatabaseOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DatabaseLogger(string categoryName, DatabaseOptions options = null, IHttpContextAccessor httpContextAccessor = null)
    {
        _categoryName = categoryName;
        _options = options ?? new DatabaseOptions();
        _httpContextAccessor = httpContextAccessor;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => _options.IsEnabled && ConvertLogLevelToSerilogLevel(logLevel) >= _options.MinimumLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        string message = formatter(state, exception);

        // HttpContext'ten gelen request bilgilerini ekle
        if (_httpContextAccessor?.HttpContext != null)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var request = httpContext.Request;
            message = $"[{request.Method}] {request.Path} - {message}";
        }

        // Veritabanına loglama yap
        switch (_options.DatabaseType)
        {
            case DatabaseType.MsSql:
                LogToMsSqlDatabase(logLevel, message);
                break;
            case DatabaseType.MySql:
                LogToMySqlDatabase(logLevel, message);
                break;
            default:
                throw new NotSupportedException("Database type not supported.");
        }
    }

    // LogLevel'ü Serilog.Events.LogEventLevel türüne dönüştüren yöntem
    private LogEventLevel ConvertLogLevelToSerilogLevel(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => LogEventLevel.Verbose,
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Information => LogEventLevel.Information,
            LogLevel.Warning => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            LogLevel.Critical => LogEventLevel.Fatal,
            _ => LogEventLevel.Information,
        };
    }

    private void LogToMsSqlDatabase(LogLevel logLevel, string message)
    {
        string connectionString = _options.ConnectionString;

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = $"INSERT INTO {_options.TableName} ({_options.Columns.TimestampColumnName}, {_options.Columns.LevelColumnName}, {_options.Columns.MessageColumnName}) VALUES (@Timestamp, @Level, @Message)";
                command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                command.Parameters.AddWithValue("@Level", logLevel.ToString());
                command.Parameters.AddWithValue("@Message", message);
                command.ExecuteNonQuery();
            }
        }
    }

    private void LogToMySqlDatabase(LogLevel logLevel, string message)
    {
        string connectionString = _options.ConnectionString;

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = $"INSERT INTO {_options.TableName} ({_options.Columns.TimestampColumnName}, {_options.Columns.LevelColumnName}, {_options.Columns.MessageColumnName}) VALUES (@Timestamp, @Level, @Message)";
                command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                command.Parameters.AddWithValue("@Level", logLevel.ToString());
                command.Parameters.AddWithValue("@Message", message);
                command.ExecuteNonQuery();
            }
        }
    }
}