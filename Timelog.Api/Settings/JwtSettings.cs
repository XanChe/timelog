namespace Timelog.Api.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = String.Empty;

        public string Secret { get; set; } = String.Empty;

        public int ExpirationInDays { get; set; }
    }
}
