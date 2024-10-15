namespace Shared.Common.Utils;

/// <summary>
/// Вспомогательные функции для типа <see cref="DateTime"/>
/// </summary>
public static class DateTimeUtils
{
    private readonly static DateTime unixTimeStart = new(1970, 1, 1);

    /// <summary>
    /// Перевод дата/время в Unix Time формат
    /// </summary>
    /// <param name="value">Дата/время в UTC формате</param>
    public static int ConvertToUnixTime(DateTime value)
    {
        return (int)value.Subtract(unixTimeStart).TotalSeconds;
    }

}