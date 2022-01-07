using System;

namespace Common
{
    public class SiteSettings
    {
        public string ElmahPath { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
        public SmsConfiguration SmsConfiguration { get; set; }
        public EmailConfiguration EmailConfiguration { get; set; }
    }

    public class IdentitySettings
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string EncryptKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }

    public class SmsConfiguration
    {
        public string BaseUrl { get; set; }
        public string SendSmsRoute { get; set; }

        public string GetTokenRoute { get; set; }
        public string LineNumberPrimary { get; set; }
        public string LineNumberSecondary { get; set; }

        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public TimeSpan TokenTtl { get; set; }
    }

    public class EmailConfiguration
    {
        public string HostAddress { get; set; }
        public int HostPort { get; set; }
        public string HostUsername { get; set; }
        public string HostPassword { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }


}
