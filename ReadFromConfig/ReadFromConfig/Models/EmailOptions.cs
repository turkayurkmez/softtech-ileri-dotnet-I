namespace ReadFromConfig.Models
{

    public abstract class EmailBase
    {
        public abstract string Host { get; set; }
        public abstract string Password { get; set; }
    }
    public class EmailOptions : EmailBase
    {

        public EmailOptions(string email)
        {
            Email = email;

        }
        public EmailOptions()
        {
                
        }
        public const string MailConfigurationName = "MailSettings";
        public override string Host { get; set; }
        public override string Password { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EnableSSL { get; set; } = true;
    }
}
