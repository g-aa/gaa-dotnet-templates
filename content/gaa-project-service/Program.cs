using NLog;
using NLog.Web;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Gaa.Project.Service;

/// <summary>
/// ������� ����� ����������.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    /// <summary>
    /// ������������ �������.
    /// </summary>
    public const string ServiceName = "Web Service";

    /// <summary>
    /// ������ ����������.
    /// </summary>
    public static string CurrentVersion { get; private set; }

    /// <summary>
    /// ������������� ����������� ���������� ������ <see cref="Program"/>.
    /// </summary>
    static Program()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        CurrentVersion = $"v{fvi.FileVersion}";
    }

    /// <summary>
    /// ����� ����� � ����������.
    /// </summary>
    /// <param name="args">��������� ������� ����������.</param>
    public static async Task Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        try
        {
            logger.Debug("{0} '{1}' - ������� �� ����������...", ServiceName, CurrentVersion);
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        catch (Exception exception)
        {
            logger.Error(exception, "{0} '{1}' - ���������� �� �� ��������� �� ��������������� ����������!", ServiceName, CurrentVersion);
        }
        finally
        {
            logger.Debug("{0} '{1}' - ����������.", ServiceName, CurrentVersion);
            LogManager.Shutdown();
        }
    }

    /// <summary>
    /// ������������� ����������.
    /// </summary>
    /// <param name="args">��������� ������� ����������.</param>
    /// <returns>��������� ����������.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, logging) =>
            {
                /*
                * var nLogSection = context.Configuration.GetSection("NLog"); // ���� ������
                * LogManager.Configuration = new NLogLoggingConfiguration(nLogSection); // ���� ������
                */
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                /*
                 * logging.AddNLogWeb(); // ���� ������
                 */
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseNLog();
    }
}