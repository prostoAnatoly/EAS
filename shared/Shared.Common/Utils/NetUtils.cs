using Shared.Common.Extensions;

namespace Shared.Common.Utils;

/// <summary>
/// Впомогательные функции для работы с объектами связанными с сетью
/// </summary>
public static class NetUtils
{
    private static readonly char[] charsByBase64ToUrlSource = new char[] { '+', '/' };
    private static readonly char[] charsByBase64ToUrlDest = new char[] { '_', '-' };

    /// <summary>
    /// Возвращает базовый адрес
    /// </summary>
    /// <param name="scheme">Имя схемы</param>
    /// <param name="host">Имя хоста. Имя DNS или IP-адрес сервера</param>
    public static string GetBaseAddess(string scheme, string host)
    {
        return scheme + "://" + host;
    }

    /// <summary>
    /// Кодирует строку формата base64 в кодировку Request path URL-строки
    /// </summary>
    /// <param name="base64">Строка в формате base64</param>
    public static string Base64ToPathUrlEncode(string base64)
    {
        return base64.ReplaceMulti(charsByBase64ToUrlSource, charsByBase64ToUrlDest);
    }

    /// <summary>
    /// Декодирует строку в кодировке Request path URL-строки в формат base64
    /// </summary>
    /// <param name="decode">Строка в кодировке Request path URL-строки</param>
    public static string PathUrlDecodeToBase64(string decode)
    {
        return decode.ReplaceMulti(charsByBase64ToUrlDest, charsByBase64ToUrlSource);
    }

}