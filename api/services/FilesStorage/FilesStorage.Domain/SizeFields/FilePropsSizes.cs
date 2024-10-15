using FilesStorage.Domain.Aggregates.Files;

namespace Identity.Domain.SizeFields;

/// <summary>
/// Размер полей для <see cref="FileProps"/>
/// </summary>
public class FilePropsSizes
{

    /// <summary>
    /// Размер значения поля <see cref="FileProps.FileName"/>
    /// </summary>
    public const int FILE_NAME = 250;

}