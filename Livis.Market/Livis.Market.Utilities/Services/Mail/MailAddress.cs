namespace Livis.Market.Utilities.Services.Mail
{
    public class MailAddress
    {
        public MailAddress(string address, string displayName = null)
        {
            Address = address;
            DisplayName = displayName;
        }

        public string Address { get; }

        public string DisplayName { get; }
    }
}