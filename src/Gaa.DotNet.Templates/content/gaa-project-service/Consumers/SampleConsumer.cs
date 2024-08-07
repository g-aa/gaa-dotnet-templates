using Gaa.Project.Service.Models;

using MassTransit;

namespace Gaa.Project.Service.Consumers;

/// <summary>
/// Пример обработчика сообщений из очереди.
/// </summary>
public sealed class SampleConsumer : IConsumer<SampleMessageModel>
{
    private readonly ILogger<SampleConsumer> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SampleConsumer"/>.
    /// </summary>
    /// <param name="logger">Журнал сообщений.</param>
    public SampleConsumer(ILogger<SampleConsumer> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public Task Consume(ConsumeContext<SampleMessageModel> context)
    {
        var model = context.Message;
        _logger.LogWarning("Получено и обработано сообщение '{Message}'.", model.Message);
        return Task.CompletedTask;
    }
}