using Aaron.Core.SysConfiguration;

namespace Aaron.Core.Domain.Messages
{
    public class EmailAccountSettings : ISettings
    {
        public int DefaultEmailAccountId { get; set; }
    }
}
