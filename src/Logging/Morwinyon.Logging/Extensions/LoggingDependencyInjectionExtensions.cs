using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Net.Http.Headers;

namespace Morwinyon.Logging;

public static class LoggingDependencyInjectionExtensions
{
    public static IServiceCollection AddMorwinyonDefaultFileLogging(this IServiceCollection services)
    {
        // Varsayılan FileOptions değerlerini kullanarak bir örnek oluşturun
        var defaultOptions = new FileOptions
        {
            MinimumLogEventLevel = LogEventLevel.Information,
            FilePath = "logs/log.txt", // Varsayılan dosya yolu
            MaximumFileSizeBytes = 1024 * 1024 * 10, // 10 MB
            MaximumFileAge = TimeSpan.FromDays(7), // 1 hafta
            MaximumNumberOfFiles = 5, // En fazla 5 dosya
            LogMessageTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
            RotationStrategy = FileRotationStrategy.None, // Döngüsel rotasyon
            SizeLimitBehavior = FileSizeLimitBehavior.Truncate, // Dosya boyutu limitine ulaşıldığında dosyayı kırp
            CreateAtStartup = true,
            IsEnabled = true
        };

        // Varsayılan FileOptions örneğini IServiceCollection'a ekleyin
        services.AddSingleton(defaultOptions);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // HttpContextAccessor'ı DI konteynerine ekledik.

        // FileOptions örneğini alarak yapılacak değişiklikleri desteklemek için bir lambda ifadesi kullanarak FileLoggerProvider'ı ekleyin

        services.AddSingleton<ILoggerProvider, FileLoggerProvider>(provider =>
        {
            var options = provider.GetRequiredService<FileOptions>();

            // Kullanıcı tarafından belirlenen FileOptions ayarlarını almak için DI konteynerinden örneği alın
            var configuredOptions = new FileOptions
            {
                MinimumLogEventLevel = options.MinimumLogEventLevel,
                FilePath = options.FilePath,
                MaximumFileSizeBytes = options.MaximumFileSizeBytes,
                MaximumFileAge = options.MaximumFileAge,
                MaximumNumberOfFiles = options.MaximumNumberOfFiles,
                LogMessageTemplate = options.LogMessageTemplate,
                RotationStrategy = options.RotationStrategy,
                SizeLimitBehavior = options.SizeLimitBehavior,
                CreateAtStartup = options.CreateAtStartup,
                IsEnabled = options.IsEnabled
            };
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

            LogLevel minimumLogLevel;
            switch (options.MinimumLogEventLevel)
            {
                case LogEventLevel.Verbose:
                    minimumLogLevel = LogLevel.Trace;
                    break;
                case LogEventLevel.Debug:
                    minimumLogLevel = LogLevel.Debug;
                    break;
                case LogEventLevel.Information:
                    minimumLogLevel = LogLevel.Information;
                    break;
                case LogEventLevel.Warning:
                    minimumLogLevel = LogLevel.Warning;
                    break;
                case LogEventLevel.Error:
                    minimumLogLevel = LogLevel.Error;
                    break;
                case LogEventLevel.Fatal:
                    minimumLogLevel = LogLevel.Critical;
                    break;
                default:
                    minimumLogLevel = LogLevel.Information;
                    break;
            }

            return new FileLoggerProvider((category, logLevel) => logLevel >= minimumLogLevel, configuredOptions, httpContextAccessor);
        });

        return services;
    }

    public static IServiceCollection AddMorwiyonDefaultConsoleLogging(this IServiceCollection services)
    {
        // ConsoleOptions'ı DI konteynerine ekleyelim
        services.AddSingleton<ConsoleOptions>();

        // ConsoleLoggerProvider'ı DI konteynerine ekleyelim
        services.AddSingleton<ILoggerProvider, ConsoleLoggerProvider>();

        // ILogger kullanımını ekleyelim
        services.AddLogging();

        return services;
    }

    public static IServiceCollection AddMorwinyonDefaultDatabaseLogging(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseOptions>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // HttpContextAccessor'ı DI konteynerine ekledik.

        services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>(provider =>
        {
            var options = provider.GetRequiredService<DatabaseOptions>();

            // Kullanıcı tarafından belirlenen DatabaseOptions ayarlarını almak için DI konteynerinden örneği alın
            var configuredOptions = new DatabaseOptions
            {
                ConnectionString = "Server=localhost;Database=Logging;User Id=SA;Password=Password123;MultipleActiveResultSets=true;Encrypt=False",
                Columns = new DatabaseColumnsConfiguration
                {
                    TimestampColumnName = "Timestamp",
                    LevelColumnName = "Level",
                    MessageColumnName = "Message",
                    CustomColumnName = string.Empty,
                },
                DatabaseType = DatabaseType.MsSql,
                IsEnabled = true,
                MinimumLevel = LogEventLevel.Information,
                TableName = "Logs"

            };
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

            LogLevel minimumLogLevel;
            switch (options.MinimumLevel)
            {
                case LogEventLevel.Verbose:
                    minimumLogLevel = LogLevel.Trace;
                    break;
                case LogEventLevel.Debug:
                    minimumLogLevel = LogLevel.Debug;
                    break;
                case LogEventLevel.Information:
                    minimumLogLevel = LogLevel.Information;
                    break;
                case LogEventLevel.Warning:
                    minimumLogLevel = LogLevel.Warning;
                    break;
                case LogEventLevel.Error:
                    minimumLogLevel = LogLevel.Error;
                    break;
                case LogEventLevel.Fatal:
                    minimumLogLevel = LogLevel.Critical;
                    break;
                default:
                    minimumLogLevel = LogLevel.Information;
                    break;
            }

            return new DatabaseLoggerProvider((category, logLevel) => logLevel >= minimumLogLevel, configuredOptions, httpContextAccessor);
        });

        return services;
    }
}
