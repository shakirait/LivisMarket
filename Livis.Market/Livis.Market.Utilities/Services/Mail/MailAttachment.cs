

using Livis.Market.Utilities.Services.Messaging;

namespace Livis.Market.Utilities.Services.Mail
{
    public class MailAttachment: IMessageAttachment
    {
        private const string _TYPE_PDF = "application/pdf";
        private const string _DISPOSITION_ATTACHMENT = "attachment";
        private const string _DISPOSITION_INLINE = "inline";

        public MailAttachment(
            string attachmentName,
            string attachmentContent,
            string attachmentType = _TYPE_PDF,
            string attachmentDisposition = _DISPOSITION_ATTACHMENT)
        {
            AttachmentName = attachmentName;
            AttachmentContent = attachmentContent;
            AttachmentType = attachmentType;
            AttachmentDisposition = attachmentDisposition;
        }

        public string AttachmentName { get; }

        public string AttachmentContent { get; }

        public string AttachmentType { get; }

        public string AttachmentDisposition { get; }
    }
}