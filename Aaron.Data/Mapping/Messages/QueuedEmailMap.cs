using Aaron.Core;
using Aaron.Core.Domain.Messages;

namespace Aaron.Data.Mapping.Messages
{
    public class QueuedEmailMap : BaseEntityTypeConfiguration<QueuedEmail, int>
    {
        public QueuedEmailMap()
            : base("QueuedEmail")
        {
            this.Property(qe => qe.From).IsRequired().HasMaxLength(500);
            this.Property(qe => qe.FromName).HasMaxLength(500);
            this.Property(qe => qe.To).IsRequired().HasMaxLength(500);
            this.Property(qe => qe.ToName).HasMaxLength(500);
            this.Property(qe => qe.CC).HasMaxLength(500);
            this.Property(qe => qe.Bcc).HasMaxLength(500);
            this.Property(qe => qe.Subject).HasMaxLength(1000);
            this.Property(qe => qe.Body).IsMaxLength();


            this.HasRequired(qe => qe.EmailAccount)
                .WithMany()
                .HasForeignKey(qe => qe.EmailAccountId).WillCascadeOnDelete(true);
        }
    }
}
