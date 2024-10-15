using System.Globalization;
using System.Text;

namespace Shared.Common.Extensions;

public static class StringExtensions
{

    public static string SubstringSmart(this string value, int length, string trimReason)
    {
        if (string.IsNullOrEmpty(value)) { return value; }

        var more = true;
        if (length > value.Length)
        {
            length = value.Length;
            more = false;
        }

        var result = value[..length];

        if (more && !string.IsNullOrEmpty(trimReason))
        {
            result += $"...[{trimReason}]";
        }

        return result;
    }

    public static string SubstringSmart(this string value, int length)
    {
        if (string.IsNullOrEmpty(value)) { return value; }

        if (length > value.Length)
        {
            length = value.Length;
        }

        return value[..length];
    }

    public static bool IsBase64(this string base64)
    {
        var buffer = new Span<byte>(new byte[base64.Length]);

        return Convert.TryFromBase64String(base64, buffer, out _);
    }

    /// <summary>
    /// Конвертировать первую буквы в верхний регистр
    /// </summary>
    /// <param name="value">Исходная строка</param>
    public static string? FirstToUpper(this string? value)
    {
        if (value == null) { return null; }

        if (value.Length < 2) { return value.ToUpper(); }

        return value[..1].ToUpper() + value[1..];
    }

    /// <summary>
    /// Конвертировать первую буквы в нижний регистр
    /// </summary>
    /// <param name="value">Исходная строка</param>
    public static string? FirstToLower(this string? value)
    {
        if (value == null) { return null; }

        if (value.Length < 2) { return value.ToLower(); }

        return value[..1].ToLower() + value[1..];
    }

    /// <summary>
    /// Приведение строки к перечислению
    /// </summary>
    /// <typeparam name="T">Тип перечисления</typeparam>
    /// <param name="value">Исходная строка</param>
    /// <param name="defaulValue">Значение по умолчанию</param>
    public static T ToEnum<T>(this string value, T defaulValue)
        where T : struct, Enum
    {
        if (string.IsNullOrEmpty(value)) { return defaulValue; }

        return Enum.TryParse(value, true, out T result) ? result : defaulValue;
    }

    /// <summary>
    /// Возвращает с конвертированное значение
    /// </summary>
    /// <typeparam name="T">Тип, в который следует с конвертировать значение <paramref name="value"/></typeparam>
    /// <param name="value">Конвертируемое значение</param>
    /// <param name="formatProvider"></param>
    /// <param name="def">Значение по умолчанию</param>
    public static T? GetValue<T>(this string value, IFormatProvider? formatProvider = null, T? def = default)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return def;
        }

        var typeVal = typeof(T);

        if (typeVal == typeof(bool))
        {
            // Эта так требуется так как в БД тип boolean храниться как 1 или 0.
            return (T)(object)(value == "1");
        }

        if (typeVal.IsGenericType && typeVal.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            typeVal = Nullable.GetUnderlyingType(typeVal);
        }

        if (value is IConvertible convertible)
        {
            return (T?)convertible.ToType(typeVal, formatProvider ?? CultureInfo.InvariantCulture);
        }

        throw new FormatException("Нельзя преобразовать тип");
    }

    /// <summary>
    /// Замена символов в строке
    /// </summary>
    /// <param name="source">Исходная строка</param>
    /// <param name="oldChars">Старые символы</param>
    /// <param name="newChars">Новые символы</param>
    /// <returns>
    /// Возвращает новую строку с заменёнными символами
    /// </returns>
    public static string ReplaceMulti(this string source, char[] oldChars, char[] newChars)
    {
        var lenChars = oldChars.Length;
        if (lenChars != newChars.Length)
        {
            throw new ArgumentException($"Размеры параметров {nameof(oldChars)} и {nameof(newChars)} не равны");
        }

        var len = source.Length;
        var result = new char[len];

        for (var i = 0; i < len; i++)
        {
            var ch = source[i];
            for (var j = 0; j < lenChars; j++)
            {
                if (ch == oldChars[j])
                {
                    ch = newChars[j];
                }
            }
            result[i] = ch;
        }

        return new string(result);
    }

    public static string ToSnakeCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        var builder = new StringBuilder(value.Length + Math.Min(2, value.Length / 5));
        var previousCategory = default(UnicodeCategory?);

        for (var currentIndex = 0; currentIndex < value.Length; currentIndex++)
        {
            var currentChar = value[currentIndex];
            if (currentChar == '_')
            {
                builder.Append('_');
                previousCategory = null;
                continue;
            }

            var currentCategory = char.GetUnicodeCategory(currentChar);
            switch (currentCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (previousCategory == UnicodeCategory.SpaceSeparator ||
                        previousCategory == UnicodeCategory.LowercaseLetter ||
                        previousCategory != UnicodeCategory.DecimalDigitNumber &&
                        previousCategory != null &&
                        currentIndex > 0 &&
                        currentIndex + 1 < value.Length &&
                        char.IsLower(value[currentIndex + 1]))
                    {
                        builder.Append('_');
                    }

                    currentChar = char.ToLower(currentChar);
                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (previousCategory == UnicodeCategory.SpaceSeparator)
                    {
                        builder.Append('_');
                    }
                    break;

                default:
                    if (previousCategory != null)
                    {
                        previousCategory = UnicodeCategory.SpaceSeparator;
                    }
                    continue;
            }

            builder.Append(currentChar);
            previousCategory = currentCategory;
        }

        return builder.ToString();
    }
}
