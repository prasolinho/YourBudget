namespace YourBudget.Infrastructure.Settings
{
    public class JwtSettings
    {
        /// <summary>
        /// Prywatny klucz dla naszej aplikacji.
        /// </summary>
        /// <returns></returns>
        public string Key { get; set; }

        public string Issuer { get; set; }

        public int ExpiryMinutes { get; set; }
    }
}