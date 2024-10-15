namespace Eas.Gate.Ui.Models.Identity
{
    public class LoginRequest
    {
        public string UserName { get; init; }

        /// <summary>
        /// Пароль
        /// </summary>
        /// <example>MTIz</example>
        public string Password { get; init; }
    }
}
