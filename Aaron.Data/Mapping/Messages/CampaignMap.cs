using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aaron.Core;
using Aaron.Core.Domain.Messages;

namespace Aaron.Data.Mapping.Messages
{
    public class CampaignMap : BaseEntityTypeConfiguration<Campaign, int>
    {
        public CampaignMap()
            : base("Campaign")
        {
            this.Property(c => c.Name).IsRequired();
            this.Property(c => c.Subject).IsRequired();
            this.Property(c => c.Body).IsRequired();
        }
    }
}
