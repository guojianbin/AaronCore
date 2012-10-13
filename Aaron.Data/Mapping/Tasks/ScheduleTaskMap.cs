using Aaron.Core;
using Aaron.Core.Domain.Tasks;

namespace Aaron.Data.Mapping.Tasks
{
    public partial class ScheduleTaskMap : BaseEntityTypeConfiguration<ScheduleTask, int>
    {
        public ScheduleTaskMap() 
            : base("ScheduleTask")
        {
            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Type).IsRequired();
        }
    }
}