namespace Shared.Infrastructure.PipelineInfrastructure;

/// <summary>
/// Интерфейс настроек инфраструктурного слоя.
/// </summary>
public interface IInfrastructureOptions
{
    /// <summary>
    /// Валидация настроек.
    /// </summary>
    void Validate();
}
