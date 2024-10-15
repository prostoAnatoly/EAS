namespace Identity.Domain.SizeFields;

/// <summary>
/// Общие размеры полей.
/// </summary>
class CommonSizes
{

    /// <summary>
    /// Размер значения IP-адреса для версии 4.
    /// </summary>
    internal const int IPv4 = 15;

    /// <summary>
    /// Размер значения имени персоны.
    /// </summary>
    internal const int PERSON_NAME = 50;

    /// <summary>
    /// Размер значения фамилии персоны.
    /// </summary>
    internal const int PERSON_SURNAME = 200;

    /// <summary>
    /// Размер значения отчества персоны.
    /// </summary>
    internal const int PERSON_PATRONYMIC = 50;

    /// <summary>
    /// Размер значения мобильного номера телефона.
    /// </summary>
    internal const int MOBILE_PHONE_NUMBER = 11;

    /// <summary>
    /// Размер значения адреса электронной почты.
    /// </summary>
    internal const int EMAIL = 255;
}
