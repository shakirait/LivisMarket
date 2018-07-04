

using Livis.Market.Utilities.Services.Messaging;

namespace Livis.Market.Utilities.Services.Mail
{
    public class MailMessage: IMessageContent
    {
        public MailMessage()
        {
            To = new MailAddressCollection();
            Cc = new MailAddressCollection();
            Bcc = new MailAddressCollection();
            Attachment = new MailAttachmentCollection();
        }

        public MailAddress From { get; set; }

        public MailAddressCollection To { get; set; }

        public MailAddressCollection Cc { get; set; }

        public MailAddressCollection Bcc { get; set; }

        public MailAttachmentCollection Attachment { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}