using Microsoft.AspNetCore.Http;
using MimeKit;

namespace MyProject.Application.Abstractions.Services
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection optionalFiles { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection lFiles)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress("",x)));
            Subject = subject;
            Content = content;
            optionalFiles = optionalFiles;
        }
    }
}
