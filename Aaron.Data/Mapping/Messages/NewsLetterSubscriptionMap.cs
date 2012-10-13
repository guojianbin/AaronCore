using System;
using Aaron.Core;
using Aaron.Core.Domain.Messages;

namespace Aaron.Data.Mapping.Messages
{
    public class NewsLetterSubscriptionMap : BaseEntityTypeConfiguration<NewsLetterSubscription, Guid>
    {
        public NewsLetterSubscriptionMap()
            : base("NewsLetterSubscription")
        {
            this.Property(n => n.Email).IsRequired().HasMaxLength(255);
        }
    }
}
